using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutliersLib
{
    public class ModuleJson
    {
        public object Data { get; set; }
        public Dictionary<string, object> Params { get; set; }
        public ModuleJson(object data, Dictionary<string, object> pars)
        {
            Data = data;
            Params = pars;
        }

    }
}
