using MongoDB.Driver;

namespace SistemaTurnosOnline.Api.Data
{
    public class SistemaTurnosOnlineDbContext
    {
        public MongoClient? client;
        public IMongoDatabase db;
        public SistemaTurnosOnlineDbContext(IConfiguration configuration)
        {

            // Acceder al connection string
            var connectionString = configuration.GetConnectionString("CONNECTION_STRING");

            // Inicializar una nueva instancia de MongoCliente mediante el connection string
            var client = new MongoClient(connectionString);

            // Acceder al nombre de la db
            var databaseName = "turnosdb";

            db = client.GetDatabase(databaseName);
        }
    }
}
