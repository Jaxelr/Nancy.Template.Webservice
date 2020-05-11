using System;
using Nancy.Template.WebService.Entities;

namespace Nancy.Template.WebService.Repositories
{
    public class HelloRepository : IHelloRepository
    {
        public Hello SayHello(string name) => new Hello { Name = $"Hello, {name} : The time is: {DateTime.Now.ToLongTimeString()}." };
    }
}
