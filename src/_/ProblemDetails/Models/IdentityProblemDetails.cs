using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using $safeprojectname$.Resources;

namespace $safeprojectname$._.Problems
{
    public class IdentityProblemDetails : ProblemDetails
    {
        public IdentityProblemDetails(string details, string provider,IStringLocalizer<Resource> localizer)
            : base()
        {
            Title = localizer[Resource.identity_problem_title];
            Status = StatusCodes.Status400BadRequest;
            Detail = details;
            Extensions["provider"] = provider;
        }
    }
}
