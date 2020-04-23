using Microsoft.AspNetCore.Mvc;
using $safeprojectname$._.Results.Responses;

namespace $safeprojectname$._.Results.ObjectResults
{
    /// <summary>
    /// Object Result used to wrap InternalServerErrors
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ObjectResult" />
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(ProblemDetails problemDetails)
            : base(new ErrorResult(problemDetails))
        {
            StatusCode = problemDetails.Status;
        }
    }
}
