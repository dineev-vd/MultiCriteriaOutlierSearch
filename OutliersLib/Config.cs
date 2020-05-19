﻿using System.Collections.Generic;
using Newtonsoft.Json;
 using Newtonsoft.Json.Serialization;

 namespace OutliersLib
{
    /// <summary>
    /// Конфигурация встроенных модулей
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Config
    {
        public Dictionary<string, InternalModuleConfig> Algorithms { get; set; }
        public Dictionary<string, InternalModuleConfig> Combinations { get; set; }
        public Config()
        {
            Algorithms = new Dictionary<string, InternalModuleConfig>();
            Combinations = new Dictionary<string, InternalModuleConfig>();
        }
    }
}
