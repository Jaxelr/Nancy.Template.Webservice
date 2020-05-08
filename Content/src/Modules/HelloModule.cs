using System.Diagnostics;
using Nancy;
using Nancy.Template.WebService.Extensions;
using Nancy.Template.WebService.Models.Entities;
using Nancy.Template.WebService.Models.Operations;
using Nancy.Template.WebService.Repositories;

namespace Nancy.Template.WebService.Modules
{
    public class HelloModule : NancyModule
    {
        private readonly Stopwatch watch;
        private readonly IHelloRepository repo;

        public HelloModule(Stopwatch watch, IHelloRepository repo) : base("api")
        {
            this.repo = repo;

            this.watch = watch;
            this.watch.Restart();

            this.Get<HelloRequest, HelloResponse>(nameof(Hello), HelloOp);
        }

        public HelloResponse HelloOp(HelloRequest user) => new HelloResponse { Response = repo.SayHello(user.Name) };
    }
}
