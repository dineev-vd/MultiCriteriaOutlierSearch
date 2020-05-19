using Newtonsoft.Json;
using OutliersApp.Models;
using OutliersLib;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using OutliersApp.Components;

namespace OutliersApp.Pages
{
    public partial class FormPage
    {
        FormModel UserInstance { get; set; }
        Config Config { get; set; }
        Responses Responses { get; set; }
        bool IsValid { get; set; }
        bool IsLoading { get; set; }
        bool ConfigNotLoaded { get; set; }
        
        private string[,] ForTable { get; set; }

        List<PredefinedModule> PredefinedAlgorithms { get; set; }
        List<PredefinedModule> PredefinedCombinations { get; set; }

        protected override async Task OnInitializedAsync()
        {
            IsValid = true;
            Responses = new Responses();
            Config = new Config();
            UserInstance = new FormModel();
            IsLoading = false;
            ConfigNotLoaded = false;
            ForTable = new string[0,0];
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

            PredefinedAlgorithms = Converters.ConvertConfig(Config.Algorithms);
            PredefinedCombinations = Converters.ConvertConfig(Config.Combinations);
            await base.OnInitializedAsync();
        }

        public async Task FetchData()
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, Converters.ApiAdress);
            var response = await client.SendAsync(request);
            Config = JsonConvert.DeserializeObject<Config>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
        }

        public void OnAddAlgorithm()
        {
            UserInstance.Algorithms.Add(new ModuleFormModel(PredefinedAlgorithms));
        }

        public void OnAddCombination()
        {
            UserInstance.Combinations.Add(new ModuleFormModel(PredefinedCombinations));
        }

        public async void OnSend()
        {
            //Обнуляем для текста
            Responses = new Responses();

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

        

        public string[,] ToTable()
        {
            int columnsCount = 1 + UserInstance.Values.GetLength(1);
            List<string> columnNames = new List<string>();
                
            foreach (var algResponse in Responses.AlgResponses)
            {
                if (algResponse.Status != 200)
                {
                    continue;
                }

                if (Config.Algorithms.ContainsKey(algResponse.Name))
                {
                    columnNames.Add(Config.Algorithms[algResponse.Name].FullName);
                }
                else
                {
                    columnNames.Add(algResponse.Name);
                }
                
                columnsCount++;
            }

            foreach (var combResponse in Responses.CombResponses)
            {
                if (combResponse.Status != 200)
                {
                    continue;
                }
                
                if (Config.Combinations.ContainsKey(combResponse.Name))
                {
                    columnNames.Add(Config.Combinations[combResponse.Name].FullName);
                }
                else
                {
                    columnNames.Add(combResponse.Name);
                }
                
                columnsCount++;
            }
            
            string[,] array = new string[UserInstance.Values.GetLength(0) + 1,columnsCount];
            
            // Заполняем колонки
            array[0, 0] = "#";

            for (int i = 1; i < 1 + UserInstance.Values.GetLength(1); i++)
            {
                array[0, i] = "Признак " + i;
            }
            
            for (int i = 0; i < columnNames.Count; i++)
            {
                array[0, i + 1 + UserInstance.Values.GetLength(1)] = columnNames[i];
            }
            
            
            
            // Заполняем значения
            for (int i = 1; i < UserInstance.Values.GetLength(0) + 1; i++)
            {
                array[i, 0] = i.ToString();
                for (int j = 1; j < UserInstance.Values.GetLength(1) + 1; j++)
                {
                    array[i, j] = UserInstance.Values[i - 1, j - 1].ToString();
                }
            }
            
            // Заполняем алгоритмы
            int curIndex = 1 + UserInstance.Values.GetLength(1);
            foreach (var alg in Responses.AlgResponses)
            {
                if (alg.Status != 200)
                {
                    continue;
                }

                for (int i = 1; i < alg.Data.Count + 1; i++)
                {
                    array[i, curIndex] = alg.Data[i - 1].ToString();
                }

                curIndex++;
            }
            
            // Заполняем комбинации
            foreach (var comb in Responses.CombResponses)
            {
                if (comb.Status != 200)
                {
                    continue;
                }

                for (int i = 1; i < comb.Data.Count + 1; i++)
                {
                    array[i, curIndex] = comb.Data[i - 1].ToString();
                }

                curIndex++;
            }

            return array;
        }

}
}