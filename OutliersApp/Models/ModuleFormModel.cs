using System.Collections.Generic;
using OutliersLib;

namespace OutliersApp.Models
{
    public class ModuleFormModel
    {
        public bool IsShown { get; set; }
        public bool IsInternal { get; set; }
        public InternalFormModel Internal { get; set; }
        public ExternalFormModel External { get; set; }
        public double Weight { get; set; }
        public bool WeightIsValid { get; set; }

        public ModuleFormModel(List<PredefinedModule> internalModulesList)

        {
            IsShown = false;
            IsInternal = true;
            Internal = new InternalFormModel(internalModulesList);
            External = new ExternalFormModel();
            Weight = 1;
            WeightIsValid = true;
        }

        public Module GetCurrent()
        {
            var module = IsInternal ? Internal.ToModule() : External.ToModule();
            module.Weight = Weight;
            return module;
        }
    }
}