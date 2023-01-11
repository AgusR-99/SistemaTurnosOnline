using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services.Contracts;
using SistemaTurnosOnline.Shared.Turnos;

namespace SistemaTurnosOnline.Web.Services
{
    public class TurnoService : ITurnoService
    {
        private readonly HttpClient httpClient;

        public TurnoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public Task<List<Turno>> GetTurnos()
        {
            throw new NotImplementedException();
        }

        public Task<Turno> GetTurno(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Turno> CreateTurno(TurnoForm turnoForm)
        {
            var response = await httpClient.PostAsJsonAsync("api/Turno", turnoForm);

            return await response.Content.ReadFromJsonAsync<Turno>();
        }

        public Task<Turno> UpdateTurno(TurnoForm turnoForm)
        {
            throw new NotImplementedException();
        }

        public Task<Turno> DeleteTurno(string id)
        {
            throw new NotImplementedException();
        }
    }
}
