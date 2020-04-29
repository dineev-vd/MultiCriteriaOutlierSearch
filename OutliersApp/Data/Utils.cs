using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using OutliersLib;

namespace OutliersApp.Data
{
    public static class Utils
    {
        public static bool IsValidDouble(object value)
        {
            if (!(value is double))
            {
                return false;
            }

            return true;
        }
        public static bool IsValidDouble(object value, Parameter pars)
        {
            if (!(value is double))
            {
                return false;
            }

            if (pars.Max >= 0 && (double) value > pars.Max)
            {
                return false;
            }

            if (pars.Min >= 0 && (double) value < pars.Min)
            {
                return false;
            }

            return true;
        }

        public static void ChangeWeight(ChangeEventArgs args, Module Mod)
        {
            if ((string) args.Value == string.Empty)
            {
                Mod.Weight = 1;
                return;
            }
            
            try
            {
                Mod.Weight = double.Parse((string) args.Value, CultureInfo.InvariantCulture);
            }
            catch
            {
                Mod.Weight = -1;
            }
        }
        
        public static void ChangeValue(ChangeEventArgs value, KeyValuePair<string, Parameter> o, Module Mod)
        {
            if ((string) value.Value == String.Empty)
            {
                Mod.Params[o.Key] = o.Value.Default;
                return;
            }

            try
            {
                Mod.Params[o.Key] = double.Parse((string)value.Value, CultureInfo.InvariantCulture);
            }
            catch
            {
                Mod.Params[o.Key] = o.Value.Default;
            }
        }

        public static List<SettingsForm> ConvertConfigSettings(Dictionary<string, Parameter> configParameters)
        {
            var result = new List<SettingsForm>();
            foreach (var parameter in configParameters)
            {
                if (parameter.Value.Type == "double")
                {
                    result.Add(new DoubleSettingForm(parameter.Key, parameter.Value.Min, parameter.Value.Max, (double)parameter.Value.Default));
                    continue;
                }

                if (parameter.Value.Type == "select")
                {
                    result.Add(new SelectSettingForm(parameter.Key, parameter.Value.Data, (string)parameter.Value.Default));
                    continue;
                }

                if (parameter.Value.Type == "bool")
                {
                    result.Add(new BoolSettingForm(parameter.Key, (bool)parameter.Value.Default));
                    continue;
                }

                throw new NotImplementedException(
                    $"Conversion error: SettingsForm with type {parameter.Value.Type} is not supported");
            }

            return result;
        }
        
        public static List<PredefinedModule> ConvertConfig(Dictionary<string, ModuleSettings> configModulesDict)
        {
            var result = new List<PredefinedModule>();
            foreach (var configModule in configModulesDict)
            {
                result.Add(new PredefinedModule(configModule.Key, configModule.Value.CoolName, ConvertConfigSettings(configModule.Value.Parameters)));
            }

            return result;
        }
    }
}