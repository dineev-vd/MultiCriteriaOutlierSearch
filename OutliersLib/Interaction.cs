using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OutliersLib
{
    public static class Interaction
    {
        public static HttpRequestMessage MakeRequest(IncomingModuleRequest incomingModuleRequest,
            object data)
        {
            string uri = string.Empty;
            if (!incomingModuleRequest.Internal)
            {
                uri = incomingModuleRequest.Uri;

                if (uri == string.Empty)
                {
                    throw new ArgumentException($"Ссылка на внешний модуль пустая");
                }
            }
            else
            {
                var config = Utils.Config;

                if (config.Algorithms.ContainsKey(incomingModuleRequest.Name))
                {
                    uri = config.Algorithms[incomingModuleRequest.Name].Uri;
                }

                if (config.Combinations.ContainsKey(incomingModuleRequest.Name))
                {
                    uri = config.Combinations[incomingModuleRequest.Name].Uri;
                }

                if (uri == string.Empty)
                {
                    throw new ArgumentException($"Не удалось найти информацию о модуле с именем: {incomingModuleRequest.Name}");
                }
            }


            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(
                JsonConvert.SerializeObject(new OutcomingModuleRequest(data, incomingModuleRequest.Params)),
                Encoding.UTF8, "application/json");
            return request;
        }

        async static public Task<ModuleResponse> GetResponse(IncomingModuleRequest incomingModuleRequest, object data)
        {
            HttpResponseMessage response;
            try
            {
                Logger.Push($"request sent {incomingModuleRequest.Name}");
                response = await Utils.Client.SendAsync(MakeRequest(incomingModuleRequest, data));
                Logger.Push($"response from {incomingModuleRequest.Name}");
            }
            catch (ArgumentException e)
            {
                return new ModuleResponse(incomingModuleRequest.Name, 500, e.Message, null);
            }
            catch
            {
                return new ModuleResponse(incomingModuleRequest.Name, 500,
                    "При запросе от api к внутреннему модулю произошла ошибка", null);
            }

            ModuleResponse moduleResponse = new ModuleResponse();
            try
            {
                var result =
                    JsonConvert.DeserializeObject<ModuleResponse>(await response.Content.ReadAsStringAsync());
                result.Name = incomingModuleRequest.Name;
                result.Status = (int)response.StatusCode;
                return result;
            }
            catch
            {
                return new ModuleResponse(incomingModuleRequest.Name, 500,"Ответ модуля в неверном формате", null);
            }
        }
    }
}

