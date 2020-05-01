using System;

namespace OutliersApp.Models.Parameters
{
    public class DoubleParameterModel : ParameterModelBase
    {
        public double _value;
        public double Min { get; set; }
        public double Max { get; set; }
        public double Default { get; set; }
        public double Value
        {
            get { return _value;}
            set
            {
                if (!(Min <= value && value <= Max))
                {
                    throw new ArgumentOutOfRangeException();
                }

                _value = value;
            }
        }

        public DoubleParameterModel(string name, double min, double max, double def) : base(name)
        {
            Min = min;
            Max = max;
            Default = def;
            Value = Default;
        }
    }
}