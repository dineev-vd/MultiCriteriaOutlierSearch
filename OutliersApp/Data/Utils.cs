using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using OutliersLib;

namespace OutliersApp.Data
{
    public static class Utils
    {
        public static bool IsValidDouble(object value, Parameters pars)
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

        public static void ChangeValue(ChangeEventArgs value, KeyValuePair<string, Parameters> o, NameModule Mod)
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

        public class InternalComponent
        {
            public Module Mod { get; set; }
            public ModuleSettings Selected { get; set; }

            public InternalComponent()
            {
                Mod = new Module() {Internal = true};
                Selected = new ModuleSettings();
            }
        }
    }
}