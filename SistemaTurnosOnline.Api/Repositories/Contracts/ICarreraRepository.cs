using SistemaTurnosOnline.Models;

namespace SistemaTurnosOnline.Api.Repositories.Contracts
{
    public interface ICarreraRepository
    {
        Task<Carrera> CreateCarrera(Carrera professor);
        Task<Carrera> UpdateCarrera(Carrera professor);
        Task<Carrera> DeleteCarrera(string id);
        Task<List<Carrera>> GetCarreras();
        Task<Carrera> GetCarrera(string id);
    }
}
