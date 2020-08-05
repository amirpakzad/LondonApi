using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LondonAPI.Context;
using LondonAPI.Filter;
using LondonAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSwag.AspNetCore;
using Microsoft.EntityFrameworkCore.InMemory;
namespace LondonAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<HotelInfo>(Configuration.GetSection("Info"));
            services.AddControllers(options =>
            {
                options.Filters.Add<JsonExceptionFilter>();
                options.Filters.Add<RequireHttpsCloseAttribute>();
            });
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSwaggerDocument();
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion=new ApiVersion(1,0);  
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });
            //Use in-memory database for quick development and testing
            //TODO:Swap for a real database in production
            services.AddDbContext<HotelApiDbContext>(options =>
                {
                    options.UseInMemoryDatabase("hotelDb");
                }
                    );
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyApp",
                    policy=>policy.
                        AllowAnyOrigin());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseSwagger(
                settings =>
                {
                    settings.Path = "/swagger/" + "v1" + "/swagger.json";
                    settings.PostProcess = (document, request) =>
                    {
                        document.Host = request.Headers["X-Forwarded-Host"].FirstOrDefault();

                    };
                });
            app.UseSwaggerUi3(options =>
            {
                options.DocumentPath= "/swagger"+"/v1"+"/swagger.json";
            });
            app.UseRouting();

            
            //app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseCors("AllowMyApp");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
