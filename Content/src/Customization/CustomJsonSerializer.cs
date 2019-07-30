using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Nancy.Template.WebService.Customization
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
