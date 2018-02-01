using Nancy;
using Nancy.Metadata.Swagger.Modules;
using Nancy.Routing;
using System.Threading.Tasks;

namespace Api.Modules
{
    public class DocsModule : SwaggerDocsModuleBase
    {
        public DocsModule(IRouteCacheProvider routeCacheProvider)
            : base(routeCacheProvider,
              "/api/docs",                      // where module should be located
              "Hello Api",                      // title
              "v1.0",                             // api version
              "localhost:50657",                // host
              "/api",                           // api base url (ie /dev, /api)
              "http")                           // schemes
        {
            Get("/", async (x, ct) => await Task.Run(() => Response.AsRedirect("/index.html")));
        }
    }
}
