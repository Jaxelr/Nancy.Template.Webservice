using Api.Models.Entities;
using System;

namespace Api.Repositories
{
    public class HelloRepository : IHelloRepository
    {
        public Hello SayHello(string name) => new Hello { Name = $"Hello, {name} : The time is: {DateTime.Now.ToLongTimeString()}." };
    }
}
