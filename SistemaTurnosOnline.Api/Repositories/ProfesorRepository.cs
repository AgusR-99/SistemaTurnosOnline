using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SistemaTurnosOnline.Api.Data;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Models;

namespace SistemaTurnosOnline.Api.Repositories
{
    public class ProfesorRepository : IProfesorRepository
    {
        // Instanciar MongoDb context
        private readonly SistemaTurnosOnlineDbContext dbContext;

        // Instanciar coleccion correspondiente
        private IMongoCollection<Profesor> profesorCollection;

        // Constructor inicializa coleccion
        public ProfesorRepository(SistemaTurnosOnlineDbContext dbContext)
        {
            this.dbContext = dbContext;
            profesorCollection = this.dbContext.db.GetCollection<Profesor>("profesor");
        }

        private async Task<bool> ProfesorExists(string dni)
        {
            return await profesorCollection.Find(p => p.Dni == dni).AnyAsync();
        }

        public async Task<Profesor> CreateProfesor(Profesor profesor)
        {
            if(!await ProfesorExists(profesor.Dni))
            {
                await profesorCollection.InsertOneAsync(profesor);

                return profesor;
            }

            return null;
        }

        public Task<Profesor> DeleteProfesor(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Profesor> GetProfesor(string id)
        {
            var profesor = await profesorCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
            return profesor;
        }

        public async Task<IEnumerable<Profesor>> GetProfesores()
        {
            // Filtrar mediante BsonDocument vacio
            var profesores = await profesorCollection.FindAsync(new BsonDocument()).Result.ToListAsync();
            return profesores;
        }

        public Task<Profesor> UpdateProfesor(Profesor profesor)
        {
            throw new NotImplementedException();
        }
    }
}
