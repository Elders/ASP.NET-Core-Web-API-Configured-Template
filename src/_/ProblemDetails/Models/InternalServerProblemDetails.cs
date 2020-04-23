using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using $safeprojectname$.Resources;

namespace $safeprojectname$._.Problems
{
    /// <summary>
    /// Whenever something extraordinary for the system happens which is not part of the normal work flow which could be handled, an InternalServerError 500 is returned as a response
    /// </summary>
    public class InternalServerProblemDetails : ProblemDetails
    {
        public InternalServerProblemDetails(Exception exception, bool isProduction, IStringLocalizer<Resource> localizer)
            : base()
        {
            Title = localizer[Resource.internal_server_title];
            Status = StatusCodes.Status500InternalServerError;
            Detail = isProduction ? localizer[Resource.internal_server_details] : exception.ToString();
        }

        public InternalServerProblemDetails(Exception exception, HttpContext httpContext, bool isProduction, IStringLocalizer<Resource> localizer)
            : base()
        {
            Title = localizer[Resource.internal_server_title];
            Status = StatusCodes.Status500InternalServerError;
            Detail = isProduction ? localizer[Resource.internal_server_details] : exception.ToString();
        }

        public InternalServerProblemDetails(string errorMessage, IStringLocalizer<Resource> localizer)
            : base()
        {
            Title = localizer[Resource.internal_server_title];
            Status = StatusCodes.Status500InternalServerError;
            Detail = errorMessage;
        }
    }
}
