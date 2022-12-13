using MongoDB.Driver;

namespace SistemaTurnosOnline.Api.Data
{
    public class SistemaTurnosOnlineDbContext
    {
        private readonly IConfiguration configuration;
        public MongoClient? client;
        public IMongoDatabase db;
        public SistemaTurnosOnlineDbContext(IConfiguration configuration)
        {
            // Se accede al connection string
            var connectionString = this.configuration["CONNECTION_STRING"];

            // Se inicializa una nueva instancia de MongoCliente mediante el connection string
            var client = new MongoClient(connectionString);

            db = client.GetDatabase("turnosdb");
        }
    }
}
