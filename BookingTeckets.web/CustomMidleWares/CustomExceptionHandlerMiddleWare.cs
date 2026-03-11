using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace BookingTeckets.web.CustomMidleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleWare>
            logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                await HandleNotFoundEndPointAsync(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var response = new ProblemDetails()
                {
                    Title = "Internal Server Error",
                    Detail = ex.Message, 
                    Status = StatusCodes.Status500InternalServerError,
                    Instance = context.Request.Path
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound && !httpContext.Response.HasStarted)
            {
                //var response = new ErrorToReturn()
                //{
                //    StatusCode = StatusCodes.Status404NotFound,
                //    ErrorMessage = $"End Point {httpContext.Request.Path} is not found"
                //};
                var response = new ProblemDetails()
                {
                    Title = "Error while Processing the Http Request - EndPoint Not Found",
                    Detail = $"End Point {httpContext.Request.Path} is not found",
                    Status = StatusCodes.Status404NotFound,
                    Instance = httpContext.Request.Path
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }





    }
}
