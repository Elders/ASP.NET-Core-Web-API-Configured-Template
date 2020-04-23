using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using $safeprojectname$.Resources;

namespace $safeprojectname$._.Problems
{
    public class ProblemDetailsFactory
    {
        private readonly bool isProduction;
        private readonly IStringLocalizer<Resource> localizer;

        public ProblemDetailsFactory(IWebHostEnvironment env, IStringLocalizer<Resource> localizer)
        {
            isProduction = env.IsProduction();
            this.localizer = localizer;
        }

        public ValidationProblemDetails GetValidationProblem(string detailsMessage, ICollection<ValidationError> errors)
        {
            return new ValidationProblemDetails(detailsMessage, errors, localizer);
        }

        public ValidationProblemDetails GetValidationProblem(string detailsMessage)
        {
            return new ValidationProblemDetails(detailsMessage, localizer);
        }

        public ResourceNotFoundProblemDetails GetResourceNotFound(string detailsMessage)
        {
            return new ResourceNotFoundProblemDetails(detailsMessage, localizer);
        }

        public InternalServerProblemDetails GetInternalServerError(Exception ex)
        {
            return new InternalServerProblemDetails(ex, isProduction, localizer);
        }

        public InternalServerProblemDetails GetInternalServerError(string details)
        {
            return new InternalServerProblemDetails(details, localizer);
        }

        public IdentityProblemDetails GetIdentityProblem(string details, string provider)
        {
            return new IdentityProblemDetails(details, provider, localizer);
        }
    }
}
