using System.Collections.Generic;
using Newtonsoft.Json;

namespace OutliersLib
{
    /// <summary>
    /// Ответ модуля на запрос
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ModuleResponse
    {
        public string Name { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public List<double> Data { get; set; }

        public ModuleResponse(string name, int status,string message, List<double> data)
        {
            Name = name;
            Status = status;
            Message = message;
            Data = data;
        }

        public ModuleResponse()
        {
            Name = string.Empty;
            Status = -1;
            Message = string.Empty;
            Data = new List<double>();
        }
    }
}