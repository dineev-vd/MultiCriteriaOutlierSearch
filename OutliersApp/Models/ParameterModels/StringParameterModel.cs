using OutliersLib.ParameterTypes;

namespace OutliersApp.Models.ParameterModels
{
    public class StringParameterModel : StringParameter
    {
        public string Default { get; set; }
        public string Value { get; set; }

        public StringParameterModel(string coolName)
        {
            CoolName = coolName;
            Default = string.Empty;
            Value = Default;
        }
    }
}