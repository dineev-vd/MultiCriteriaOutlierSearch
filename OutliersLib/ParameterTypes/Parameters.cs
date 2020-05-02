using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using OutliersApp.Models.Parameters;

namespace OutliersLib.ParameterTypes
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Parameters : IEnumerable<Parameters>
    {
        [JsonProperty("bool")] public Dictionary<string, BoolParameter> BoolParameters { get; set; }
        [JsonProperty("int")] public Dictionary<string, IntParameter> IntParameters { get; set; }
        [JsonProperty("double")] public Dictionary<string, DoubleParameter> DoubleParameters { get; set; }
        [JsonProperty("string")] public Dictionary<string, StringParameter> StringParameters { get; set; }
        [JsonProperty("select")] public Dictionary<string, SelectParameter> SelectParameters { get; set; }
    }
}