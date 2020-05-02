//using OutliersApp.Models.Parameters;
using OutliersLib;
using OutliersLib.ParameterTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace OutliersApp.Models
{
    public static class Utils
    {
        public static Dictionary<string, ParameterBase> ConvertConfigSettings(OutliersLib.ParameterTypes.Parameters configParameters)
        {
            var result = new Dictionary<string, ParameterBase>();
            foreach (var parameter in configParameters)
            {
                result.Add(parameter.Key, parameter.Value);
            }

            return result;
        }

        public static List<PredefinedModule> ConvertConfig(Dictionary<string, InternalModuleConfig> configModulesDict)
        {
            var result = new List<PredefinedModule>();
            foreach (var configModule in configModulesDict)
            {
                result.Add(new PredefinedModule(configModule.Key, configModule.Value.CoolName,
                    ConvertConfigSettings(configModule.Value.Parameters)));
            }

            return result;
        }

        public static double[,] ParseInput(string input)
        {
            string[] rows = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            double[,] result = new double[rows.Length, rows.First().Split(',').Length];


            for (int i = 0; i < result.GetLength(0); i++)
            {
                string[] doubleStrings = rows[i].Split(',');
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = double.Parse(doubleStrings[j], new NumberFormatInfo { NumberDecimalSeparator = "." });
                }
            }

            return result;
        }
    }
}