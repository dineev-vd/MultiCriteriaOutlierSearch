using System;
using OutliersLib.ParameterTypes;

namespace OutliersApp.Models.Parameters
{
    public abstract class IntParameter : ParameterBase
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Default { get; set; }
        public int Value { get; set; }
        public override string DefaultToString()
        {
            return Default.ToString();
        }
    }
}