using System;
using System.Globalization;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

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
        
        [JsonIgnore]
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
                Value = double.Parse(input.Replace('.', ','));
                IsValid = true;
            }
            catch (ArgumentNullException)
            {
                IsValid = false;
                ErrorMessage = "Введите непустое значение";
            }
            catch (FormatException)
            {
                IsValid = false;
                ErrorMessage = "Введенное значение имеет неверный формат";
            }
            catch (ArgumentOutOfRangeException)
            {
                IsValid = false;
                ErrorMessage = $"Введите значение в интервале от {Min} до {Max}";
            }
            catch (OverflowException)
            {
                IsValid = false;
                ErrorMessage = "Введенное значение выходит за пределы типа Double";
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

        public override object GetValue()
        {
            return Value;
        }
    }
}