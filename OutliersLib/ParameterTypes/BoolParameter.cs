using OutliersApp.Models.Parameters;

namespace OutliersLib.ParameterTypes
{
    public class BoolParameter : ParameterBase
    {
        public bool Default { get; set; }
        public bool Value { get; set; }
        
        
        public override string DefaultToString()
        {
            return Default.ToString();
        }
    }
}