using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace outliers_lib
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class OutlierRequestData
    {
        public List<Module> Algorithms { get; set; }
        public List<Module> Combinations { get; set; }
        public List<double> Values { get; set; }
        public OutlierRequestData()
        {
            Algorithms = new List<Module>();
            Combinations = new List<Module>();
            Values = new List<double>();
        }
        async public Task<List<ModuleResponse>> FetchAlgorithms()
        {
            var responseList = new List<ModuleResponse>();
            foreach(var alg in Algorithms)
            {
                responseList.Add(await alg.GetResponse(Values));
            }

            return responseList;
        }

        async public Task<List<ModuleResponse>> FetchCombinations(List<ModuleResponse> algResponses)
        {
            var weightsList = new List<List<double>>();
            foreach (var resp in algResponses)
            {
                weightsList.Add((List<double>)resp.Data);
            }

            var responseList = new List<ModuleResponse>();
            foreach(var comb in Combinations)
            {
                responseList.Add(await comb.GetResponse(weightsList));
            }

            return responseList;
        }
    }
}
