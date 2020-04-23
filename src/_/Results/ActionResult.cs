using Microsoft.AspNetCore.Mvc;
using $safeprojectname$._.Results.ObjectResults;
using BadRequestObjectResult = $safeprojectname$._.Results.ObjectResults.BadRequestObjectResult;
using ConflictObjectResult = $safeprojectname$._.Results.ObjectResults.ConflictObjectResult;

namespace $safeprojectname$
{
    public static class ActionResult
    {
        public static OkObjectResult<T> Ok<T>(T result) where T : class
        {
            return new OkObjectResult<T>(result);
        }

        public static OkObjectResult<object> Ok()
        {
            return new OkObjectResult<object>();
        }

        public static BadRequestObjectResult BadRequest(ProblemDetails problemDetails) => new BadRequestObjectResult(problemDetails);

        public static InternalServerErrorObjectResult InternalServerError(ProblemDetails problemDetails) => new InternalServerErrorObjectResult(problemDetails);

        public static NotFoundErrorObjectResult NotFound(ProblemDetails problemDetails) => new NotFoundErrorObjectResult(problemDetails);

        public static ConflictObjectResult Conflict(ProblemDetails problemDetails) => new ConflictObjectResult(problemDetails);

    }
}
