using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using System.Text.Json;
using System;
using Newtonsoft.Json;
using RaceBT_API.Model;
using RaceBT_API.Constant;
using MongoDB.Bson;
using MongoDB.Driver;
namespace RaceBT_API.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class NextRaceController : ControllerBase
        {
            [HttpPost("post_next_info")]
            public async Task<ActionResult<string>> GetNextRace()
            {
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"http://ergast.com/api/f1/current/next.json");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Model.RaceSchInfo.RaceData raceData = JsonConvert.DeserializeObject<Model.RaceSchInfo.RaceData>(json);
                    return Ok(raceData);
                }
                else return NotFound();
            }
        }
}
