using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OutliersLib
{
    public static class Interaction
    {
        public static async Task<HttpRequestMessage> MakeRequest(string name, Module module, object data)
        {
            string uri = string.Empty;
            if (!module.Internal)
            {
                uri = module.Uri;
            }
            else
            {
                var config = await Configuration.Read();
                if (config.Algorithms.ContainsKey(name))
                {
                    uri = config.Algorithms[name].Uri;
                }

                if (config.Combinations.ContainsKey(name))
                {
                    uri = config.Combinations[name].Uri;
                }
            }

            if (uri == string.Empty)
            {
                throw new ArgumentException($"cant resolve internal name : {name}");
            }

            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonConvert.SerializeObject(new ModuleJson(data, module.Params)),
                Encoding.UTF8, "application/json");
            return request;
        }

        async static public Task<ModuleResponse> GetResponse(string name, Module module, object data)
        {
            HttpResponseMessage response;
            try
            {
                response = await Configuration.httpClient.SendAsync(await MakeRequest(name, module, data));
            }
            catch (Exception e)
            {
                return new ModuleResponse(e.Message, null);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new ModuleResponse(response.StatusCode.ToString(), null);
            }

            var moduleResponse = new ModuleResponse(response.StatusCode.ToString(),
                JsonConvert.DeserializeObject<List<double>>(await response.Content.ReadAsStringAsync()));
            return moduleResponse;
        }
    }
}