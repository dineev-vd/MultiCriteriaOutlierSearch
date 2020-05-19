using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OutliersLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace outliers_api
{
    [Route("/")]
    public class OutliersController : Controller
    {
        // POST /
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutlierRequestData data)
        {
            // Проверка на корректность входных данных в body
            if (data is null)
            {
                Logger.Push("Неверный формат входящего запроса");
                return BadRequest("Неверный формат входящего запроса");
            }

            try
            {
                data.Check();
            }
            catch (Exception e)
            {
                Logger.Push(e.Message);
                return BadRequest(e.Message);
            }

            var algResponses = await data.FetchAlgorithms();
            Logger.Push("algorithms finished");
            var combResponses = await data.FetchCombinations(algResponses);
            Logger.Push("combinations finished");

            return Ok(new {algResponses, combResponses});
        }

        // GET /
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Utils.Read();
                Logger.Push("GET /");
            }
            catch(Exception e)
            {
                Logger.Push(e.Message);
            }

            return Ok(Utils.Config);
        }
    }
}
