using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;
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
        public ProfesorRepository()
        {
            profesorCollection = dbContext.db.GetCollection<Profesor>("profesor");
        }

        public Task<Profesor> CreateProfesor(Profesor profesor)
        {
            throw new NotImplementedException();
        }

        public Task<Profesor> DeleteProfesor(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Profesor> GetProfesor(string id)
        {
            throw new NotImplementedException();
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
