using SistemaTurnosOnline.Shared;
using System.Linq.Expressions;

namespace SistemaTurnosOnline.Api.Repositories.Contracts
{
    public interface IProfesorRepository
    {
        Task<Profesor> CreateProfesor(Profesor profesor);
        Task<Profesor> UpdateProfesor(Profesor profesor, string id);
        Task<Profesor> DeleteProfesor(string id);
        Task<Profesor> GetProfesorByParam(string value, Expression<Func<Profesor, string>> field);
        Task<Profesor> GetProfesor(string id);
        Task<IEnumerable<Profesor>> GetProfesores();
        Task<IEnumerable<Profesor>> GetProfesoresInactive();
    }
}
