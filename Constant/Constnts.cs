using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RaceBT_API.Model;
using RaceBT_API.Constant;
using MongoDB.Bson;
using MongoDB.Driver;


namespace RaceBT_API.Constant
{
    public class constants
    {
        public static string botId = "6271901154:AAGQxey2sFyiuprU0Vti44Vq8KRqBJkcZGk";
        public static MongoClient mongoClient;
        public static IMongoDatabase database;
        public static IMongoCollection<BsonDocument> collection;
    }
}
