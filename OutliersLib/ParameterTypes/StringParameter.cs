namespace OutliersLib.ParameterTypes
{
    public class StringParameter : ParameterBase
    {
        public string Default { get; set; }
        public string Value { get; set; }
        public override string DefaultToString()
        {
            return Default.ToString();
        }
    }
}