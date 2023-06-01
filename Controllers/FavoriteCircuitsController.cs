using Microsoft.AspNetCore.Mvc;
using static RaceBT_API.Model.CircuitInfo;
using Newtonsoft.Json;
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
    public class FavoriteCircuitsController : ControllerBase
    {

        [HttpPut("bot_is_waiting_for_add/{id}")]
        public ActionResult<string> BotIsWaitingForCircuitsToAdd(long id, bool b)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);
            var update = Builders<BsonDocument>.Update.Set("bot_is_waiting_for_add", b);
            var result = constants.collection.UpdateOne(filter, update);
            return Ok();
        }

        [HttpGet("bot_is_waiting_for_add/{id}")]
        public ActionResult<bool> BotIsWaitingForCircuitToAdd(long id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);
            var document = constants.collection.Find(filter).FirstOrDefault();
            if (document != null && document.TryGetValue("bot_is_waiting_for_add", out BsonValue value))
            {
                return value.AsBoolean;
            }

            return NotFound();
        }

        [HttpPut("bot_is_waiting_for_circuit_num/{id}")]
        public ActionResult<string> BotIsWaitingForCircuitNum(long id, bool b)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);
            var update = Builders<BsonDocument>.Update.Set("bot_is_waiting_for_circuit_num", b);
            var result = constants.collection.UpdateOne(filter, update);
            return Ok();
        }

        [HttpGet("bot_is_waiting_for_circuit_num/{id}")]
        public ActionResult<bool> BotIsWaitingForCircuitNum(long id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);
            var document = constants.collection.Find(filter).FirstOrDefault();
            if (document != null && document.TryGetValue("bot_is_waiting_for_circuit_num", out BsonValue value))
            {
                return value.AsBoolean;
            }

            return NotFound();
        }

        [HttpPut("bot_is_waiting_for_delete/{id}")]
        public ActionResult<string> BotIsWaitingForCircuitsToDelete(long id, bool b)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);
            var update = Builders<BsonDocument>.Update.Set("bot_is_waiting_for_delete", b);
            var result = constants.collection.UpdateOne(filter, update);
            return Ok();
        }

        [HttpGet("bot_is_waiting_for_delete/{id}")]
        public ActionResult<bool> BotIsWaitingForCircuitToDelete(long id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);
            var document = constants.collection.Find(filter).FirstOrDefault();
            if (document != null && document.TryGetValue("bot_is_waiting_for_delete", out BsonValue value))
            {
                return value.AsBoolean;
            }

            return NotFound();
        }
    }

}
