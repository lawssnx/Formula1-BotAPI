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
    public class RaceScheduleController : ControllerBase
    {
        [HttpGet("get_schedule_info")]
        public async Task<ActionResult<string>> GetRaceSchedule(int year)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://ergast.com/api/f1/{year}.json");
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string json = await response.Content.ReadAsStringAsync();
                Model.RaceSchInfo.RaceData raceData = JsonConvert.DeserializeObject<Model.RaceSchInfo.RaceData>(json);
                return Ok(raceData);
            }
            else return NotFound();
        }
        [HttpPut("bot_is_waiting_for_year/{id}")]
        public ActionResult<string> BotWaitForToken(long id, bool b)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);
            var update = Builders<BsonDocument>.Update.Set("bot_is_waiting_for_year", b);
            var result = constants.collection.UpdateOne(filter, update);
            return Ok();
        }

        [HttpGet("bot_is_waiting_for_year/{id}")]
        public ActionResult<bool> BotWaitForToken(long id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);
            var document = constants.collection.Find(filter).FirstOrDefault();
            if (document != null && document.TryGetValue("bot_is_waiting_for_year", out BsonValue value))
            {
                return value.AsBoolean;
            }
            return NotFound();
        }
    }
}
