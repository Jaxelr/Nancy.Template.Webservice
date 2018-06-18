using System;

namespace Api.Repositories
{
    public class HelloRepository : IHelloRepository
    {
        public string SayHello(string name) => $"Hello, {name} : The time is: {DateTime.Now.ToLongTimeString()}.";
    }
}
