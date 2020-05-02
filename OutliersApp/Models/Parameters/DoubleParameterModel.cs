using System;

namespace OutliersApp.Models.Parameters
{
    public class DoubleParameterModel : ParameterModelBase
    {
        double _value;
        public override int Id { get; set; } = 1;
        public double Min { get; set; }
        public double Max { get; set; }
        public double Default { get; set; }
        public double Value
        {
            get { return _value;}
            set
            {
                if (!(Min <= value && value <= Max) && !IsCustom)
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
            IsCustom = false;
        }

        public DoubleParameterModel(string name) : base(name)
        {
            IsCustom = true;
        }
    }
}