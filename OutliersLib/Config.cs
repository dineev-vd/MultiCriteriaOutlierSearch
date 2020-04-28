using System;
using System.Collections.Generic;
using System.Text;

namespace OutliersLib
{
    public class Parameters
    {
        public string Type { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public object Default { get; set; }
    }
    public class ModuleSettings { 
        public string Uri { get; set; }
        public string CoolName { get; set; }
        public Dictionary<string, Parameters> Options { get; set; }

        public ModuleSettings()
        {
            Uri = "";
            CoolName = "";
            Options = new Dictionary<string, Parameters>();
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
