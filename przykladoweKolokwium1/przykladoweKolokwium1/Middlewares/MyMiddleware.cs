using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace przykladoweKolokwium1.Middlewares
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        
        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("IndexNumber","s19191");
            if (_next != null)
            {
                await _next(context);
            }
        }
    }
}