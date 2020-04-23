using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using $safeprojectname$.Resources;

namespace $safeprojectname$._.Problems
{
    /// <summary>Problem Details Object used for problems related to the validation of an incoming request</summary>
    public class ValidationProblemDetails : ProblemDetails
    {
        public ValidationProblemDetails(string details, ICollection<ValidationError> validationErrors, IStringLocalizer<Resource> localizer)
            : this(details, localizer)
        {
            Extensions["validationErrors"] = validationErrors;
        }

        public ValidationProblemDetails(string details, IStringLocalizer<Resource> localizer)
            : base()
        {
            Title = localizer[Resource.entered_information_is_invalid];
            Detail = details;
            Status = StatusCodes.Status400BadRequest;
        }
    }

    public class ValidationError
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
