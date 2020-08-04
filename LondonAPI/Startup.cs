using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LondonAPI.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSwag.AspNetCore;
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
            services.AddControllers(options =>
            {
                options.Filters.Add<JsonExceptionFilter>();
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
            app.UseHttpsRedirection();
            app.UseSwaggerUi3(options =>
            {
                options.DocumentPath= "/swagger"+"/v1"+"/swagger.json";
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
