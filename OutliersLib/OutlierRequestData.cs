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
            Values = new double[0, 0];
        }

        async public Task<List<ModuleResponse>> FetchAlgorithms()
        {
            var responseList = new List<ModuleResponse>();
            foreach (var alg in Algorithms)
            {
                var response = await Interaction.GetResponse(alg, Values);
                responseList.Add(response);
            }

            return responseList;
        }

        async public Task<List<ModuleResponse>> FetchCombinations(
            List<ModuleResponse> algResponses)
        {
            var values = new List<List<double>>();
            var weights = new List<double>();
            // Алгоритмы, вернувшие ошибку отбрасываются
            for (int i = 0; i < algResponses.Count; i++)
            {
                if (algResponses[i].Status != (int) HttpStatusCode.OK)
                {
                    continue;
                }

                weights.Add(Algorithms[i].Weight);
                values.Add(algResponses[i].Data);
            }

            var responseList = new List<ModuleResponse>();
            foreach (var comb in Combinations)
            {
                responseList.Add(await Interaction.GetResponse(comb, new {values, weights}));
            }

            return responseList;
        }
    }
}