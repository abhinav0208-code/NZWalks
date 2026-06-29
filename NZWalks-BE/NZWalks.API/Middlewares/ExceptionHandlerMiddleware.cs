namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //calls the next middlewares and if any exception is there then catch block will get executed.
                await next(httpContext);
            }
            catch (Exception ex)
            {

                Guid exceptionId = Guid.NewGuid();
                logger.LogError(ex, $"{exceptionId} : {ex.Message}");

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";


                var errorObjet = new
                {
                    Id = exceptionId,
                    ErrorMessage = "exception handler middleware is sending this error message"

                };

                //adding error object in response body.
               await  httpContext.Response.WriteAsJsonAsync(errorObjet);
            }
        }
    }
}
