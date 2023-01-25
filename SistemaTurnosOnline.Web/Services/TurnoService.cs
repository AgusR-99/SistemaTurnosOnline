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

        public async Task<IEnumerable<Turno>> GetTurnos()
        {
            var response = await httpClient.GetAsync("api/Turno");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return Enumerable.Empty<Turno>();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Turno>>();
            }

            var message = await response.Content.ReadFromJsonAsync<Turno>();
            throw new Exception($"Http status: {response.StatusCode} Message: {message}");
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
