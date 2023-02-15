using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Api.Repositories.Contracts
{
    public interface ICarreraRepository
    {
        Task<Carrera> CreateCarrera(Carrera professor);
        Task<Carrera> UpdateCarrera(Carrera professor, string id);
        Task<Carrera> DeleteCarrera(string id);
        Task<List<Carrera>> GetCarreras();
        Task<Carrera> GetCarrera(string id);
        Task<Carrera> GetCarreraByName(string name);
        Task<Carrera> GetCarreraByCode(string code);
    }
}
