using SistemaTurnosOnline.Shared.Turnos;

namespace SistemaTurnosOnline.Web.Services.TurnoManagement.Contracts
{
    public interface ITurnoService
    {
        Task<IEnumerable<Turno>> GetTurnos();
        Task<Turno> GetTurno(string id);
        Task<IEnumerable<Turno>> GetTurnosByUserId(string userId);
        Task<Turno> CreateTurno(TurnoForm turnoForm);
        Task<Turno> UpdateTurno(Turno turno);
        Task<Turno> DeleteTurno(string id);
    }
}
