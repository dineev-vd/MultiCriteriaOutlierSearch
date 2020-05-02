using OutliersApp.Models.Parameters;
using System;

namespace OutliersApp.Models.ParameterModels
{
    public class IntParameterModel : IntParameter
    {
        int _value;

        public int Min { get; set; }
        public int Max { get; set; }
        public int Default { get; set; }

        public int Value
        {
            get => _value;
            set
            {
                if (!(Min <= value && value <= Max))
                {
                    throw new ArgumentOutOfRangeException();
                }

                _value = value;
            }
        }

        public IntParameterModel(string coolName)
        {
            CoolName = coolName;
            Min = int.MinValue;
            Max = int.MaxValue;
            Default = 0;
            Value = Default;
        }
    }
}