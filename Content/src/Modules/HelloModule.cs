using Api.Helpers;
using Api.Models.Operations;
using Api.Models.Entities;
using Api.Repositories;
using Nancy;
using Nancy.Metadata.Modules;
using Nancy.Metadata.Swagger.Core;
using Nancy.Metadata.Swagger.Fluent;
using System.Diagnostics;

namespace Api.Modules
{
    public class SampleMetadataModule : MetadataModule<SwaggerRouteMetadata>
    {
        public SampleMetadataModule()
        {
            Describe[nameof(Hello)] = desc => new SwaggerRouteMetadata(desc)
                .With(i => i.WithResponseModel("200", typeof(HelloResponse), "Hello Response")
                .WithDescription("This operation returns the hello world message", tags: new string[] { "Hello" })
                .WithResponseModel("400", typeof(string), "Failed Validation Response")
                .WithRequestParameter("name", description: "A string to return as part of hello world", loc: "query", type: "string"));
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
