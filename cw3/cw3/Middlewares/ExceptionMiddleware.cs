using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using cw3.Models;
using Microsoft.AspNetCore.Http;

namespace MiddleWare3.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if (exception is SqlException)
            {
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Wystąpił błąd w komunikacji z serverem\n" + exception.Message
                }.ToString());
            }
            return context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "Wystąpił jakiś błąd, bliżej nie zidentyfikowany"
            }.ToString());
        }
    }
}