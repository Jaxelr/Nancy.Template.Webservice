using System.Threading.Tasks;
using Nancy.Metadata.OpenApi.Model;
using Nancy.Metadata.OpenApi.Modules;
using Nancy.Routing;

namespace Nancy.Template.WebService.Modules
{
    public class DocsModule : OpenApiDocsModuleBase
    {
        public static Server Server => new Server() { Description = "Localhost", Url = "http://localhost:50657" };

        public DocsModule(IRouteCacheProvider routeCacheProvider)
            : base(routeCacheProvider,
              "/api/docs",                      // where module should be located
              "Hello Api",                      // title
              "v1.0",                           // api version
              host: Server)                     // host
        {
            Get("/", async (x, ct) => await Task.Run(() => Response.AsRedirect("/index.html")));
        }
    }
}
