using System;
using System.Collections.Generic;
using System.Linq;
using OutliersApp.Models.Parameters;

namespace OutliersApp.Models
{
    public class PredefinedModule : ICloneable
    {
        public string Name { get; set; }
        public string CoolName { get; set; }
        public List<ParameterModelBase> Settings { get; set; }

        public PredefinedModule()
        {
            Name = string.Empty;
            CoolName = string.Empty;
            Settings = new List<ParameterModelBase>();
        }

        public PredefinedModule(string name, string coolName, List<ParameterModelBase> settings)
        {
            Name = name;
            CoolName = coolName;
            Settings = settings;
        }

        public object Clone()
        {
            return new PredefinedModule(Name, CoolName,
                new List<ParameterModelBase>(Settings.Select(x => x.Clone() as ParameterModelBase)));
        }
    }
}