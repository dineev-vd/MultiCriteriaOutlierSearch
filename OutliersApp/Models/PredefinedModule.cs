using System;
using System.Collections.Generic;
using System.Linq;
using OutliersApp.Models.Parameters;
using OutliersLib.ParameterTypes;

namespace OutliersApp.Models
{
    public class PredefinedModule : ICloneable
    {
        public string Name { get; set; }
        public string CoolName { get; set; }
        public Dictionary<string, ParameterBase> Settings { get; set; }

        public PredefinedModule()
        {
            Name = string.Empty;
            CoolName = string.Empty;
            Settings = new Dictionary<string, ParameterBase>();
        }

        public PredefinedModule(string name, string coolName, Dictionary<string, ParameterBase> settings)
        {
            Name = name;
            CoolName = coolName;
            Settings = settings;
        }

        public object Clone()
        {
            return new PredefinedModule(Name, CoolName,
                new Dictionary<string, ParameterBase>(Settings.Select(x => x.Clone() as KeyValuePair<string,ParameterBase>)));
        }
    }
}