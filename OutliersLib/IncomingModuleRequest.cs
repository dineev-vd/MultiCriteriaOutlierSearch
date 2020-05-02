using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Net;

namespace OutliersLib
{
    /// <summary>
    /// Структура входящего запроса модуля
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class IncomingModuleRequest
    {
        public string Name { get; set; }
        public bool Internal { get; set; }
        public string Uri { get; set; }
        public double Weight { get; set; }
        public Dictionary<string, object> Params { get; set; }
        public IncomingModuleRequest()
        {
            Internal = true;
            Uri = String.Empty;
            Params = new Dictionary<string, object>();
            Weight = 1;
        }

        public IncomingModuleRequest(string name, bool intern, string uri, double weight, Dictionary<string, object> pars)
        {
            Name = name;
            Internal = intern;
            Uri = uri;
            Params = pars;
            Weight = weight;
        }

    }
}
