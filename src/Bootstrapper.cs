using Nancy;
using Nancy.Bootstrapper;
using Nancy.LightningCache.CacheKey;
using Nancy.LightningCache.Extensions;
using Nancy.Routing;
using Nancy.TinyIoc;
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
            if (CacheEnabled)
            {
                this.EnableLightningCache(container.Resolve<IRouteResolver>(), ApplicationPipelines, new DefaultCacheKeyGenerator(new[] { "query", "form", "accept" }));
                pipelines.AfterRequest.AddItemToStartOfPipeline(ConfigureCache);
            }
        }

        public void ConfigureCache(NancyContext context)
        {
            if (context.Response.StatusCode == HttpStatusCode.OK && context.Request.Method == "GET")
            {
                context.Response = context.Response.AsCacheable(DateTime.Now.AddSeconds(CacheTimespan));
            }
        }
    }
}
