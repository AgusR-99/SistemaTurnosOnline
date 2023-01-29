using MongoDB.Bson;
using MongoDB.Driver;
using SistemaTurnosOnline.Api.Data;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Shared;
using System.Linq.Expressions;
using SistemaTurnosOnline.Shared.Turnos;

namespace SistemaTurnosOnline.Api.Repositories
{
    public class ProfesorRepository : IProfesorRepository
    {
        // Instanciar MongoDb context
        private readonly SistemaTurnosOnlineDbContext dbContext;
        private readonly ITurnoRepository turnoRepository;

        // Instanciar coleccion correspondiente
        private IMongoCollection<Profesor> profesorCollection;

        private bool CheckIfLastAdmin(List<Profesor> profesores)
        {
            int count = profesores.Count(profesor => profesor.Rol == "Admin");

            return count == 1;
        }

        // Constructor inicializa coleccion
        public ProfesorRepository(SistemaTurnosOnlineDbContext dbContext, ITurnoRepository turnoRepository)
        {
            this.dbContext = dbContext;
            this.turnoRepository = turnoRepository;
            profesorCollection = this.dbContext.db.GetCollection<Profesor>("profesor");
        }
        public async Task<Profesor> CreateProfesor(Profesor profesor)
        {
            await profesorCollection.InsertOneAsync(profesor);

            return profesor;
        }

        public async Task<Profesor?> DeleteProfesor(string id)
        {
            var filtro = Builders<Profesor>.Filter.Eq(p => p.Id, id);

            var profesorList = await profesorCollection.FindAsync(new BsonDocument()).Result.ToListAsync();

            var profesor = profesorList.First(p => p.Id == id);

            if (!CheckIfLastAdmin(profesorList) || profesor.Rol == "Guest")
            {
                if (profesor == null) return default(Profesor);

                var turnos = await turnoRepository.GetTurnosByUserId(profesor.Id);

                turnos.ToList().ForEach(t => turnoRepository.DeleteTurno(t.Id));

                await profesorCollection.DeleteOneAsync(filtro);

                return profesor;
            }

            throw new InvalidOperationException("El usuario no puede ser eliminado debido a que es el unico administrador");
        }

        public async Task<Profesor> GetProfesor(string id)
        {
            var filtroId = Builders<Profesor>.Filter.Eq(p => p.Id, id);

            var profesor = await profesorCollection.FindAsync(filtroId).Result.FirstOrDefaultAsync();

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
