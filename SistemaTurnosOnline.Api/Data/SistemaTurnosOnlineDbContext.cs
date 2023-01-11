using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Api.Data
{
    public class SistemaTurnosOnlineDbContext
    {
        public MongoClient? client;
        public IMongoDatabase db;
        public SistemaTurnosOnlineDbContext(IOptions<MongoDbSettings> mongoDbSettings)
        {

            // Acceder al connection string
            var connectionString = mongoDbSettings.Value.ConnectionURI;

            // Inicializar una nueva instancia de MongoCliente mediante el connection string
            var client = new MongoClient(connectionString);

            // Acceder al nombre de la db
            var databaseName = mongoDbSettings.Value.DatabaseName;

            db = client.GetDatabase(databaseName);
        }
    }
}
