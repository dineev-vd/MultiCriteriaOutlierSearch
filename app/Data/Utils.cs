using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using outliers_lib;
namespace app.Data
{
    public static class Utils
    {
        public static bool IsValidDouble(object value, Parameters pars)
        {
            if (!(value is double))
            {
                return false;
            }

            if (pars.Max >= 0 && (double)value > pars.Max)
            {
                return false;
            }

            if (pars.Min >= 0 && (double)value < pars.Min)
            {
                return false;
            }

            return true;
        }
        
        public static void ChangeValue(ChangeEventArgs value, KeyValuePair<string, Parameters> o, Module Mod)
        {
            if((string)value.Value == String.Empty)
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
                Mod.Params[o.Key] = "error";

            }
        }
    }
}