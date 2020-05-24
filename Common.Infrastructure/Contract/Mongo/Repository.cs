using MongoDB.Bson;
using MongoDB.Driver;

namespace Common.Infrastructure.Mongo
{
    public class Repository
    {
        protected readonly IMongoDatabase MongoDatabase;

        protected Repository(MongoConfig config)
        {
            var client = new MongoClient(config.Server);

            MongoDatabase = client.GetDatabase(config.Database,
                new MongoDatabaseSettings
                {
                    GuidRepresentation = GuidRepresentation.Standard
                });
        }
    }
}