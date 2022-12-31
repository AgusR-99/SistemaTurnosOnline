using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SistemaTurnosOnline.Api.Data;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Models;
using System.Linq.Expressions;

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
        public async Task<Profesor> CreateProfesor(Profesor profesor)
        {
            await profesorCollection.InsertOneAsync(profesor);

            return profesor;
        }

        public async Task<Profesor> DeleteProfesor(string id)
        {
            var filtro = Builders<Profesor>.Filter.Eq(p => p.Id, id);

            var profesor = await profesorCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();

            if (profesor != null)
            {
                await profesorCollection.DeleteOneAsync(filtro);
            }

            return profesor;
        }

        public async Task<Profesor> GetProfesor(string id)
        {
            var filtroId = Builders<Profesor>.Filter.Eq(p => p.Id, id);

            var profesor = await profesorCollection.FindAsync(filtroId).Result.FirstAsync();
            return profesor;
        }

        public async Task<IEnumerable<Profesor>> GetProfesores()
        {
            var profesores = await profesorCollection.FindAsync(new BsonDocument()).Result.ToListAsync();
            return profesores;
        }

        public async Task<IEnumerable<Profesor>> GetProfesoresInactive()
        {
            var filtroEstado = Builders<Profesor>.Filter.Eq(p => p.Estado, false);

            var profesores = await profesorCollection.FindAsync(filtroEstado).Result.ToListAsync();
            return profesores;
        }

        public async Task<Profesor> UpdateProfesor(Profesor profesor, string id)
        {
            var filtroId = Builders<Profesor>.Filter.Eq(p => p.Id, id);

            await profesorCollection.ReplaceOneAsync(filtroId, profesor);

            return profesor;
        }

        public async Task<Profesor> GetProfesorByParam(string value, Expression<Func<Profesor, string>> field)
        {
            var filtro = Builders<Profesor>.Filter.Eq(field, value);

            var profesor = await profesorCollection.FindAsync(filtro).Result.FirstOrDefaultAsync();

            return profesor;
        }
    }
}
