using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Web.Services.Contracts
{
    public interface ICarreraService
    {
        Task<List<Carrera>> GetCarreras();
        List<string> GetCarrerasValues();
        Task<IEnumerable<Carrera>> GetCarrerasByUserId(string userId);
        void SetCarrerasValues(List<string> carreras);
        Task<Carrera> GetCarrera(string id);
        Task<Carrera> CreateCarrera(Carrera carrera);
        Task<Carrera> UpdateCarrera(Carrera carrera);
        Task<Carrera> DeleteCarrera(string id);
    }
}
