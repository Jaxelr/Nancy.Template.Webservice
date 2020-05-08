using Nancy.Metadata.Modules;
using Nancy.Metadata.OpenApi.Core;
using Nancy.Metadata.OpenApi.Fluent;
using Nancy.Template.WebService.Models.Entities;
using Nancy.Template.WebService.Models.Operations;

namespace Nancy.Template.WebService.Modules.Metadata
{
    public class HelloMetadataModule : MetadataModule<OpenApiRouteMetadata>
    {
        public HelloMetadataModule()
        {
            Describe[nameof(Hello)] = desc => new OpenApiRouteMetadata(desc)
                .With(i => i.WithResponseModel(HttpStatusCode.OK, typeof(HelloResponse), "Hello Response")
                .WithDescription("This operation returns the hello world message", tags: new string[] { "Hello" })
                .WithResponseModel(HttpStatusCode.BadRequest, typeof(string), "Failed Validation Response")
                .WithRequestParameter("name", description: "A string to return as part of hello world", loc: Loc.Query, type: typeof(string)));
        }
    }
}
