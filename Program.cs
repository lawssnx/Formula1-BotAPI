using MongoDB.Bson;
using MongoDB.Driver;
using RaceBT_API.Model;
using RaceBT_API.Constant;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.OpenApi.Models;

class Program
{
    static void Main(string[] args)
    {
        constants.mongoClient = new MongoClient("mongodb+srv://lawssnx:kofois6102@racebt.kaiwb3h.mongodb.net/");
        constants.database = constants.mongoClient.GetDatabase("RaceBT");
        constants.collection = constants.database.GetCollection<BsonDocument>("RaceS");

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();


        builder.Services.AddSwaggerGen(options =>
        {

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My API",
                Version = "v1"
            });
        });

        var app = builder.Build();


        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty;
        });

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
