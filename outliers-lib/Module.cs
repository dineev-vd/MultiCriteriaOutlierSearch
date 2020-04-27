using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Net;

namespace outliers_lib
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Module
    {
        public string Name { get; set; }
        public bool Internal { get; set; }
        public string Uri { get; set; }
        public Dictionary<string, object> Params { get; set; }
        public Module()
        {
            Name = String.Empty;
            Internal = true;
            Uri = String.Empty;
            Params = new Dictionary<string, object>();
        }
        public async Task<HttpRequestMessage> MakeRequest(object data)
        {
            string uri;
            if (!Internal)
            {
                uri = Uri;
            }
            else
            {
                try
                {
                    var config = await Configuration.Read();
                    uri = config.Algorithms[Name].Uri;
                }
                catch
                {
                    throw new ArgumentException($"cant resolve internal name : {Name}");
                }
            }

            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonConvert.SerializeObject(new ModuleJson(data, Params)), Encoding.UTF8, "application/json");
            return request;
        }
        async public Task<ModuleResponse> GetResponse(object data)
        {
            HttpResponseMessage response;
            try
            {
                response = await Configuration.httpClient.SendAsync(await MakeRequest(data));
            }
            catch(Exception e)
            {
                return new ModuleResponse(Name,e.Message, null);
            }

            if(response.StatusCode != HttpStatusCode.OK)
            {
                return new ModuleResponse(Name,response.StatusCode.ToString(), null);
            }

            var moduleResponse = new ModuleResponse(Name,response.StatusCode.ToString(), JsonConvert.DeserializeObject<List<double>>(await response.Content.ReadAsStringAsync()));
            return moduleResponse;
        }
    }
}
