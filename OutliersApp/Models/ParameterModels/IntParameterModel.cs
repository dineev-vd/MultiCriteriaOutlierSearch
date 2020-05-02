using System;

namespace OutliersApp.Models.ParameterModels
{
    public class IntParameterModel : ParameterModelBase
    {
        int _value;
        public override int Id { get; } = 5;
        public int Min { get; set; }
        public int Max { get; set; }
        public int Default { get; set; }

        public int Value
        {
            get => _value;
            set
            {
                if (!(Min <= value && value <= Max) && !IsCustom)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _value = value;
            }
        }

        public IntParameterModel(string name,string coolName, int min, int max, int def) : base(name, coolName)
        {
            Min = min;
            Max = max;
            Default = def;
            Value = Default;
            IsCustom = false;
        }

        public IntParameterModel(string name, string coolName) : base(name, coolName)
        {
            IsCustom = true;
        }

        public override object Clone()
        {
            return new IntParameterModel(Name, CoolName, Min, Max, Default);
        }
    }
}