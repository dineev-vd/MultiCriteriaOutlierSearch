using Newtonsoft.Json;
using OutliersLib;
using System.Collections.Generic;

namespace OutliersApp.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Responses
    {
        public List<ModuleResponse> AlgResponses { get; set; }
        public List<ModuleResponse> CombResponses { get; set; }

        public Responses()
        {
            AlgResponses = new List<ModuleResponse>();
            CombResponses = new List<ModuleResponse>();
        }
    }
}