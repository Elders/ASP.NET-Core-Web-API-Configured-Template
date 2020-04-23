using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using $safeprojectname$._.Problems;
using $safeprojectname$._.Results.Responses;

namespace $safeprojectname$._.ExceptionHandling
{
    /// <summary>
    /// Intercepts all not handled exceptions in the API
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            var problemDetailsFactory = app.ApplicationServices.GetRequiredService<ProblemDetailsFactory>();
            var logger = app.ApplicationServices.GetRequiredService<ILogger<ProblemDetailsFactory>>();

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        ProblemDetails problemDetails = ResolveProblemDetails(problemDetailsFactory, contextFeature);

                        string problemDetailsAsString = JsonSerializer.Serialize(problemDetails);

                        logger.LogError(problemDetailsAsString, contextFeature.Error);

                        var response = new ErrorResult(problemDetails);

                        SetResponseStatusCode(context, response);

                        context.Response.ContentType = "application/problem+json";

                        var responseAsString = JsonSerializer.Serialize(response); //, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        await context.Response.WriteAsync(responseAsString);
                    }
                });
            });
        }

        private static void SetResponseStatusCode(HttpContext context, ErrorResult response)
        {
            if (response.Error.Status.HasValue)
                context.Response.StatusCode = response.Error.Status.Value;
        }

        private static ProblemDetails ResolveProblemDetails(ProblemDetailsFactory problemDetailsFactory, IExceptionHandlerFeature contextFeature)
        {
            var problemDetails = default(ProblemDetails);

            if (contextFeature.Error is BadHttpRequestException badHttpRequestException)
                problemDetails = problemDetailsFactory.GetValidationProblem(badHttpRequestException.Message);
            else
                problemDetails = problemDetailsFactory.GetInternalServerError(contextFeature.Error);
            return problemDetails;
        }
    }
}
