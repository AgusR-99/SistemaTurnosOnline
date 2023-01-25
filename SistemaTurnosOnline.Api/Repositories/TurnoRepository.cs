using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using SistemaTurnosOnline.Api.Data;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Shared.Turnos;

namespace SistemaTurnosOnline.Api.Repositories
{
    public class TurnoRepository : ITurnoRepository
    {
        // Instanciar MongoDb context
        private readonly SistemaTurnosOnlineDbContext dbContext;

        // Instanciar coleccion correspondiente
        private IMongoCollection<Turno> turnoCollection;

        public TurnoRepository(SistemaTurnosOnlineDbContext dbContext)
        {
            this.dbContext = dbContext;
            turnoCollection = this.dbContext.db.GetCollection<Turno>("turno");
        }

        public async Task<Turno> CreateTurno(Turno turno)
        {
            var count = await turnoCollection.CountDocumentsAsync(new BsonDocument());

            turno.OrdenEnCola = count + 1;

            turno.Estado = true;

            turno.Descripcion = string.IsNullOrWhiteSpace(turno.Descripcion) ? "Sin descripcion" : turno.Descripcion;

            await turnoCollection.InsertOneAsync(turno);

            return turno;
        }

        public Task<Turno> UpdateTurno(Turno turno, string id)
        {
            throw new NotImplementedException();
        }

        public Task<Turno> DeleteTurno(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Turno> GetTurnoByParam(string value, Expression<Func<Turno, string>> field)
        {
            throw new NotImplementedException();
        }

        public async Task<Turno> GetTurno(string id)
        {
            var filtroId = Builders<Turno>.Filter.Eq(t => t.Id, id);

            var turno = await turnoCollection.FindAsync(filtroId).Result.FirstAsync();

            return turno;
        }

        public async Task<IEnumerable<Turno>> GetTurnos()
        {
            return await turnoCollection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
    }
}
