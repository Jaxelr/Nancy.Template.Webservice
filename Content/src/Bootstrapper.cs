using Nancy;
using Nancy.Bootstrapper;
using Nancy.RapidCache.Extensions;
using Nancy.Routing;
using Nancy.Serilog;
using Nancy.TinyIoc;
using Serilog;
using Serilog.Formatting.Json;
using System;

namespace Api
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly int CacheTimespan;
        private readonly bool CacheEnabled;

        public Bootstrapper(AppSettings settings)
        {
            CacheEnabled = settings.Cache.CacheEnabled;
            CacheTimespan = settings.Cache.CacheTimespan;
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            pipelines.EnableSerilog();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(new JsonFormatter(), "log.txt")
                .MinimumLevel.Debug()
                .CreateLogger();

            if (CacheEnabled)
            {
                this.EnableRapidCache(container.Resolve<IRouteResolver>(), ApplicationPipelines, new[] { "query", "form", "accept" });
                pipelines.AfterRequest.AddItemToStartOfPipeline(ConfigureCache);
            }
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context) => 
            container.Register((tinyIoc, namedParams) => context.GetContextualLogger());

        public void ConfigureCache(NancyContext context)
        {
            if (context.Response.StatusCode == HttpStatusCode.OK && context.Request.Method == "GET")
            {
                context.Response = context.Response.AsCacheable(DateTime.UtcNow.AddSeconds(CacheTimespan));
            }
        }
    }
}
