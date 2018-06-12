using Api;
using Api.Helpers;
using Api.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using Nancy.Owin;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Nancy.Template.WebService
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private AppSettings Settings = new AppSettings();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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
            Configuration.GetSection(nameof(AppSettings)).Bind(Settings);

            services.AddSingleton(Settings);
            services.AddSingleton<JsonSerializer, CustomJsonSerializer>();

            services.AddTransient<Stopwatch>();
            services.AddTransient<IHelloRepository, HelloRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseOwin(x => x.UseNancy(options => options.Bootstrapper = new Api.Bootstrapper(Settings)));
        }
    }
}
