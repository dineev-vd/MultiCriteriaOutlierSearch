using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace OutliersLib
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class OutlierRequestData
    {
        public List<IncomingModuleRequest> Algorithms { get; set; }
        public List<IncomingModuleRequest> Combinations { get; set; }
        public double[,] Values { get; set; }

        public OutlierRequestData()
        {
            Algorithms = new List<IncomingModuleRequest>();
            Combinations = new List<IncomingModuleRequest>();
            Values = new double[0,0];
        }

        async public Task<List<ModuleResponse>> FetchAlgorithms()
        {
            var responseList = new List<ModuleResponse>();
            foreach (var alg in Algorithms)
            {
                var response = await Interaction.GetResponse(alg, Values);
                if (alg.Weight != 1)
                {
                    response.Data = new List<double>(response.Data.Select(x => x * alg.Weight));
                }

                responseList.Add(response);
            }

            return responseList;
        }

        async public Task<List<ModuleResponse>> FetchCombinations(
            List<ModuleResponse> algResponses)
        {
            var weightsList = new List<List<double>>();
            // Алгоритмы, вернувшие ошибку отбрасываются
            foreach (var resp in algResponses)
            {
                if (resp.Status != (int)HttpStatusCode.OK)
                {
                    continue;
                }

                weightsList.Add(resp.Data);
            }

            var responseList = new List<ModuleResponse>();
            foreach (var comb in Combinations)
            {
                responseList.Add(await Interaction.GetResponse(comb, weightsList));
            }

            return responseList;
        }
    }
}