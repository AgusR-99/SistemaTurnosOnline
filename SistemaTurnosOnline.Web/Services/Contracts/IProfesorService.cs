using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Web.Services.Contracts
{
    public interface IProfesorService
    {
        Task<IEnumerable<Profesor>> GetProfesores();
        Task<IEnumerable<Profesor>> GetProfesoresInactive();
        Task<ProfesorSecure> GetProfesor(string id);
        Task<Profesor> GetProfesorByDni(string dni);
        Task<Profesor> CreateProfesor(ProfesorForm profesorForm);
        //TODO: cambiar a UpdateProfesor(ProfesorForm profesorForm);
        Task<Profesor> UpdateProfesor(ProfesorSecure profesorSecure);
        Task<Profesor> DeleteProfesor(string id);
    }
}
