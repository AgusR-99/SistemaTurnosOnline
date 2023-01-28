using System.Linq.Expressions;
using SistemaTurnosOnline.Shared.Turnos;

namespace SistemaTurnosOnline.Api.Repositories.Contracts
{
    public interface ITurnoRepository
    {
        Task<Turno> CreateTurno(Turno turno);
        Task<Turno> UpdateTurno(Turno turno, string id);
        Task<Turno> DeleteTurno(string id);
        Task<Turno> GetTurnoByParam(string value, Expression<Func<Turno, string>> field);
        Task<Turno> GetTurno(string id);
        Task<IEnumerable<Turno>> GetTurnosByUserId(string userId);
        Task<IEnumerable<Turno>> GetTurnos();
        Task<long> GetTurnoCount();
    }
}
