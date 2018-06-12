using Newtonsoft.Json;

namespace Api.Model.Operations
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Hello
    {
        public string Name { get; set; }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class HelloResponse
    {
        public string Result { get; set; }
    }
}
