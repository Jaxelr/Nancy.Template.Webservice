using System.Diagnostics;
using Nancy;
using Nancy.Template.WebService.Validators;
using Nancy.Template.WebService.Modules;
using Nancy.Template.WebService.Repositories;
using Nancy.Testing;
using NSubstitute;
using Xunit;

namespace Nancy.Template.WebService.Tests
{
    public class ModuleTest
    {
        private readonly IHelloRepository repository = Substitute.For<IHelloRepository>();

        [Fact]
        public void Hello_module_returns_hello()
        {
            //Arrange
            string Name = Fakes.FakeProps.Username;
            string HelloPath = Fakes.FakeProps.HelloPath;
            string Accept = Fakes.FakeProps.AcceptHeader;

            repository.SayHello(Name).Returns(new Entities.Hello { Name = $"Hello, {Name}" });

            var browser = new Browser(with =>
            {
                with.Module<HelloModule>();
                with.Dependency(new Stopwatch());
                with.Dependency(new HelloValidator());
                with.Dependency(repository);
            });

            //Act
            var response = browser.Get(HelloPath, with =>
            {
                with.Header(nameof(Accept), Accept);
                with.Query(nameof(Name), Name);
            });

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.Result.StatusCode);
            Assert.Contains(Name, response.Result.Body.AsString());
        }

        [Fact]
        public void Hello_module_returns_400_code()
        {
            //Arrange
            string HelloPath = Fakes.FakeProps.HelloPath;
            string Accept = Fakes.FakeProps.AcceptHeader;

            var browser = new Browser(with =>
            {
                with.Module<HelloModule>();
                with.Dependency(new Stopwatch());
                with.Dependency(new HelloValidator());
                with.Dependency(repository);
            });

            //Act
            var response = browser.Get(HelloPath, with => with.Header(nameof(Accept), Accept));

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.Result.StatusCode);
        }
    }
}
