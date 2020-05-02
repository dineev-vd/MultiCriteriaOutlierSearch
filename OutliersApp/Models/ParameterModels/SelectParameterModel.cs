using OutliersLib.ParameterTypes;
using System.Collections.Generic;

namespace OutliersApp.Models.ParameterModels
{
    public class SelectParameterModel : SelectParameter
    {
        public string Value { get; set; }
        public string Default { get; set; }
        public List<string> Options { get; set; }
    }
}