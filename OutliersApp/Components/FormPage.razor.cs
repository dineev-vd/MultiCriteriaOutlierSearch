using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using OutliersApp.Models;
using OutliersLib;
using Utils = OutliersApp.Models.Utils;

namespace OutliersApp.Components
{
    public partial class FormPage
    {
        Form UserInstance { get; set; }
        Config Config { get; set; }
        string str { get; set; }
        string req { get; set; }
        string values { get; set; } = "";
        Responses responses { get; set; }
        bool TextValid { get; set; }
        bool IsValid { get; set; }

        List<PredefinedModule> PredefinedAlgorithms { get; set; }
        List<PredefinedModule> PredefinedCombinations { get; set; }

        protected override async Task OnInitializedAsync()
        {
            IsValid = true;
            responses = new Responses();
            Config = new Config();
            UserInstance = new Form();
            await FetchData();
            PredefinedAlgorithms = Utils.ConvertConfig(Config.Algorithms);
            PredefinedCombinations = Utils.ConvertConfig(Config.Combinations);
            await base.OnInitializedAsync();
        }

        public async Task FetchData()
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/config/");
            var response = await client.SendAsync(request);
            Config = JsonConvert.DeserializeObject<Config>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
            StateHasChanged();
        }

        public void OnInput(ChangeEventArgs eventArgs)
        {
        }

        public void OnClick()
        {
            UserInstance.Algorithms.Add(new ModuleFormModel(PredefinedAlgorithms));
        }

        public void OnClickCombs()
        {
            UserInstance.Combinations.Add(new ModuleFormModel(PredefinedCombinations));
        }

        public async void OnSendClick()
        {
            double[,] result;
            try
            {
                result = Utils.ParseInput(UserInstance.ValuesString);
            }
            catch
            {
                TextValid = false;
                return;
            }

            //Обнуляем для текста
            responses = new Responses();
            
            IsValid = UserInstance.Check();

            if (!IsValid)
                return;

            UserInstance.Values = result;

            Send();
            
            StateHasChanged();
        }

        public async void Send()
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/api/");
            request.Content = new StringContent(JsonConvert.SerializeObject(UserInstance.ToRequestData()), System.Text.Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);
            var algsandcombs = JsonConvert.DeserializeObject<Responses>(await response.Content.ReadAsStringAsync());
            responses = algsandcombs;
            str = await response.Content.ReadAsStringAsync();
            req = await request.Content.ReadAsStringAsync();
            Console.WriteLine(str);
            Console.WriteLine(req);
            StateHasChanged();
        }

        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class Responses
        {
            public List<ModuleResponse> AlgResponses { get; set; }
            public List<ModuleResponse> CombResponses { get; set; }

            public Responses()
            {
                AlgResponses = new List<ModuleResponse>();
                CombResponses = new List<ModuleResponse>();
            }
            
        }
    }
}