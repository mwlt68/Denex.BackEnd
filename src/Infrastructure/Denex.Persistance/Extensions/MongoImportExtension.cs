using Denex.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Denex.Persistance.Extensions
{
    public static  class MongoImportExtension
    {
        public static void AddMongoImport(this IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("MongoDbSettings:ConnectionString");
            var databaseName = configuration.GetValue<string>("MongoDbSettings:Database");
            var filePath="Data/Seed/PracticeSchemaInit.json";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            string className = typeof(PracticeSchema).Name;
            var collectionExists = db.ListCollectionNames().ToList().Contains(className);
            Console.WriteLine($"{className} collection exist :{collectionExists}");
            if (!collectionExists) {
                try
                {
                    using (StreamReader r = new StreamReader(filePath))
                    {
                        string json = r.ReadToEnd();
                        var items = BsonSerializer.Deserialize<BsonDocument[]>(json);
                        var collection = db.GetCollection<BsonDocument>(className);
                        collection.InsertMany(items);
                    }
                    Console.WriteLine($"{className} collection created");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"{className} collection could not create. {ex.Message} ");
                }
            }
        }
    }
}