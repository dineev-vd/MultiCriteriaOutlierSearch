﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using OutliersLib.ParameterTypes;

//TODO Проверка конфигурации модулей на корректность

namespace OutliersLib
{
    public static class Utils
    {
        const string ConfigPath = "config.json";
        private const string ConfigAdressAppend = "/config/";
        public static HttpClient httpClient = new HttpClient();
        public static Config Config = new Config();

        /// <summary>
        /// Считывает конфигурацию
        /// </summary>
        /// <returns></returns>
        public static void Read()
        {
            Config config;
            
            // Считывание файла конфигурации
            using(var fs = File.OpenText(ConfigPath))
            {
                config = JsonConvert.DeserializeObject<Config>(fs.ReadToEnd());
            }
            
            // Опрашивание алгоритмов для сбора параметров
            FillParameters(config.Algorithms).Wait();
            FillParameters(config.Combinations).Wait();
            
            Config = config;
        }

        /// <summary>
        /// Заполняет параметры в указанном массиве конфигураций, опрашивая модули
        /// </summary>
        /// <param name="moduleConfigs"></param>
        /// <returns></returns>
        public static async Task FillParameters(Dictionary<string, InternalModuleConfig> moduleConfigs)
        {
            foreach(var config in moduleConfigs)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, config.Value.Uri + ConfigAdressAppend);
                var response = await httpClient.SendAsync(request);
                if(response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    continue;
                }

                try
                {
                    var bytes = await response.Content.ReadAsByteArrayAsync();
                    var str = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    config.Value.Parameters = JsonConvert.DeserializeObject<Parameters>(str);
                }
                catch
                {
                    //TODO:Log => $"Произошла ошибка при получении параметров для модуля {algorithm.Key}");
                }
            }
        }
    }
}
