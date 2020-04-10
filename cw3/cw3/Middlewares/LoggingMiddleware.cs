using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cw3.DAL;

namespace MiddleWare3.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IDbService service)
        {
            if (httpContext.Request != null)
            {
                string sciezka = httpContext.Request.Path;
                string querystring = httpContext.Request?.QueryString.ToString();
                string metoda = httpContext.Request.Method.ToString();
                string bodyStr = "";

                using (StreamReader reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                }
                //logowanie do pliku
            }
            await _next(httpContext);
        }
    }
}