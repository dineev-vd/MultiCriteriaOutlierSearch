using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OutliersLib
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ModuleResponse
    {
        public string Status { get; set; }
        public List<double> Data { get; set; }
        public ModuleResponse(string status, List<double> data)
        {
            Status = status;
            Data = data;
        }
    }
}
