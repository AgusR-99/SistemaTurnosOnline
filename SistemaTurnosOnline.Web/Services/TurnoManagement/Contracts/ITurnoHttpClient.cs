using SistemaTurnosOnline.Shared.Turnos;

namespace SistemaTurnosOnline.Web.Services.TurnoManagement.Contracts
{
    public interface ITurnoHttpClient
    {
        public Task<HttpResponseMessage> CreateTurnoAsync(TurnoForm turnoForm);

        public Task<HttpResponseMessage> DeleteTurnoAsync(string id);

        public Task<HttpResponseMessage> UpdateTurnoAsync(Turno turno);

        public Task<HttpResponseMessage> GetTurnosAsync();

        public Task<HttpResponseMessage> GetTurnoAsync(string id);

        public Task<HttpResponseMessage> GetTurnosByUserIdAsync(string userId);
    }
}
