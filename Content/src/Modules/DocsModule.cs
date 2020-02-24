using System.Threading.Tasks;
using Nancy;
using Nancy.Metadata.OpenApi.Model;
using Nancy.Metadata.OpenApi.Modules;
using Nancy.Routing;
using Nancy.Template.WebService.Properties;

namespace Nancy.Template.WebService.Modules
{
    public class DocsModule : OpenApiDocsModuleBase
    {
        public DocsModule(IRouteCacheProvider routeCacheProvider, AppSettings appSettings)
            : base(routeCacheProvider,
              appSettings.Metadata.DocsPath,                            // where module should be located
              appSettings.Metadata.Title,                               // title
              appSettings.Metadata.Version,                             // api version
              host: new Server()                                        // host
              {
                  Description = appSettings.Metadata.Host.Description,
                  Url = appSettings.Metadata.Host.Url
              }
            )
        {
            Get("/", async (_, __) => await Task.FromResult(Response.AsRedirect("/index.html")).ConfigureAwait(false));
        }
    }
}
