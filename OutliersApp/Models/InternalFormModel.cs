using OutliersLib;
using System.Collections.Generic;
using System.Linq;

namespace OutliersApp.Models
{
    public class InternalFormModel : BaseFormModel
    {
        public override bool IsValid
        {
            get
            {
                if (Selected.Name == string.Empty)
                {
                    NotSelected = true;
                    return false;
                }

                foreach (var setting in Selected.Settings)
                {
                    if (!setting.Value.IsValid)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public bool NotSelected { get; set; }
        public PredefinedModule Selected { get; set; }
        public List<PredefinedModule> InternalModulesList { get; set; }

        public InternalFormModel(List<PredefinedModule> internalModulesList)
        {
            NotSelected = false;
            Selected = new PredefinedModule();
            InternalModulesList = new List<PredefinedModule>(internalModulesList.Select(x => x.Clone() as PredefinedModule));
        }

        public Dictionary<string, object> SettingsToDict()
        {
            var result = new Dictionary<string, object>();
            foreach (var setting in Selected.Settings)
            {
                result.Add(setting.Key, setting.Value.GetValue());
            }

            return result;
        }

        public IncomingModuleRequest ToModule()
        {
            var result = new IncomingModuleRequest();
            result.Internal = true;
            result.Name = Selected.Name;
            result.Params = SettingsToDict();
            return result;
        }
    }
}