namespace OutliersApp.Models.ParameterModels
{
    public class BoolParameterModel : ParameterModelBase
    {
        public override int Id { get; } = 4;
        public bool Default { get; set; }
        public bool Value { get; set; }

        public BoolParameterModel(string name,string coolName, bool def) : base(name,coolName)
        {
            Default = def;
            Value = Default;
            IsCustom = false;
        }

        public BoolParameterModel(string name,string coolName) : base(name, coolName)
        {
            Value = false;
            IsCustom = true;
        }

        public override object Clone()
        {
            return new BoolParameterModel(Name, CoolName, Default);
        }
    }
}