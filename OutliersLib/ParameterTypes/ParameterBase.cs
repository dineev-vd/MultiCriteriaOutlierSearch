using System;
using Newtonsoft.Json;

namespace OutliersLib.ParameterTypes
{
    public abstract class ParameterBase : ICloneable
    {
        public virtual bool IsValid { get; set; }
        public  bool IsCustom { get; set; }
        public abstract string DefaultToString();
        public string CoolName { get; set; }
        // public abstract object Value { get; set; }
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}