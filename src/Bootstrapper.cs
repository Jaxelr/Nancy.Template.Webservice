using Api.Helpers;
using Api.Repository;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.LightningCache.CacheKey;
using Nancy.LightningCache.Extensions;
using Nancy.Routing;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Api
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly int CacheTimespan = 60;
        private readonly bool CacheEnabled = true;

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            if (CacheEnabled)
            {
                this.EnableLightningCache(container.Resolve<IRouteResolver>(), ApplicationPipelines, new DefaultCacheKeyGenerator(new[] { "id", "query", "take", "skip", "accept" }));
                pipelines.AfterRequest.AddItemToStartOfPipeline(ConfigureCache);
            }
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<JsonSerializer, CustomJsonSerializer>();

            var templateAssembly = typeof(Bootstrapper).Assembly;

            container.Register<Stopwatch, Stopwatch>();
            container.Register<IHelloRepository, HelloRepository>();
        }

        public void ConfigureCache(NancyContext context)
        {
            if (context.Response.StatusCode == HttpStatusCode.OK && context.Request.Method == "GET")
            {
                context.Response = context.Response.AsCacheable(System.DateTime.Now.AddSeconds(CacheTimespan));
            }
        }
    }
}
