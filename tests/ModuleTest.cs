using Api.Modules;
using Api.Repository;
using Api.Validators;
using Nancy.Testing;
using NSubstitute;
using System.Diagnostics;
using Xunit;

namespace Nancy.Template.WebService.Tests
{
    public class ModuleTest
    {
        private readonly string AcceptHeader = @"application/json";
        private IHelloRepository repository = Substitute.For<IHelloRepository>();

        [Fact]
        public void Sample_module_returns_hello()
        {
            //Arrange
            string username = "User";
            string HelloPath = string.Concat("/api/Hello");

            repository.SayHello(username).Returns($"Hello, {username}");

            var browser = new Browser(with =>
            {
                with.Module<SampleModule>();
                with.Dependency(new Stopwatch());
                with.Dependency(new HelloValidator());
                with.Dependency(repository);
            });

            //Act
            var response = browser.Get(HelloPath, with =>
            {
                with.Header("Accept", AcceptHeader);
                with.Query("Name", username);
            });

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.Result.StatusCode);
            Assert.Contains(username, response.Result.Body.AsString());
        }

        [Fact]
        public void Sample_module_returns_400_code()
        {
            //Arrange
            string HelloPath = string.Concat("/api/Hello");

            var browser = new Browser(with =>
            {
                with.Module<SampleModule>();
                with.Dependency(new Stopwatch());
                with.Dependency(new HelloValidator());
                with.Dependency(repository);
            });

            //Act
            var response = browser.Get(HelloPath, with =>
            {
                with.Header("Accept", AcceptHeader);
            });

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.Result.StatusCode);
        }
    }
}
