using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using Newtonsoft.Json;
using OutliersApp.Models.Parameters;

namespace OutliersLib.ParameterTypes
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Parameters : IEnumerable<KeyValuePair<string, ParameterBase>>
    {
        [JsonProperty("bool")] public Dictionary<string, BoolParameterModel> BoolParameters { get; set; }
        [JsonProperty("int")] public Dictionary<string, IntParameterModel> IntParameters { get; set; }
        [JsonProperty("double")] public Dictionary<string, DoubleParameterModel> DoubleParameters { get; set; }
        [JsonProperty("string")] public Dictionary<string, StringParameterModel> StringParameters { get; set; }
        [JsonProperty("select")] public Dictionary<string, SelectParameterModel> SelectParameters { get; set; }


        public IEnumerator<KeyValuePair<string, ParameterBase>> GetEnumerator()
        {
            var dict = new Dictionary<string, ParameterBase>();

            foreach (var parameter in BoolParameters ??= new Dictionary<string, BoolParameterModel>())
            {
                if (!(parameter.Value is null))
                    dict.Add(parameter.Key, parameter.Value);
            }

            foreach (var parameter in IntParameters ??= new Dictionary<string, IntParameterModel>())
            {
                if (!(parameter.Value is null))
                    dict.Add(parameter.Key, parameter.Value);
            }

            foreach (var parameter in DoubleParameters ??= new Dictionary<string, DoubleParameterModel>())
            {
                if (!(parameter.Value is null))
                    dict.Add(parameter.Key, parameter.Value);
            }

            foreach (var parameter in StringParameters ??= new Dictionary<string, StringParameterModel>())
            {
                if (!(parameter.Value is null))
                    dict.Add(parameter.Key, parameter.Value);
            }

            foreach (var parameter in SelectParameters ??= new Dictionary<string, SelectParameterModel>())
            {
                if (!(parameter.Value is null))
                    dict.Add(parameter.Key, parameter.Value);
            }

            return dict.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [JsonIgnore]
        public int Count
        {
            get
            {
                int count = 0;
                foreach (var item in this)
                {
                    count++;
                }

                return count;
            }
        }
    }
}