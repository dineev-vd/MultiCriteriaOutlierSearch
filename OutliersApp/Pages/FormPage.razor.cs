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
using OutliersApp.Components;

namespace OutliersApp.Pages
{
    public partial class FormPage
    {
        Form UserInstance { get; set; }
        Config Config { get; set; }
        string str { get; set; }
        string req { get; set; }
        Responses responses { get; set; }
        bool IsValid { get; set; }
        bool IsLoading { get; set; }
        bool ConfigNotLoaded { get; set; }

        List<PredefinedModule> PredefinedAlgorithms { get; set; }
        List<PredefinedModule> PredefinedCombinations { get; set; }

        protected override async Task OnInitializedAsync()
        {
            IsValid = true;
            responses = new Responses();
            Config = new Config();
            UserInstance = new Form();
            IsLoading = false;
            ConfigNotLoaded = false;
            try
            {
                await FetchData();
            }
            catch
            {
                ConfigNotLoaded = true;
            }

            if (Config is null)
            {
                Config = new Config();
            }
            
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
            //Обнуляем для текста
            responses = new Responses();

            if (!UserInstance.IsValid)
            {
                IsValid = false;
                return;
            }

            IsValid = true;

            IsLoading = true;
            try
            {
                await Send();
            }
            catch
            {
                // error
                IsLoading = false;
                StateHasChanged();
                return;
            }
            
            IsLoading = false;
            StateHasChanged();
        }

        public async Task Send()
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/api/");
            request.Content = new StringContent(JsonConvert.SerializeObject(UserInstance.ToRequestData()),
                System.Text.Encoding.UTF8, "application/json");
            var response = new HttpResponseMessage();

            response = await client.SendAsync(request);


            var algsandcombs = JsonConvert.DeserializeObject<Responses>(await response.Content.ReadAsStringAsync());
            if (algsandcombs is null)
            {
                responses = new Responses();
            }
            else
            {
                responses = algsandcombs;
            }

            str = await response.Content.ReadAsStringAsync();
            req = await request.Content.ReadAsStringAsync();
            Console.WriteLine(str);
            Console.WriteLine(req);
            StateHasChanged();
        }

        
    }
}