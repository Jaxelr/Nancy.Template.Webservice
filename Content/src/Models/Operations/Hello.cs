using Nancy.Template.WebService.Models.Entities;

namespace Nancy.Template.WebService.Models.Operations
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
