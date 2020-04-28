using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;

//TODO Проверка конфигурации модулей на корректность

namespace OutliersLib
{
    public static class Configuration
    {
        public async static Task<Config> Read()
        {
            Config dict;
            dict = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
            foreach(var m in dict.Algorithms)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, m.Value.Uri + "/config");
                var response = await httpClient.SendAsync(request);
                if(response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    continue;
                }

                m.Value.Options = JsonConvert.DeserializeObject<Dictionary<string, Parameters>>(await response.Content.ReadAsStringAsync());
            }

            return dict;
        }

        public static HttpClient httpClient = new HttpClient();
    }
}
