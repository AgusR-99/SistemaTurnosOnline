using MongoDB.Bson;
using MongoDB.Driver;
using SistemaTurnosOnline.Api.Data;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Api.Repositories
{
    public class CarreraRepository : ICarreraRepository
    {
        private readonly SistemaTurnosOnlineDbContext dbContext;

        // Instanciar coleccion correspondiente
        private IMongoCollection<Carrera> carreraCollection;

        public CarreraRepository(SistemaTurnosOnlineDbContext dbContext)
        {
            this.dbContext = dbContext;
            carreraCollection = this.dbContext.db.GetCollection<Carrera>("carrera");
        }

        public async Task<Carrera> CreateCarrera(Carrera carrera)
        {
            await carreraCollection.InsertOneAsync(carrera);
            return carrera;
        }

        public async Task<Carrera> UpdateCarrera(Carrera carrera, string id)
        {
            var filtroId = Builders<Carrera>.Filter.Eq(c => c.Id, id);

            await carreraCollection.ReplaceOneAsync(filtroId, carrera);

            return carrera;
        }

        public async Task<Carrera> DeleteCarrera(string id)
        {
            var filtro = Builders<Carrera>.Filter.Eq(c => c.Id, id);

            var carrera = await carreraCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();

            if (carrera != null)
            {
                await carreraCollection.DeleteOneAsync(filtro);
            }

            return carrera;
        }

        public async Task<List<Carrera>> GetCarreras()
        {
            var carreras = await carreraCollection.FindAsync(new BsonDocument()).Result.ToListAsync();
            return carreras;
        }

        public async Task<Carrera> GetCarrera(string id)
        {
            var filtro = Builders<Carrera>.Filter.Eq(c => c.Id, id);

            var carrera = await carreraCollection.FindAsync(filtro).Result.FirstAsync();
            return carrera;
        }

        public async Task<Carrera> GetCarreraByName(string name)
        {
            var filtroId = Builders<Carrera>.Filter.Eq(c => c.Nombre, name);

            var profesor = await carreraCollection.FindAsync(filtroId).Result.FirstOrDefaultAsync();

            return profesor;
        }
    }
}
