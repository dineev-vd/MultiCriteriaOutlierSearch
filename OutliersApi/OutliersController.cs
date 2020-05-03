using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using System.IO;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using OutliersLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace outliers_api
{
    [Route("api/")]
    public class OutliersController : Controller
    {
        // POST /api/outliers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutlierRequestData data)
        {
            // Проверка на корректность входных данных в body
            if(data is null)
            {
                return BadRequest();
            }

            var algResponses = await data.FetchAlgorithms();
            var combResponses = await data.FetchCombinations(algResponses);

            return Ok(new {algResponses, combResponses});
        }
    }

    [Route("config/")]
    public class ConfigController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            Utils.Read();
            return Ok(Utils.Config);
        }
    }
}
