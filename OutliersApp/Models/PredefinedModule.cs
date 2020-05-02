using OutliersLib.ParameterTypes;
using System;
using System.Collections.Generic;

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
            return new PredefinedModule(Name, CoolName, CloneDict(Settings));
        }

        public Dictionary<string, ParameterBase> CloneDict(Dictionary<string, ParameterBase> dict)
        {
            var result = new Dictionary<string, ParameterBase>();
            foreach (var item in dict)
            {
                result.Add(item.Key, (ParameterBase)item.Value.Clone());
            }

            return result;
        }
    }
}