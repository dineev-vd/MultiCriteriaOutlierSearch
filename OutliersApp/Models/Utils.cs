using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Components;
using OutliersApp.Models.Parameters;
using OutliersLib;

namespace OutliersApp.Models
{
    public static class Utils
    {
       public static List<ParameterModelBase> ConvertConfigSettings(Dictionary<string, Parameter> configParameters)
        {
            var result = new List<ParameterModelBase>();
            foreach (var parameter in configParameters)
            {
                if (parameter.Value.Type == "double")
                {
                    result.Add(new DoubleParameterModel(parameter.Key,parameter.Value.CoolName,  parameter.Value.Min, parameter.Value.Max,
                        (double) parameter.Value.Default));
                    continue;
                }

                if (parameter.Value.Type == "select")
                {
                    result.Add(new SelectParameterModel(parameter.Key, parameter.Value.CoolName,parameter.Value.Data,
                        (string) parameter.Value.Default));
                    continue;
                }

                if (parameter.Value.Type == "bool")
                {
                    result.Add(new BoolParameterModel(parameter.Key,parameter.Value.CoolName, (bool) parameter.Value.Default));
                    continue;
                }
                
                if (parameter.Value.Type == "string")
                {
                    result.Add(new StringParameterModel(parameter.Key, parameter.Value.CoolName,(string) parameter.Value.Default));
                    continue;
                }
                
                if (parameter.Value.Type == "int")
                {
                    result.Add(new IntParameterModel(parameter.Key, parameter.Value.CoolName,(int)parameter.Value.Min,(int)parameter.Value.Max,(int) parameter.Value.Default));
                    continue;
                }

                throw new NotImplementedException(
                    $"Conversion error: SettingsForm with type {parameter.Value.Type} is not supported");
            }

            result.Sort();
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
                    result[i, j] = double.Parse(doubleStrings[j], new NumberFormatInfo {NumberDecimalSeparator = "."});
                }
            }

            return result;
        }
    }
}