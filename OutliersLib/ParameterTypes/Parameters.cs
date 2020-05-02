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
        [JsonProperty("bool")] public Dictionary<string, BoolParameter> BoolParameters { get; set; }
        [JsonProperty("int")] public Dictionary<string, IntParameter> IntParameters { get; set; }
        [JsonProperty("double")] public Dictionary<string, DoubleParameter> DoubleParameters { get; set; }
        [JsonProperty("string")] public Dictionary<string, StringParameter> StringParameters { get; set; }
        [JsonProperty("select")] public Dictionary<string, SelectParameter> SelectParameters { get; set; }

        public Parameters()
        {
            BoolParameters = new Dictionary<string, BoolParameter>();
            IntParameters = new Dictionary<string, IntParameter>();
            DoubleParameters = new Dictionary<string, DoubleParameter>();
            StringParameters = new Dictionary<string, StringParameter>();
            SelectParameters = new Dictionary<string, SelectParameter>();
        }


        public IEnumerator<KeyValuePair<string,ParameterBase>> GetEnumerator()
        {
            var dict = new Dictionary<string, ParameterBase>();
            foreach (var parameter in BoolParameters)
            {
                dict.Add(parameter.Key, parameter.Value);
            }

            foreach (var parameter in IntParameters)
            {
                dict.Add(parameter.Key,parameter.Value);
            }

            foreach (var parameter in DoubleParameters)
            {
                dict.Add(parameter.Key, parameter.Value);
            }

            foreach (var parameter in StringParameters)
            {
                dict.Add(parameter.Key,parameter.Value);
            }

            foreach (var parameter in SelectParameters)
            {
                dict.Add(parameter.Key,parameter.Value);
            }

            return dict.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

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