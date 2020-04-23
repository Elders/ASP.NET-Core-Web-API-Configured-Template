using Microsoft.AspNetCore.Mvc;

namespace $safeprojectname$._.Results.Responses
{
    /// <summary>
    /// Wrapper for returning the generic Response object. It is used for better understandability of the code
    /// by having concrete type
    /// </summary>
    public class ErrorResult : Response<object>
    {
        public ErrorResult(ProblemDetails error) : base(error)
        {
        }
    }
}
