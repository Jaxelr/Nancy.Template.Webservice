using Api.Models.Entities;
using Newtonsoft.Json;

namespace Api.Models.Operations
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
