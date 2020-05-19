using OutliersLib.ParameterTypes;

namespace OutliersLib
{
    /// <summary>
    /// Информация о внутреннем модуле
    /// </summary>
    public class InternalModuleConfig { 
        public string Uri { get; set; }
        public string FullName { get; set; }

        public Parameters Parameters { get; set; }

        public InternalModuleConfig()
        {
            Uri = "";
            FullName = "";
            Parameters = new Parameters();
        }
    }
}