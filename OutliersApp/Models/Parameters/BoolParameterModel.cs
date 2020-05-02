namespace OutliersApp.Models.Parameters
{
    public class BoolParameterModel : ParameterModelBase
    {
        public override int Id { get; set; } = 4;
        public bool Default { get; set; }
        public bool Value { get; set; }

        public BoolParameterModel(string name, bool def) : base(name)
        {
            Default = def;
            Value = Default;
            IsCustom = false;
        }

        public BoolParameterModel(string name) : base(name)
        {
            Value = false;
            IsCustom = true;
        }
    }
}