using Newtonsoft.Json;

namespace Nancy.Template.WebService.Models.Entities
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Hello
    {
        public string Name { get; set; }
    }
}
