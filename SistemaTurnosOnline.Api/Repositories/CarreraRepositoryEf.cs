using Microsoft.EntityFrameworkCore;
using SistemaTurnosOnline.Api.Data;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Api.Repositories
{
    public class CarreraRepositoryEf : ICarreraRepositoryEf
    {
        private readonly SistemaTurnosOnlineDbContextEf dbContextEf;
        private readonly IProfesorRepository profesorRepository;

        public CarreraRepositoryEf(SistemaTurnosOnlineDbContextEf dbContextEf, IProfesorRepository profesorRepository)
        {
            this.dbContextEf = dbContextEf;
            this.profesorRepository = profesorRepository;
        }

        public async Task<List<CarreraEf>> GetCarreras()
        {
            return await dbContextEf.CarreraSet.ToListAsync();
        }

        public async Task CreateCarrera(CarreraEf carrera)
        {
            dbContextEf.CarreraSet.Add(carrera);
            await dbContextEf.SaveChangesAsync();
        }

        public async Task<CarreraEf> GetCarrera(int id)
        {
            return await dbContextEf.CarreraSet.FindAsync(id);
        }

        public async Task UpdateCarrera(CarreraEf carreraEf)
        {
            dbContextEf.CarreraSet.Update(carreraEf);
            await dbContextEf.SaveChangesAsync();
        }

        public async Task DeleteCarrera(int id)
        {
            var carrera = await dbContextEf.CarreraSet.FindAsync(id);

            if (carrera != null)
            {
                dbContextEf.CarreraSet.Remove(carrera);
                await dbContextEf.SaveChangesAsync();
            }
        }

        public async Task<List<CarreraEf>> GetCarrerasByUserId(string userId)
        {
            var profesor = await profesorRepository.GetProfesor(userId);

            var carrerasProfesorIds = profesor.CarrerasId?.ToList().ConvertAll(int.Parse) ?? new List<int>();

            return dbContextEf.CarreraSet.Where(carrera => carrerasProfesorIds.Contains(carrera.Id)).ToList();
        }

        public Task<CarreraEf> GetCarreraByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<CarreraEf> GetCarreraByCode(string code)
        {
            throw new NotImplementedException();
        }
    }
}
