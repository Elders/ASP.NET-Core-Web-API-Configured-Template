using Microsoft.AspNetCore.Mvc;
using $safeprojectname$._.Results.Responses;

namespace $safeprojectname$._.Results.ObjectResults
{
    public class OkObjectResult<T> : ObjectResult where T : class
    {
        public OkObjectResult(T value) : base(new SuccessResult<T>(value))
        {
            StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
        }

        public OkObjectResult() : base(new SuccessResult<T>())
        {
            StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
        }
    }
}
