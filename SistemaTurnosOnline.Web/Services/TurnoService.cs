using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services.Contracts;
using SistemaTurnosOnline.Shared.Turnos;
using System.Text.Json;
using System.Text;

namespace SistemaTurnosOnline.Web.Services
{
    public class TurnoService : ITurnoService
    {
        private readonly HttpClient httpClient;

        private async Task<Turno> ProcessTurnoResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default(Turno);
                }

                return await response.Content.ReadFromJsonAsync<Turno>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status: {response.StatusCode} Message: {message}");
        }

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

        public async Task<Turno> GetTurno(string id)
        {
            var response = await httpClient.GetAsync($"api/Turno/{id}");

            return await ProcessTurnoResponseAsync(response);
        }

        public async Task<Turno> CreateTurno(TurnoForm turnoForm)
        {
            var response = await httpClient.PostAsJsonAsync("api/Turno", turnoForm);

            return await response.Content.ReadFromJsonAsync<Turno>();
        }

        public async Task<Turno> UpdateTurno(Turno turno)
        {
            var turnoFormJson = new StringContent(JsonSerializer.Serialize(turno), Encoding.UTF8, "application/json");

            var response = await httpClient.PatchAsync($"api/Turno/{turno.Id}", turnoFormJson);

            return await ProcessTurnoResponseAsync(response);
        }

        public async Task<Turno> DeleteTurno(string id)
        {
            var response = await httpClient.DeleteAsync($"api/Turno/{id}");

            return await ProcessTurnoResponseAsync(response);
        }
    }
}
