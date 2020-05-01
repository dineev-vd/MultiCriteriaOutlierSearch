using System.Collections.Generic;
using System.Linq;
using OutliersLib;

namespace OutliersApp.Models
{
    public class InternalFormModel
    {
        public PredefinedModule Selected { get; set; }
        public List<PredefinedModule> InternalModulesList { get; set; }

        public InternalFormModel(List<PredefinedModule> internalModulesList)
        {
            Selected = new PredefinedModule();
            InternalModulesList = new List<PredefinedModule>(internalModulesList.Select(x => x.Clone() as PredefinedModule));
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
}