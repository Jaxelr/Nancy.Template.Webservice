using System.Diagnostics;
using Nancy;
using Nancy.Metadata.Modules;
using Nancy.Metadata.OpenApi.Core;
using Nancy.Metadata.OpenApi.Fluent;
using Nancy.Template.WebService.Extensions;
using Nancy.Template.WebService.Models.Entities;
using Nancy.Template.WebService.Models.Operations;
using Nancy.Template.WebService.Repositories;

namespace Nancy.Template.WebService.Modules
{
    public class SampleMetadataModule : MetadataModule<OpenApiRouteMetadata>
    {
        public SampleMetadataModule()
        {
            Describe[nameof(Hello)] = desc => new OpenApiRouteMetadata(desc)
                .With(i => i.WithResponseModel(HttpStatusCode.OK, typeof(HelloResponse), "Hello Response")
                .WithDescription("This operation returns the hello world message", tags: new string[] { "Hello" })
                .WithResponseModel(HttpStatusCode.BadRequest, typeof(string), "Failed Validation Response")
                .WithRequestParameter("name", description: "A string to return as part of hello world", loc: Loc.Query, type: typeof(string)));
        }
    }

    public class SampleModule : NancyModule
    {
        private readonly Stopwatch watch;
        private readonly IHelloRepository repo;

        public SampleModule(Stopwatch watch, IHelloRepository repo) : base("api")
        {
            this.repo = repo;

            this.watch = watch;
            this.watch.Restart();

            this.GetHandler<HelloRequest, HelloResponse>(nameof(Hello), HelloOp);
        }

        public HelloResponse HelloOp(HelloRequest user) => new HelloResponse { Response = repo.SayHello(user.Name) };
    }
}
