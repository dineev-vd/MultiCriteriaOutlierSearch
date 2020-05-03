using System;
using Newtonsoft.Json;
using OutliersApp.Models.Parameters;

namespace OutliersLib.ParameterTypes
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class BoolParameterModel : ParameterBase
    {
        private bool _default;

        public bool Default
        {
            get => _default;
            set
            {
                Value = value;
                _default = value;
            }
        }

        public bool Value { get; set; }

        public BoolParameterModel()
        {
            Default = true;
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
                Value = bool.Parse(input);
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
            return new BoolParameterModel()
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
    }
}