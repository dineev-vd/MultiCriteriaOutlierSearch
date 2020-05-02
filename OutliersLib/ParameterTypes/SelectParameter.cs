using System.Collections.Generic;
using OutliersApp.Models.Parameters;

namespace OutliersLib.ParameterTypes
{
    public class SelectParameter : ParameterBase
    {
        public string Value { get; set; }
        public string Default { get; set; }
        public List<string> Options { get; set; }
    }
}