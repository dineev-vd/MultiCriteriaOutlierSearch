using OutliersLib;
using OutliersLib.ParameterTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace OutliersApp.Models
{
    public static class Converters
    {
        static string _apiAdress;
        public static string ApiAdress
        {
            get
            {
                if (_apiAdress is null)
                {
                    using (var fs = File.OpenText("api_adress.txt"))
                    {
                        _apiAdress = fs.ReadLine();
                    }
                }

                return _apiAdress;
            }
        }
        
        public static Dictionary<string, ParameterBase> ConvertConfigSettings(Parameters configParameters)
        {
            var result = new Dictionary<string, ParameterBase>();
            foreach (var parameter in configParameters.ToDict())
            {
                if (parameter.Value is null) continue;
                result.Add(parameter.Key, parameter.Value);
            }

            return result;
        }

        public static List<PredefinedModule> ConvertConfig(Dictionary<string, InternalModuleConfig> configModulesDict)
        {
            var result = new List<PredefinedModule>();
            foreach (var configModule in configModulesDict)
            {
                result.Add(new PredefinedModule(configModule.Key, configModule.Value.FullName,
                    ConvertConfigSettings(configModule.Value.Parameters)));
            }

            return result;
        }

        public static double[,] ParseInput(string input)
        {
            string[] rows = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            double[,] result = new double[rows.Length, rows.First().Split(';').Length];


            for (int i = 0; i < result.GetLength(0); i++)
            {
                string[] doubleStrings = rows[i].Split(';');
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = double.Parse(doubleStrings[j].Replace(',','.'), new NumberFormatInfo { NumberDecimalSeparator = "." });
                }
            }

            return result;
        }
    }
}