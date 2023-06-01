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
    public class DriverInformationController : ControllerBase
    {
        [HttpGet("get_driver_info")]
        public async Task<ActionResult<string>> GetDriverInfo(string last_name)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync($"https://ergast.com/api/f1/drivers/{last_name}.json");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string json = await response.Content.ReadAsStringAsync();
                Model.DriverInfo.DriverData driverData = JsonConvert.DeserializeObject<Model.DriverInfo.DriverData>(json);
                return Ok(driverData);
            }
            else return NotFound();
        }
        [HttpPut("bot_is_waiting_for_racer_lastname/{id}")]
        public ActionResult<string> BotWaitForToken(long id, bool b)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);
            var update = Builders<BsonDocument>.Update.Set("bot_is_waiting_for_racer_lastname", b);
            var result = constants.collection.UpdateOne(filter, update);
            return Ok();
        }

        [HttpGet("bot_is_waiting_for_racer_lastname/{id}")]
        public ActionResult<bool> BotWaitForToken(long id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);
            var document = constants.collection.Find(filter).FirstOrDefault();
            if (document != null && document.TryGetValue("bot_is_waiting_for_racer_lastname", out BsonValue value))
            {
                return value.AsBoolean;
            }

            return NotFound();
        }
    }
}
