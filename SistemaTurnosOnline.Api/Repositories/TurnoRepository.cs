using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using SistemaTurnosOnline.Api.Data;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Shared;
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
            var count = await GetTurnoCount();

            turno.OrdenEnCola = count + 1;

            turno.Estado = true;

            turno.Descripcion = string.IsNullOrWhiteSpace(turno.Descripcion) ? "Sin descripcion" : turno.Descripcion;

            await turnoCollection.InsertOneAsync(turno);

            return turno;
        }

        public async Task<Turno> UpdateTurno(Turno updatedTurno, string id)
        {
            var filtroId = Builders<Turno>.Filter.Eq(t => t.Id, id);

            var turnoToUpdate = await turnoCollection.FindAsync(filtroId).Result.FirstOrDefaultAsync();

            if (turnoToUpdate.OrdenEnCola == updatedTurno.OrdenEnCola)
            {
                await turnoCollection.ReplaceOneAsync(filtroId, updatedTurno);

                return updatedTurno;
            }

            var turnoList = await turnoCollection
                .Find(new BsonDocument())
                .Sort(Builders<Turno>.Sort.Ascending("OrdenEnCola"))
                .ToListAsync();

            var initialIndex = turnoList.FindIndex(t => t.Id == turnoToUpdate.Id);

            var finalIndex = turnoList.FindIndex(t => t.OrdenEnCola == updatedTurno.OrdenEnCola);

            turnoList.RemoveAt(initialIndex);

            turnoList.Insert(finalIndex, updatedTurno);

            if (initialIndex < finalIndex)
            {
                for (var i = initialIndex; i <= finalIndex; i++)
                {
                    if (i != finalIndex) turnoList[i].OrdenEnCola--;

                    filtroId = Builders<Turno>.Filter.Eq(t => t.Id, turnoList[i].Id);

                    await turnoCollection.ReplaceOneAsync(filtroId, turnoList[i]);
                }
            }
            else
            {
                for (var i = finalIndex; i <= initialIndex; i++)
                {
                    if (i != finalIndex) turnoList[i].OrdenEnCola++;

                    filtroId = Builders<Turno>.Filter.Eq(t => t.Id, turnoList[i].Id);

                    await turnoCollection.ReplaceOneAsync(filtroId, turnoList[i]);
                }
            }

            return updatedTurno;
        }

        public async Task<Turno> DeleteTurno(string id)
        {
            var filtro = Builders<Turno>.Filter.Eq(p => p.Id, id);

            var turno = await turnoCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();

            var turnoList = await turnoCollection
                .Find(new BsonDocument())
                .Sort(Builders<Turno>.Sort.Ascending("OrdenEnCola"))
                .ToListAsync();

            var index = turnoList.FindIndex(t => t.Id == turno.Id);

            var count = await GetTurnoCount();

            if (turno != null) await turnoCollection.DeleteOneAsync(filtro);

            for (var i = index; i < count; i++)
            {
                turnoList[i].OrdenEnCola--;

                filtro = Builders<Turno>.Filter.Eq(t => t.Id, turnoList[i].Id);

                await turnoCollection.ReplaceOneAsync(filtro, turnoList[i]);
            }

            return turno;
        }

        public Task<Turno> GetTurnoByParam(string value, Expression<Func<Turno, string>> field)
        {
            throw new NotImplementedException();
        }

        public async Task<Turno> GetTurno(string id)
        {
            var filtroId = Builders<Turno>.Filter.Eq(t => t.Id, id);

            var turno = await turnoCollection.FindAsync(filtroId).Result.FirstOrDefaultAsync();

            return turno;
        }

        public async Task<IEnumerable<Turno>> GetTurnosByUserId(string userId)
        {
            var filtroUserId = Builders<Turno>.Filter.Eq(t => t.UserId, userId);

            var turno = await turnoCollection
                .Find(filtroUserId)
                .Sort(Builders<Turno>.Sort.Ascending("OrdenEnCola"))
                .ToListAsync();

            return turno;
        }

        public async Task<IEnumerable<Turno>> GetTurnos()
        {
            return await turnoCollection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<long> GetTurnoCount()
        {
            return await turnoCollection.CountDocumentsAsync(new BsonDocument());
        }
    }
}
