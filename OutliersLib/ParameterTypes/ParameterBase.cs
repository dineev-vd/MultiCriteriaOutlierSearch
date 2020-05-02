using System;
using Newtonsoft.Json;

namespace OutliersLib.ParameterTypes
{
    public abstract class ParameterBase : ICloneable
    {
        public string CoolName { get; set; }
        // public abstract object Value { get; set; }
        public abstract object Clone();
    }
}