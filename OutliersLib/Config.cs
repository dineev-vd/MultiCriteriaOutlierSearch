using System;
using System.Collections.Generic;
using System.Text;

namespace OutliersLib
{
    public class Parameter
    {
        public string Type { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public object Default { get; set; }
        public List<string> Data { get; set; }
    }
    public class ModuleSettings { 
        public string Uri { get; set; }
        public string CoolName { get; set; }
        public Dictionary<string, Parameter> Parameters { get; set; }

        public ModuleSettings()
        {
            Uri = "";
            CoolName = "";
            Parameters = new Dictionary<string, Parameter>();
        }
    }
    public class Config
    {
        public Dictionary<string, ModuleSettings> Algorithms { get; set; }
        public Dictionary<string, ModuleSettings> Combinations { get; set; }
        public Config()
        {
            Algorithms = new Dictionary<string, ModuleSettings>();
            Combinations = new Dictionary<string, ModuleSettings>();
        }
    }
}
