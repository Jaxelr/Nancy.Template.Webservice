using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using Nancy.Owin;

namespace Nancy.Template.WebService
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        private readonly AppSettings settings = new AppSettings();

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //HealthChecks
            services.AddHealthChecks(checks =>
            {
                checks.AddValueTaskCheck("HTTP Endpoint", () => new
                    ValueTask<IHealthCheckResult>(HealthCheckResult.Healthy("Ok")));
            });

            //Extract the AppSettings information from the appsettings config.
            Configuration.GetSection(nameof(AppSettings)).Bind(settings);

            services.AddSingleton(settings);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseOwin(x => x.UseNancy(options => options.Bootstrapper = new Bootstrapper(settings)));
        }
    }
}
