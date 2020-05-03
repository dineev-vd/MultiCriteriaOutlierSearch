using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using OutliersApp.Models.Parameters;

namespace OutliersLib.ParameterTypes
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SelectParameterModel : ParameterBase
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

        public string Value { get; set; }
        
        [JsonRequired]
        public List<string> Options { get; set; }

        public SelectParameterModel()
        {
            Default = string.Empty;
        }

        public override string DefaultToString()
        {
            return Default.ToString();
        }

        public override void SetValue(string input)
        {
            StringValue = input;
            
            if (!Options.Contains(input))
            {
                IsValid = false;
                return;
            }

            IsValid = true;
            Value = input;
        }

        public override object Clone()
        {

            return new SelectParameterModel()
            {
                StringValue = this.StringValue,
                _default = this._default,
                Value = this.Value,
                Options = Options.Select(a => (string) a.Clone()).ToList(),
                IsValid = this.IsValid,
                IsCustom = this.IsCustom,
                ErrorMessage = this.ErrorMessage,
                FullName = this.FullName
            };
        }
    }
}