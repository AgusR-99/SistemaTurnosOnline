using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Web.Services.CarreraManagement.Contracts
{
    public interface ICarreraService
    {
        Task<List<Carrera>> GetCarreras();
        Task<List<Carrera>> GetCarrerasByUserId(string userId);
        Task<Carrera> GetCarrera(string id);
        Task<Carrera> CreateCarrera(Carrera carrera);
        Task<Carrera> UpdateCarrera(Carrera carrera);
        Task<Carrera> DeleteCarrera(string id);
    }
}
