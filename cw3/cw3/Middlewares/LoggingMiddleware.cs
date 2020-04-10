using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using cw3.Services;

namespace MiddleWare3.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            if (context.Request != null)
            {
                string path = context.Request.Path;
                string method = context.Request.Method;
                string queryString = context.Request?.QueryString.ToString();
                string bodyStr = "";

                using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }
                string logpath = @"D:\APBD\cw3\Log.txt";
                if (!File.Exists(logpath))
                {
                    File.Create(logpath).Dispose();
                }
                StreamWriter sw = File.AppendText(logpath);
                sw.WriteLine("START " + DateTime.Now);
                sw.WriteLine(method);
                sw.WriteLine(path);
                sw.WriteLine(bodyStr);
                sw.WriteLine(queryString);
                sw.WriteLine("END " + DateTime.Now);
                sw.Close();
            }

            if (_next != null)
            {
                await _next(context);
            }
        }
    }
}