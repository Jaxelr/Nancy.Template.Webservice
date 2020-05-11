using Nancy.Template.WebService.Entities;

namespace Nancy.Template.WebService.Operations
{
    public class HelloRequest
    {
        public string Name { get; set; }
    }

    public class HelloResponse
    {
        public Hello Response { get; set; }
    }
}
