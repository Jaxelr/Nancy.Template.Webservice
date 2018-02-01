using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using Nancy.Owin;
using System.Threading.Tasks;

namespace Nancy.Template.WebService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
            =>
            //HealthChecks
            services.AddHealthChecks(checks =>
            {
                checks.AddValueTaskCheck("HTTP Endpoint", () => new
                    ValueTask<IHealthCheckResult>(HealthCheckResult.Healthy("Ok")));
            });

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseOwin(x => x.UseNancy());
        }
    }
}
