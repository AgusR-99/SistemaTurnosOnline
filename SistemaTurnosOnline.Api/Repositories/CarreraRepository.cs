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
        private async Task<bool> CarreraExists(string id)
        {
            return await carreraCollection.Find(new BsonDocument { { "_id", new ObjectId(id) } }).AnyAsync();
        }
        private async Task<bool> NombreExists(string nombre)
        {
            return await carreraCollection.Find(p => p.Nombre == nombre).AnyAsync();
        }
        private async Task<bool> DuplicateExists(Carrera carrera)
        {
            if (!await NombreExists(carrera.Nombre))
            {
                return false;
            }
            throw new Exception($"Parametro {nameof(carrera.Nombre)} no se puede repetir");
        }
        public async Task<Carrera> CreateCarrera(Carrera carrera)
        {
            if (!await CarreraExists(carrera.Id))
            {
                if (!await DuplicateExists(carrera))
                {
                    await carreraCollection.InsertOneAsync(carrera);

                    return carrera;
                }
            }
            return null;
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
    }
}
