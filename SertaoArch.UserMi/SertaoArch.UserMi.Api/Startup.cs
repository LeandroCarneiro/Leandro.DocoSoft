using SertaoArch.UserMi.Bootstrap;
using SertaoArch.UserMi.Common.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;

namespace SertaoArch.UserMi.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
                options.ValueLengthLimit = int.MaxValue;
            });

            services.AddCors();

            services.AddControllers();
            services.AddOpenApiDocument();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClient();

            DIBootstrap.RegisterTypes(services);

            services.AddSwaggerDocument(document =>
            {
                document.Title = "User Mi";
                document.DocumentName = "swagger";
                document.Version = "v1";
                document.Description = "API for User Management";
                document.PostProcess = d =>
                {
                    d.Info.Version = "v1";
                    d.Info.Title = "User Mi";
                    d.Info.Description = "API for User Management";
                    d.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "SertaoArch",
                        Email = "",
                        Url = "https://sertaoarch.com"
                    };
                    d.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "MIT",
                        Url = "https://opensource.org/license/mit/"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
