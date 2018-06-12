using System;

namespace Api.Repository
{
    public class HelloRepository : IHelloRepository
    {
        public string SayHello(string name) => $"Hello, {name} : The time is: {DateTime.Now.ToLongTimeString()}.";
    }
}
