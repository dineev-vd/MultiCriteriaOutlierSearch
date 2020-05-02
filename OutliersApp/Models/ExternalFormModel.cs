﻿using System.Collections.Generic;
using OutliersApp.Models.Parameters;
using OutliersLib;

namespace OutliersApp.Models
{
    public class ExternalFormModel : BaseFormModel
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public List<ParameterModelBase> Settings { get; set; }

        public override bool IsValid
        {
            get
            {
                foreach (var item in Settings)
                {
                    if (!item.IsValid)
                        return false;
                }

                return true;
            }
        }

        public ExternalFormModel()
        {
            Name = string.Empty;
            Uri = string.Empty;
            Settings = new List<ParameterModelBase>();
        }

        public IncomingModuleRequest ToModule()
        {
            var result = new IncomingModuleRequest();
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
}