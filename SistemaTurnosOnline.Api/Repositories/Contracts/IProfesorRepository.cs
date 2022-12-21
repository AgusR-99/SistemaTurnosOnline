using SistemaTurnosOnline.Models;

namespace SistemaTurnosOnline.Api.Repositories.Contracts
{
    public interface IProfesorRepository
    {
        Task<Profesor> CreateProfesor(Profesor profesor);
        Task<Profesor> UpdateProfesor(Profesor profesor);
        Task<Profesor> DeleteProfesor(string id);
        Task<Profesor> GetProfesor(string id);
        Task<IEnumerable<Profesor>> GetProfesores();
        Task<IEnumerable<Profesor>> GetProfesoresInactive();
        Task<bool> IsDuplicated(string value, AttributeCheck.Attribute check);
    }
}
