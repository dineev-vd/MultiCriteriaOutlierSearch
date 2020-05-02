using OutliersLib.ParameterTypes;
using System;

namespace OutliersApp.Models.ParameterModels
{
    public class DoubleParameterModel : DoubleParameter
    {
        double _value;
        public double Min { get; set; }
        public double Max { get; set; }
        public double Default { get; set; }
        public double Value
        {
            get { return _value; }
            set
            {
                if (!(Min <= value && value <= Max))
                {
                    throw new ArgumentOutOfRangeException();
                }

                _value = value;
            }
        }

        public DoubleParameterModel(string coolName)
        {
            CoolName = coolName;
            Min = double.NegativeInfinity;
            Max = double.PositiveInfinity;
            Default = 0;
            Value = Default;
        }
    }
}