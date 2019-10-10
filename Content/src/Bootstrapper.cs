using System;
using System.Diagnostics;
using System.Net.Http;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.RapidCache.Extensions;
using Nancy.Routing;
using Nancy.Serilog;
using Nancy.Template.WebService.Repositories;
using Nancy.TinyIoc;
using Serilog;
using Serilog.Formatting.Json;
using Nancy.Template.WebService.Properties;

namespace Nancy.Template.WebService
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly AppSettings settings;

        public Bootstrapper(AppSettings settings)
        {
            this.settings = settings;
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            pipelines.EnableSerilog();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(new JsonFormatter(), "log.txt")
                .MinimumLevel.Debug()
                .CreateLogger();

            if (settings.Cache.CacheEnabled)
            {
                this.EnableRapidCache(container.Resolve<IRouteResolver>(), ApplicationPipelines, new[] { "query", "form", "accept" });
                pipelines.AfterRequest.AddItemToStartOfPipeline(ConfigureCache);
            }
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register(settings);
            container.Register<Stopwatch, Stopwatch>();
            container.Register<IHelloRepository, HelloRepository>();
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context) =>
            container.Register((tinyIoc, namedParams) => context.GetContextualLogger());

        private void ConfigureCache(NancyContext context)
        {
            if (context.Response.StatusCode == HttpStatusCode.OK &&
                (context.Request.Method == HttpMethod.Get.Method ||
                context.Request.Method == HttpMethod.Head.Method))
            {
                context.Response = context.Response.AsCacheable(DateTime.UtcNow.AddSeconds(settings.Cache.CacheTimespan));
            }
        }
    }
}
