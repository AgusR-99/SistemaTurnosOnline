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

        private async Task<bool> ProfesorExists(string id)
        {
            return await profesorCollection.Find(new BsonDocument { { "_id", new ObjectId(id) } }).AnyAsync();
        }
        private async Task<bool> DniExists(string dni)
        {
            return await profesorCollection.Find(p => p.Dni == dni).AnyAsync();
        }
        private async Task<bool> EmailExists(string email)
        {
            return await profesorCollection.Find(p => p.Email == email).AnyAsync();
        }
        private async Task<bool> DuplicateExists(Profesor profesor)
        {
            if (!await EmailExists(profesor.Email))
            {
                if (!await DniExists(profesor.Dni))
                {
                    return false;
                }
                throw new Exception($"Parametro {nameof(profesor.Dni)} no se puede repetir");
            }
            throw new Exception($"Parametro {nameof(profesor.Email)} no se puede repetir");
        }

        public async Task<Profesor> CreateProfesor(Profesor profesor)
        {
            if(!await ProfesorExists(profesor.Id.ToString()))
            {
                if (!await DuplicateExists(profesor))
                {
                    await profesorCollection.InsertOneAsync(profesor);

                    return profesor;
                }
            }
            return null;
        }

        public Task<Profesor> DeleteProfesor(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Profesor> GetProfesor(string id)
        {
            var filtroEstado = Builders<Profesor>.Filter.Eq(p => p.Estado, true);

            var filtroId = Builders<Profesor>.Filter.Eq(p => p.Id, new ObjectId(id));

            var profesor = await profesorCollection.FindAsync(filtroEstado & filtroId).Result.FirstAsync();
            return profesor;
        }

        public async Task<IEnumerable<Profesor>> GetProfesores()
        {
            var filtroEstado = Builders<Profesor>.Filter.Eq(p => p.Estado, true);

            var profesores = await profesorCollection.FindAsync(filtroEstado).Result.ToListAsync();
            return profesores;
        }

        public async Task<IEnumerable<Profesor>> GetProfesoresInactive()
        {
            var filtroEstado = Builders<Profesor>.Filter.Eq(p => p.Estado, false);

            var profesores = await profesorCollection.FindAsync(filtroEstado).Result.ToListAsync();
            return profesores;
        }

        public async Task<Profesor> UpdateProfesor(Profesor profesor)
        {
            // Corroborar que el Id exista en la coleccion
            if (await ProfesorExists(profesor.Id.ToString()))
            {
                if (!await DuplicateExists(profesor))
                {
                    var filtro = Builders<Profesor>.Filter.Eq(p => p.Id, profesor.Id);

                    await profesorCollection.ReplaceOneAsync(filtro, profesor);

                    return profesor;
                }
            }
            return null;
        }
    }
}
