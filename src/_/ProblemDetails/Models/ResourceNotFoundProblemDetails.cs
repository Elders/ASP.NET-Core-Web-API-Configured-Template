using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using $safeprojectname$.Resources;

namespace $safeprojectname$._.Problems
{
    /// <summary>
    /// This problem should be used each time the resource which is requested, for some reason, would not be found and returned
    /// </summary>
    public class ResourceNotFoundProblemDetails : ProblemDetails
    {
        public ResourceNotFoundProblemDetails(string detailsMessage, IStringLocalizer<Resource> localizer) : base()
        {
            Title = localizer[Resource.resource_not_found];
            Detail = detailsMessage;
            Status = StatusCodes.Status404NotFound;
        }
    }
}
