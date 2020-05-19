using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
            var responseTaskList = new List<Task<ModuleResponse>>();
            var responseList = new List<ModuleResponse>();
            foreach (var alg in Algorithms)
            {
                var responseTask = Interaction.GetResponse(alg, Values);
                responseTaskList.Add(responseTask);
            }

            responseList = (await Task.WhenAll(responseTaskList)).ToList();
            
            foreach (var response in responseList)
            {
                if (response.Status == 200 && response.Data.Count != Values.GetLength(0))
                {
                    response.Message = "Размер полученного массива не соответствует размеру массива входных данных";
                    response.Status = 500;
                }
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
            var responseTaskList = new List<Task<ModuleResponse>>();
            foreach (var comb in Combinations)
            {
                var response = Interaction.GetResponse(comb, new {values, weights});
                responseTaskList.Add(response);
            }

            responseList = (await Task.WhenAll(responseTaskList)).ToList();
            
            foreach (var response in responseList)
            {
                if (response.Status == 200 && response.Data.Count != Values.GetLength(0))
                {
                    response.Message = "Размер полученного массива не соответствует размеру массива входных данных";
                    response.Status = 400;
                }
            }

            return responseList;
        }

        public void Check()
        {
            Config config = Utils.Config;
            Utils.Read();
            foreach (var algorithm in Algorithms)
            {
                if (!algorithm.Internal)
                {
                    continue;
                }

                if (!config.Algorithms.ContainsKey(algorithm.Name))
                {
                    throw new Exception("Не найден алгоритм " + algorithm.Name);
                }

                foreach (var param in algorithm.Params)
                {
                    if (!config.Algorithms[algorithm.Name].Parameters.ToDict().ContainsKey(param.Key))
                    {
                        continue;
                    }

                    config.Algorithms[algorithm.Name].Parameters.ToDict()[param.Key].SetValue(param.Value.ToString());
                    if (!config.Algorithms[algorithm.Name].Parameters.ToDict()[param.Key].IsValid)
                    {
                        throw new FormatException(config.Algorithms[algorithm.Name].Parameters.ToDict()[param.Key]
                            .ErrorMessage);
                    }
                }
            }

            foreach (var combination in Combinations)
            {
                if (!combination.Internal)
                {
                    continue;
                }

                if (!config.Combinations.ContainsKey(combination.Name))
                {
                    throw new Exception("Не найдена комбинация " + combination.Name);
                }

                foreach (var param in combination.Params)
                {
                    if (!config.Combinations[combination.Name].Parameters.ToDict().ContainsKey(param.Key))
                    {
                        continue;
                    }
                    
                    config.Combinations[combination.Name].Parameters.ToDict()[param.Key]
                        .SetValue(param.Value.ToString());
                    if (!config.Combinations[combination.Name].Parameters.ToDict()[param.Key].IsValid)
                    {
                        throw new FormatException(config.Combinations[combination.Name].Parameters.ToDict()[param.Key]
                            .ErrorMessage);
                    }
                }
            }
        }
    }
}