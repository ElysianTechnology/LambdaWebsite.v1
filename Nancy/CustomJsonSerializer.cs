using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MachSecure.SVM.OverviewService.Nancy
{
    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            Formatting = Formatting.Indented;
        }
    }
}
