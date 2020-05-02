using System.Collections.Generic;
using OutliersApp.Models.Parameters;
using OutliersLib.ParameterTypes;

namespace OutliersLib
{
    /// <summary>
    /// Информация о внутреннем модуле
    /// </summary>
    public class InternalModuleConfig { 
        public string Uri { get; set; }
        public string CoolName { get; set; }

        public Parameters Parameters { get; set; }

        public InternalModuleConfig()
        {
            Uri = "";
            CoolName = "";
            Parameters = new Parameters();
        }
    }
    
}