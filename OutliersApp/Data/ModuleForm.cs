using System;
using System.Collections.Generic;
using System.Linq;
using OutliersLib;

namespace OutliersApp.Data
{
    public class ModuleForm
    {
        public bool IsInternal { get; set; }
        public InternalForm Internal { get; set; }
        public ExternalForm External { get; set; }
        public double Weight { get; set; }

        public ModuleForm(List<PredefinedModule> internalModulesList)
        {
            IsInternal = true;
            Internal = new InternalForm(internalModulesList);
            External = new ExternalForm();
        }

        public Module GetCurrent()
        {
            var module = IsInternal ? Internal.ToModule() : External.ToModule();
            module.Weight = Weight;
            return module;
        }
    }

    public class ExternalForm
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public List<SettingsForm> Settings { get; set; }

        public ExternalForm()
        {
            Name = string.Empty;
            Uri = string.Empty;
            Settings = new List<SettingsForm>();
        }

        public Module ToModule()
        {
            var result = new Module();
            result.Internal = false;
            result.Uri = Uri;
            result.Name = Name;
            result.Params = SettingsToDict();
            return result;
        }

        public Dictionary<string, object> SettingsToDict()
        {
            var result = new Dictionary<string, object>();
            foreach (var setting in Settings)
            {
                result.Add(setting.Name, setting.Value);
            }

            return result;
        }
    }
    
    public class InternalForm
    {
        public PredefinedModule Selected { get; set; }
        public List<PredefinedModule> InternalModulesList { get; set; }

        public InternalForm(List<PredefinedModule> internalModulesList)
        {
            Selected = new PredefinedModule();
            InternalModulesList = new List<PredefinedModule>(internalModulesList.Select(x => x.Clone() as PredefinedModule));;
        }
        
        public Dictionary<string, object> SettingsToDict()
        {
            var result = new Dictionary<string, object>();
            foreach (var setting in Selected.Settings)
            {
                result.Add(setting.Name, setting.Value);
            }

            return result;
        }

        public Module ToModule()
        {
            var result = new Module();
            result.Internal = true;
            result.Name = Selected.Name;
            result.Params = SettingsToDict();
            return result;
        }
    }

    public class PredefinedModule : ICloneable
    {
        public string Name { get; set; }
        public string CoolName { get; set; }
        public List<SettingsForm> Settings { get; set; }

        public PredefinedModule()
        {
            Name = string.Empty;
            CoolName = string.Empty;
            Settings = new List<SettingsForm>();
        }

        public PredefinedModule(string name, string coolName, List<SettingsForm> settings)
        {
            Name = name;
            CoolName = coolName;
            Settings = settings;
        }

        public object Clone()
        {
            return new PredefinedModule(Name, CoolName,
                new List<SettingsForm>(Settings.Select(x => x.Clone() as SettingsForm)));
        }
    }

    public abstract class SettingsForm : ICloneable
    {
        public object Value
        {
            get
            {
                if (this is DoubleSettingForm)
                {
                    return ((DoubleSettingForm) this).Value;
                }
                
                if (this is SelectSettingForm)
                {
                    return ((SelectSettingForm) this).Value;
                }
                
                if (this is BoolSettingForm)
                {
                    return ((BoolSettingForm) this).Value;
                }

                throw new NotImplementedException($"This type of SettingsForm ({this.GetType().Name}) is not supported");
            }
        }

        public bool IsValid { get; set; }
        public string Name { get; set; }

        public SettingsForm(string name)
        {
            Name = name;
            IsValid = true;
        }

        public object Clone()
        {
            if (this is DoubleSettingForm)
            {
                var current = this as DoubleSettingForm;
                return new DoubleSettingForm(current.Name, current.Min, current.Max, current.Default);
            }

            if (this is SelectSettingForm)
            {
                var current = this as SelectSettingForm;
                return new SelectSettingForm(current.Name, current.Options, current.Default);
            }

            if (this is BoolSettingForm)
            {
                var current = this as BoolSettingForm;
                return new BoolSettingForm(current.Name, current.Default);
            }
            
            throw new NotImplementedException($"Type {this.GetType()} cloning is not supported");
        }
    }

    public class DoubleSettingForm : SettingsForm
    {
        public double _value;
        public double Min { get; set; }
        public double Max { get; set; }
        public double Default { get; set; }
        public double Value
        {
            get { return _value;}
            set
            {
                if (!(Min <= value && value <= Max))
                {
                    throw new ArgumentOutOfRangeException();
                }

                _value = value;
            }
        }

        public DoubleSettingForm(string name, double min, double max, double def) : base(name)
        {
            Min = min;
            Max = max;
            Default = def;
            Value = Default;
        }
    }

    public class SelectSettingForm : SettingsForm
    {
        public string Value { get; set; }
        public string Default { get; set; }
        public List<string> Options { get; set; }

        public SelectSettingForm(string name, List<string> options, string def) : base(name)
        {
            Options = options;
            Default = def;
            Value = Default;
        }
    }

    public class BoolSettingForm : SettingsForm
    {
        public bool Default { get; set; }
        public bool Value { get; set; }

        public BoolSettingForm(string name, bool def) : base(name)
        {
            Default = def;
            Value = Default;
        }
    }
}