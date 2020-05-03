using System;
using System.Globalization;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using OutliersApp.Models.Parameters;

namespace OutliersLib.ParameterTypes
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class DoubleParameterModel : ParameterBase
    {
        private double _default;
        private double _value;
        public double Min { get; set; }
        public double Max { get; set; }

        public double Default
        {
            get
            {
                if (Min <= _default && _default <= Max)
                {
                    return _default;
                }

                if (Min == double.MinValue)
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

        public double Value
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

        public DoubleParameterModel()
        {
            Min = double.MinValue;
            Max = double.MaxValue;
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
                IsValid = true;
                Value = Default;
                return;
            }
            
            try
            {
                Value = double.Parse(input.Replace('.',','));
                IsValid = true;
            }
            catch(Exception e)
            {
                IsValid = false;
                ErrorMessage = e.Message;
            }
        }

        public override object Clone()
        {
            return new DoubleParameterModel()
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