using System.Collections.Generic;

namespace OutliersLib
{
    public class Parameter
    {
        public string Type { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public object Default { get; set; }
        public List<string> Data { get; set; }
    }
}