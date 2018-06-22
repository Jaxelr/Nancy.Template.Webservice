using Newtonsoft.Json;

namespace Api.Models.Entities
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Hello
    {
        public string Name { get; set; }
    }
}
