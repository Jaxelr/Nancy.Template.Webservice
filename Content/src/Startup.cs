using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Nancy;
using Nancy.Owin;
using Nancy.Template.WebService.Entities;
using Newtonsoft.Json.Linq;

namespace Nancy.Template.WebService
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private readonly AppSettings settings;

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();
            Configuration = builder.Build();

            //Extract the AppSettings information from the appsettings config.
            settings = new AppSettings();
            Configuration.GetSection(nameof(AppSettings)).Bind(settings);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //HealthChecks
            services.AddHealthChecks();

            //For Kestrel usage
            services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

            //For IIS usage
            services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);

            services.AddSingleton(settings); //typeof(AppSettings)
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHealthChecks("/healthcheck", new HealthCheckOptions()
            {
                ResponseWriter = WriteResponse
            });

            app.UseStaticFiles();

            app.UseOwin(x => x.UseNancy(options => options.Bootstrapper = new Bootstrapper(settings)));
        }

        private static Task WriteResponse(HttpContext context, HealthReport report)
        {
            context.Response.ContentType = "application/json";

            var json = new JObject(
                        new JProperty("statusCode", report.Status),
                        new JProperty("status", report.Status.ToString()),
                        new JProperty("timelapsed", report.TotalDuration)
                );

            return context.Response.WriteAsync(json.ToString(Newtonsoft.Json.Formatting.Indented));
        }
    }
}
