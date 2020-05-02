namespace OutliersApp.Models.ParameterModels
{
    public class StringParameterModel : ParameterModelBase
    {
        public override int Id { get; } = 3;
        public string Default { get; set; }
        public string Value { get; set; }

        public StringParameterModel(string name,string coolName, string def) : base(name, coolName)
        {
            Default = def;
            Value = Default;
            IsCustom = false;
        }

        public StringParameterModel(string name, string coolName) : base(name, coolName)
        {
            Default = string.Empty;
            Value = Default;
            IsCustom = true;
        }

        public override object Clone()
        {
            return new StringParameterModel(Name, CoolName, Default);
        }
    }
}