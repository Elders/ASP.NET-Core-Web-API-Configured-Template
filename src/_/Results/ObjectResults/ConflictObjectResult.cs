using Microsoft.AspNetCore.Mvc;
using $safeprojectname$._.Results.Responses;

namespace $safeprojectname$._.Results.ObjectResults
{
    public class ConflictObjectResult : ObjectResult
    {
        public ConflictObjectResult(ProblemDetails problemDetails)
            : base(new ErrorResult(problemDetails))
        {
            StatusCode = problemDetails.Status;
        }
    }
}
