using System;
using Newtonsoft.Json;

namespace OutliersLib.ParameterTypes
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class StringParameterModel : ParameterBase
    {
        private string _default;

        public string Default
        {
            get => _default;
            set
            {
                Value = value;
                _default = value;
            }
        }

        [JsonIgnore]
        public string Value { get; set; }

        public StringParameterModel()
        {
            Default = String.Empty;
        }

      

        public override void SetValue(string input)
        {
            StringValue = input;
            Value = input;
        }

        public override object Clone()
        {
            return new StringParameterModel()
            {
                StringValue = this.StringValue,
                _default = this._default,
                Value = this.Value,
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