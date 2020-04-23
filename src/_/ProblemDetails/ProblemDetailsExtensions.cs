using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using $safeprojectname$._.Results.Responses;
using $safeprojectname$.Resources;

namespace $safeprojectname$._.Problems
{
    /// <summary>
    /// Extensions related to ProblemDetails 
    /// </summary>
    public static class ProblemDetailsExtensions
    {
        /// <summary>
        /// Configures all custom behavior or settings needed for using ProblemDetails
        /// </summary>
        /// <param name="services"></param>
        public static void AddCustomProblemDetails(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.Add(new ServiceDescriptor(typeof(ProblemDetailsFactory), typeof(ProblemDetailsFactory), ServiceLifetime.Transient));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ctx => new AspModelValidation(services);
            });
        }
    }

    /// <summary>
    /// Registered as 'InvalidModelStateResponseFactory' in 'ApiBehaviorOptions' so that it can intercept
    /// all validation made on request models and in case of errors return correct ProblemDetail response.
    /// Used whenever asp.net returns invalid model while still trying to bind the model to the action
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.IActionResult" />
    public class AspModelValidation : IActionResult
    {
        private readonly IStringLocalizer<Resource> localizer;

        public AspModelValidation(IServiceCollection services)
        {
            localizer = services.BuildServiceProvider().GetService<IStringLocalizer<Resource>>();
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            ValidationProblemDetails validationProblemDetail = GenerateValidationProblemDetail(context);
            WriteProblemDetailToTheResponse(context, validationProblemDetail);

            return Task.CompletedTask;
        }

        private static void WriteProblemDetailToTheResponse(ActionContext context, ValidationProblemDetails problemDetails)
        {
            var response = new ErrorResult(problemDetails);
            var responseAsString = JsonSerializer.Serialize(response);

            if (response.Error.Status.HasValue)
                context.HttpContext.Response.StatusCode = response.Error.Status.Value;

            context.HttpContext.Response.ContentType = "application/problem+json";
            context.HttpContext.Response.WriteAsync(responseAsString).Wait();
        }

        private ValidationProblemDetails GenerateValidationProblemDetail(ActionContext context)
        {
            var modelStateEntries = context.ModelState.Where(e => e.Value.Errors.Count > 0).ToArray();
            var errors = new List<ValidationError>();

            var details = localizer[Resource.check_details_for_more_info].Value;

            if (modelStateEntries.Any())
            {
                if (RequestHasFundamentalProblem(modelStateEntries))
                {
                    details = modelStateEntries[0].Value.Errors[0].ErrorMessage;
                }
                else
                {
                    foreach (var modelStateEntry in modelStateEntries)
                    {
                        foreach (var modelStateError in modelStateEntry.Value.Errors)
                        {
                            var error = new ValidationError
                            {
                                Name = modelStateEntry.Key,
                                Description = modelStateError.ErrorMessage
                            };

                            errors.Add(error);
                        }
                    }
                }
            }

            var problemDetails = new ValidationProblemDetails(details, errors, localizer);
            return problemDetails;
        }

        private static bool RequestHasFundamentalProblem(KeyValuePair<string, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateEntry>[] modelStateEntries)
        {
            return modelStateEntries.Length == 1 && modelStateEntries[0].Value.Errors.Count == 1 && modelStateEntries[0].Key == string.Empty;
        }
    }
}
