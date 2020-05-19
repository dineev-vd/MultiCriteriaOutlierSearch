using OutliersLib.ParameterTypes;
using System;
using System.Collections.Generic;

namespace OutliersApp.Models
{
    public class PredefinedModule : ICloneable
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public Dictionary<string, ParameterBase> Settings { get; set; }

        public PredefinedModule()
        {
            Name = string.Empty;
            FullName = string.Empty;
            Settings = new Dictionary<string, ParameterBase>();
        }

        public PredefinedModule(string name, string fullName, Dictionary<string, ParameterBase> settings)
        {
            Name = name;
            FullName = fullName;
            Settings = settings;
        }

        public object Clone()
        {
            return new PredefinedModule(Name, FullName, CloneDict());
        }

        public Dictionary<string, ParameterBase> CloneDict()
        {
            var result = new Dictionary<string, ParameterBase>();
            foreach (var item in Settings)
            {
                result.Add(item.Key, (ParameterBase)item.Value.Clone());
            }

            return result;
        }
    }
}