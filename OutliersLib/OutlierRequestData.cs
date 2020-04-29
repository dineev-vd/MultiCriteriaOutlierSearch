﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace OutliersLib
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
            foreach (var alg in Algorithms)
            {
                responseList.Add(await Interaction.GetResponse(alg, Values));
            }

            return responseList;
        }

        async public Task<List<ModuleResponse>> FetchCombinations(
            List<ModuleResponse> algResponses)
        {
            var weightsList = new List<List<double>>();
            foreach (var resp in algResponses)
            {
                if (resp.Status != "OK")
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