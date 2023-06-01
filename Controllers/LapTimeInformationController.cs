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
    public class LapTimeInformationController : ControllerBase
    {
        [HttpGet("get_laptime_info")]
        public async Task<ActionResult<string>> GetLapInfo(int year, int round, int lapNumber)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://ergast.com/api/f1/{year}/{round}/laps/{lapNumber}.json");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string json = await response.Content.ReadAsStringAsync();
                Model.LapTimeInfo.LapData lapData = JsonConvert.DeserializeObject<Model.LapTimeInfo.LapData>(json);
                return Ok(lapData);
            }
            else return NotFound();
        }
        [HttpPut("bot_is_waiting_for_type_info/{id}")]
        public ActionResult<string> BotWaitForToken(long id, bool b)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);
            var update = Builders<BsonDocument>.Update.Set("bot_is_waiting_for_type_info", b);
            var result = constants.collection.UpdateOne(filter, update);
            return Ok();
        }
        [HttpGet("bot_is_waiting_for_type_info/{id}")]
        public ActionResult<bool> BotWaitForToken(long id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);
            var document = constants.collection.Find(filter).FirstOrDefault();
            if (document != null && document.TryGetValue("bot_is_waiting_for_type_info", out BsonValue value))
            {
                return value.AsBoolean;
            }

            return NotFound();
        }
    }
}
    


