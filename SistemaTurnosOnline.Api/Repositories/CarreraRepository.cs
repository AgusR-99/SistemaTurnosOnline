using MongoDB.Bson;
using MongoDB.Driver;
using SistemaTurnosOnline.Api.Data;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Models;

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

        public Task<Carrera> UpdateCarrera(Carrera professor)
        {
            throw new NotImplementedException();
        }

        public Task<Carrera> DeleteCarrera(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Carrera>> GetCarreras()
        {
            var carreras = await carreraCollection.FindAsync(new BsonDocument()).Result.ToListAsync();
            return carreras;
        }

        public async Task<Carrera> GetCarrera(string id)
        {
            var carrera = await carreraCollection.FindAsync(new BsonDocument()).Result.FirstAsync();
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
