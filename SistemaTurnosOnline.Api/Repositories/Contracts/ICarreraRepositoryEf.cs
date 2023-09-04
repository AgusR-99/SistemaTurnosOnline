using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Api.Repositories.Contracts
{
    public interface ICarreraRepositoryEf
    {
        Task CreateCarrera(CarreraEf carrera);
        Task<List<CarreraEf>> GetCarreras();
        Task<CarreraEf> GetCarrera(int id);
        Task UpdateCarrera(CarreraEf carreraEf);
        Task DeleteCarrera(int id);
        Task<List<CarreraEf>> GetCarrerasByUserId(string userId);
        Task<CarreraEf> GetCarreraByName(string name);
        Task<CarreraEf> GetCarreraByCode(string code);
    }
}
