using System;
using Newtonsoft.Json;
using OutliersLib.ParameterTypes;

namespace OutliersApp.Models.Parameters
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class IntParameterModel : ParameterBase
    {
        private int _default;
        private int _value;
        public int Min { get; set; }
        public int Max { get; set; }

        public int Default
        {
            get
            {
                if (Min <= _default && _default <= Max)
                {
                    return _default;
                }

                if (Min == int.MinValue)
                {
                    return Max;
                }

                return Min;
            }
            set
            {
                Value = value;
                _default = value;
            }
        }

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

        public IntParameterModel()
        {
            Min = int.MinValue;
            Max = int.MinValue;
            Default = 0;
        }

        public override string DefaultToString()
        {
            return Default.ToString();
        }

        public override void SetValue(string input)
        {
            StringValue = input;
            
            if (input == string.Empty)
            {
                Value = Default;
                IsValid = true;
                return;
            }

            try
            {
                Value = int.Parse(input);
                IsValid = true;
            }
            catch (Exception e)
            {
                IsValid = false;
                ErrorMessage = e.Message;
            }
        }

        public override object Clone()
        {
            return new IntParameterModel()
            {
                StringValue = this.StringValue,
                _default = this._default,
                _value = this._value,
                Min = this.Min,
                Max = this.Max,
                IsValid = this.IsValid,
                IsCustom = this.IsCustom,
                ErrorMessage = this.ErrorMessage,
                FullName = this.FullName
            };
        }
    }
}