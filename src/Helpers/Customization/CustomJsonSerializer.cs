using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Helpers
{
    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            Formatting = Formatting.Indented;
            NullValueHandling = NullValueHandling.Ignore;
        }
    }
}
