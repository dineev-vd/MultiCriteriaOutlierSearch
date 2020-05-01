namespace OutliersApp.Models.Parameters
{
    public class BoolParameterModel : ParameterModelBase
    {
        public bool Default { get; set; }
        public bool Value { get; set; }

        public BoolParameterModel(string name, bool def) : base(name)
        {
            Default = def;
            Value = Default;
        }
    }
}