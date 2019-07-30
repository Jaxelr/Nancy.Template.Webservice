using Nancy.Template.WebService.Models.Entities;
using Newtonsoft.Json;

namespace Nancy.Template.WebService.Models.Operations
{
    [JsonObject(MemberSerialization.OptOut)]
    public class HelloRequest
    {
        public string Name { get; set; }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class HelloResponse
    {
        public Hello Response { get; set; }
    }
}
