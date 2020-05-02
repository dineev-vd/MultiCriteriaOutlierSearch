using System;
using OutliersApp.Models.Parameters;

namespace OutliersLib.ParameterTypes
{
    public class DoubleParameter : ParameterBase
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public double Default { get; set; }
        public double Value { get; set; }
        public override string DefaultToString()
        {
            return Default.ToString();
        }
    }
}