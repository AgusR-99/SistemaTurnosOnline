using SistemaTurnosOnline.Models;

namespace SistemaTurnosOnline.Web.Services.Contracts
{
    public interface IProfesorService
    {
        Task<IEnumerable<Profesor>> GetProfesores();
        Task<Profesor> GetProfesor(string id);
        Task<Profesor> CreateProfesor(ProfesorForm profesorForm);
        //TODO: cambiar a UpdateProfesor(ProfesorForm profesorForm);
        Task<Profesor> UpdateProfesor(ProfesorForm profesorForm);
        Task<Profesor> DeleteProfesor(string id);
    }
}
