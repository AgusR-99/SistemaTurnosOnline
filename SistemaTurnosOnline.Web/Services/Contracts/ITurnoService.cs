using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Turnos;

namespace SistemaTurnosOnline.Web.Services.Contracts
{
    public interface ITurnoService
    {
        Task<List<Turno>> GetTurnos();
        Task<Turno> GetTurno(string id);
        Task<Turno> CreateTurno(TurnoForm turnoForm);
        Task<Turno> UpdateTurno(TurnoForm turnoForm);
        Task<Turno> DeleteTurno(string id);
    }
}
