using System;

namespace OutliersApp.Models.ParameterModels
{
    public class DoubleParameterModel : ParameterModelBase
    {
        double _value;
        public override int Id { get; } = 1;
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

        public DoubleParameterModel(string name,string coolName, double min, double max, double def) : base(name, coolName)
        {
            Min = min;
            Max = max;
            Default = def;
            Value = Default;
            IsCustom = false;
        }

        public DoubleParameterModel(string name, string coolName) : base(name, coolName)
        {
            IsCustom = true;
        }

        public override object Clone()
        {
            return new DoubleParameterModel(Name,CoolName, Min, Max, Default);
        }
    }
}