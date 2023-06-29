using SistemaTurnosOnline.Shared.Turnos;

namespace SistemaTurnosOnline.Web.Services.TurnoManagement.Contracts
{
    public interface ITurnoResponseProcessor
    {
        public Task<Turno> ProcessTurnoResponseAsync(HttpResponseMessage response);

        public Task<IEnumerable<Turno>> ProcessTurnoCollectionResponseAsync(HttpResponseMessage response);
    }
}
