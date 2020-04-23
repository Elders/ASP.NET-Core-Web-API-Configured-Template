using Microsoft.AspNetCore.Mvc;
using $safeprojectname$._.Results.Responses;

namespace $safeprojectname$._.Results.ObjectResults
{
    public class BadRequestObjectResult : ObjectResult
    {
        public BadRequestObjectResult(ProblemDetails problemDetails) : base(new ErrorResult(problemDetails))
        {
            StatusCode = problemDetails.Status;
        }
    }
}
