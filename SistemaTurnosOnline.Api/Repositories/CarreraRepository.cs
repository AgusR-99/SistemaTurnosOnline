using System.Linq.Expressions;
using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SistemaTurnosOnline.Api.Data;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Api.Repositories
{
    public class CarreraRepository : ICarreraRepository
    {
        private readonly SistemaTurnosOnlineDbContext dbContext;
        private readonly IProfesorRepository profesorRepository;
        private readonly ITurnoRepository turnoRepository;

        // Instanciar coleccion correspondiente
        private IMongoCollection<Carrera> carreraCollection;

        public CarreraRepository(SistemaTurnosOnlineDbContext dbContext, IProfesorRepository profesorRepository,
            ITurnoRepository turnoRepository)
        {
            this.dbContext = dbContext;
            this.profesorRepository = profesorRepository;
            this.turnoRepository = turnoRepository;
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

            var carreraToDelete = await carreraCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();

            if (carreraToDelete != null)
            {
                var profesores = await profesorRepository.GetProfesores();

                foreach (var profesor in profesores)
                {
                    var carreraRefId = profesor.CarrerasId.Find(c => c == id);

                    if (carreraRefId != null)
                    {
                        profesor.CarrerasId.Remove(carreraRefId);
                    }

                    profesorRepository.UpdateProfesor(profesor, profesor.Id);
                }

                var turnos = await turnoRepository.GetTurnos();

                foreach (var turno in turnos)
                {
                    if (turno.CarreraId == id)
                    {
                        turno.CarreraId = "0";

                        turnoRepository.UpdateTurno(turno, turno.Id);
                    }
                }

                await carreraCollection.DeleteOneAsync(filtro);
            }

            return carreraToDelete;
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

            var carrera = await carreraCollection.FindAsync(filtroId).Result.FirstOrDefaultAsync();

            return carrera;
        }

        public async Task<Carrera> GetCarreraByCode(string code)
        {
            var filtroId = Builders<Carrera>.Filter.Eq(c => c.Codigo, code);

            var carrera = await carreraCollection.FindAsync(filtroId).Result.FirstOrDefaultAsync();

            return carrera;
        }

        public async Task<List<Carrera>> GetCarrerasByUserId(string userId)
        {
            var profesor = await profesorRepository.GetProfesor(userId);

            var carrerasIdProfesor = profesor.CarrerasId?.ToList();

            var carrerasProfesor = new List<Carrera>();

            if (carrerasIdProfesor != null)
            {
                var carreras = await carreraCollection.FindAsync(new BsonDocument()).Result.ToListAsync();

                carrerasIdProfesor.ForEach(id => carrerasProfesor.Add(carreras.Find(x => x.Id == id)));
            }

            if (carrerasProfesor.Count() == 1 && carrerasProfesor[0] is null)
                carrerasProfesor = new List<Carrera>();

            return carrerasProfesor;
        }
    }
}
