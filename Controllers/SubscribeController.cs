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
    public class SubscribeController : ControllerBase
    {
        [HttpGet("user_is_subscribed/{id}")]
        public ActionResult<bool> UserIsSubscribedToUpdates(long id)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);
                var document = constants.collection.Find(filter).FirstOrDefault();
                var qwer = document["user_is_subscribed"].AsBoolean;
                return Ok(qwer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("user_is_subscribed/{id}")]
        public ActionResult<string> UserIsSubscribedToUpdates(long id, bool b)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("user_id", id);
                var update = Builders<BsonDocument>.Update.Set("user_is_subscribed", b);
                var result = constants.collection.UpdateOne(filter, update);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        
    }
}
