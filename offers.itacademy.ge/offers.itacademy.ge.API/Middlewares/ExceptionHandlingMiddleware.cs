using offers.itacademy.ge.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace offers.itacademy.ge.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";

                var error = new { message = ex.Message };
                var json = JsonSerializer.Serialize(error);

                await context.Response.WriteAsync(json);
            }
            catch (WrongRequestException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var error = new { message = ex.Message };
                var json = JsonSerializer.Serialize(error);

                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    status = context.Response.StatusCode,
                    message = "Something went wrong. Please contact the administrator"
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}