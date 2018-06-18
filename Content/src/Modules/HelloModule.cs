using Api.Helpers;
using Api.Model.Operations;
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
        private Stopwatch _watch;
        private IHelloRepository _repo;

        public SampleModule(Stopwatch watch, IHelloRepository repo) : base("api")
        {
            _repo = repo;
            _watch = watch;
            _watch.Restart();

            this.GetHandler<Hello, HelloResponse>(nameof(Hello), Hello);
        }

        public HelloResponse Hello(Hello user) => new HelloResponse { Result = _repo.SayHello(user.Name) };
    }
}
