using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace OutliersLib
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class OutlierRequestData
    {
        public virtual Dictionary<string, Module> Algorithms { get; set; }
        public virtual Dictionary<string, Module> Combinations { get; set; }
        public List<double> Values { get; set; }

        public OutlierRequestData()
        {
            Algorithms = new Dictionary<string, Module>();
            Combinations = new Dictionary<string, Module>();
            Values = new List<double>();
        }

        async public Task<Dictionary<string, ModuleResponse>> FetchAlgorithms()
        {
            var responseList = new Dictionary<string, ModuleResponse>();
            foreach (var alg in Algorithms)
            {
                responseList.Add(alg.Key, await Interaction.GetResponse(alg.Key, alg.Value, Values));
            }

            return responseList;
        }

        async public Task<Dictionary<string, ModuleResponse>> FetchCombinations(
            Dictionary<string, ModuleResponse> algResponses)
        {
            var weightsList = new List<List<double>>();
            foreach (var resp in algResponses)
            {
                weightsList.Add((List<double>) resp.Value.Data);
            }

            var responseList = new Dictionary<string, ModuleResponse>();
            foreach (var comb in Combinations)
            {
                responseList.Add(comb.Key, await Interaction.GetResponse(comb.Key,comb.Value,weightsList));
            }

            return responseList;
        }
    }
}