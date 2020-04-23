namespace $safeprojectname$._.Results.Responses
{
    /// <summary>
    /// Wrapper for returning the generic Response object. It is used for better understandability of the code by having concrete type
    /// </summary>
    public class SuccessResult<T> : Response<T> where T : class
    {
        public SuccessResult() : base() { }

        public SuccessResult(T result) : base(result) { }
    }
}
