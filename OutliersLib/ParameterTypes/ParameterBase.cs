using System;
using Newtonsoft.Json;

namespace OutliersLib.ParameterTypes
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public abstract class ParameterBase : ICloneable
    {
        [JsonIgnore] public bool IsValid { get; set; }
        [JsonIgnore] public bool IsCustom { get; set; }
        [JsonIgnore] public string ErrorMessage { get; set; }
        public string FullName { get; set; }
        
        [JsonIgnore]
        public string StringValue { get; set; }
        
        public abstract void SetValue(string input);

        public ParameterBase()
        {
            FullName = string.Empty;
            ErrorMessage = string.Empty;
            StringValue = string.Empty;
            IsValid = true;
        }


        // public abstract object Value { get; set; }
        public abstract object Clone();

        public abstract object GetValue();
    }
}