using SistemaTurnosOnline.Models;

namespace SistemaTurnosOnline.Web.Services.Contracts
{
    public interface ICarreraService
    {
        Task<List<Carrera>> GetCarreras();
        List<string> GetCarrerasValues();
        void SetCarrerasValues(List<string> carreras);
        Task<Carrera> GetCarrera(string id);
        Task<Carrera> CreateCarrera(Carrera carrera);
        Task<Carrera> UpdateCarrera(Carrera carrera);
        Task<Carrera> DeleteCarrera(string id);
    }
}
