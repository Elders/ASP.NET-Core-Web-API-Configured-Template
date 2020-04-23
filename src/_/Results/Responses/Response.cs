using Microsoft.AspNetCore.Mvc;

namespace $safeprojectname$._.Results.Responses
{
    /// <summary>
    /// General response object. Unifies the response under common format
    /// </summary>
    /// <typeparam name="T">The type of the object which is added to the 'result' property in case of successful result</typeparam>
    public class Response<T> where T : class
    {
        public Response() { }

        public Response(ProblemDetails error)
        {
            Error = error ?? throw new System.ArgumentNullException(nameof(error));
        }

        public Response(T result)
        {
            Result = result ?? throw new System.ArgumentNullException(nameof(result));
        }

        public ProblemDetails Error { get; set; }

        public T Result { get; set; }
    }
}
