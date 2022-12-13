using SistemaTurnosOnline.Models;

namespace SistemaTurnosOnline.Api.Repositories.Contracts
{
    public interface ICarreraRepository
    {
        Task CreateCarrera(Carrera professor);
        Task UpdateCarrera(Carrera professor);
        Task DeleteCarrera(string id);
        Task<List<Carrera>> GetCarreras();
        Task<Carrera> GetCarrera(string id);
    }
}
