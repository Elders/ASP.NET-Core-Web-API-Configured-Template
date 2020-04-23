using Microsoft.AspNetCore.Mvc;
using $safeprojectname$._.Results.Responses;

namespace $safeprojectname$._.Results.ObjectResults
{
    /// <summary>
    /// Object Result used to wrap Not Found Error
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ObjectResult" />
    public class NotFoundErrorObjectResult : ObjectResult
    {
        public NotFoundErrorObjectResult(ProblemDetails problemDetails)
            : base(new ErrorResult(problemDetails))
        {
            StatusCode = problemDetails.Status;
        }
    }
}
