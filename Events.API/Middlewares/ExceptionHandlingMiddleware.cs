using Events.Business.Utility;
using Events.Domain.DTO;
using System.Data;
using System.Net;
using System.Text.Json;

namespace Events_Web_Application.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (InvalidPasswordException ex) 
            {
                await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.BadRequest, "Wrong password");
            }
            catch (InvalidTokenException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.BadRequest, "Invalid token");
            }
            catch (DuplicatedObjectException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.BadRequest, "Trying to duplicate existing object");
            }
            catch (NullReferenceException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.NotFound, "Object is null");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.BadRequest, "Smth is wrong");
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string exMsg, HttpStatusCode httpStatusCode,
            string message)
        {
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatusCode;

            ErrorDto errorDto = new()
            {
                Message = exMsg + "; " + message,
                StatusCode = (int)httpStatusCode
            };

            string result = JsonSerializer.Serialize(errorDto);

            await response.WriteAsJsonAsync(result);
        }
    }
}
