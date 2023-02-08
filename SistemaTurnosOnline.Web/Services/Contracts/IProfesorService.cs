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
        Task<Profesor> UpdateProfesor(ProfesorSecure profesorSecure);
        Task<Profesor> UpdateProfesorPassword(ProfileSecurityForm profileSecurityForm);
        Task<string> ResetPassword(string id);
        Task<Profesor> DeleteProfesor(string id);
    }
}
