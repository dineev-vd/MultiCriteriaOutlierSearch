using System.Collections.Generic;

namespace OutliersLib
{
    /// <summary>
    /// Информация о внутреннем модуле
    /// </summary>
    public class InternalModuleConfig { 
        public string Uri { get; set; }
        public string CoolName { get; set; }
        public Dictionary<string, Parameter> Parameters { get; set; }

        public InternalModuleConfig()
        {
            Uri = "";
            CoolName = "";
            Parameters = new Dictionary<string, Parameter>();
        }
    }
    
}