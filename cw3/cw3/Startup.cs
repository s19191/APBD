using System;
using System.Text;
using cw3.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MiddleWare3.Middlewares;

namespace cw3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer=true,
                        ValidateAudience=true,
                        ValidateLifetime=true,
                        ValidIssuer="Gakko",
                        ValidAudience="Students",
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))
                    };
                });
            Console.Write(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            services.AddTransient<IStudentDbService, SqlServerStudentDbService>();
            services.AddControllers().AddXmlSerializerFormatters();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStudentDbService service)
        {
            // app.UseMiddleware<LoggingMiddleware>();
            //
            // app.UseMiddleware<ExceptionMiddleware>();
            //
            // app.Use(async (context, next) =>
            // {
            //     if (!context.Request.Headers.ContainsKey("Index"))
            //     {
            //         context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //         await context.Response.WriteAsync("Musisz podaćnumer indeksu");
            //         return;
            //     }
            //     string index = context.Request.Headers["Index"].ToString();
            //     bool ifExists = service.CheckIndexNumber(index);
            //     if (!ifExists)
            //     {
            //         context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //         await context.Response.WriteAsync("Studnet o podanym indeksie nie istnieje!");
            //         return;
            //     }
            //     await next();
            // });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}