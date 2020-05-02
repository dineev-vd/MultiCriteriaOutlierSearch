using OutliersLib.ParameterTypes;

namespace OutliersApp.Models.ParameterModels
{
    public class BoolParameterModel : BoolParameter
    {
        public bool Default { get; set; }
        public bool Value { get; set; }

        public BoolParameterModel(string coolName)
        {
            CoolName = coolName;
            Default = false;
            Value = Default;
        }
    }
}