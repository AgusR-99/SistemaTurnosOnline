using SistemaTurnosOnline.Shared.Turnos;
using SistemaTurnosOnline.Web.Services.TurnoManagement.Contracts;
using System.Text;
using System.Text.Json;

namespace SistemaTurnosOnline.Web.Services.TurnoManagement
{
    public class TurnoHttpClient : ITurnoHttpClient
    {
        private readonly HttpClient httpClient;

        public TurnoHttpClient(HttpClient httpClient) =>
            this.httpClient = httpClient;

        public async Task<HttpResponseMessage> CreateTurnoAsync(TurnoForm turnoForm) =>
             await httpClient.PostAsJsonAsync("api/Turno", turnoForm);

        public async Task<HttpResponseMessage> DeleteTurnoAsync(string id) =>
             await httpClient.DeleteAsync($"api/Turno/{id}");

        public async Task<HttpResponseMessage> GetTurnoAsync(string id) =>
            await httpClient.GetAsync($"api/Turno/{id}");

        public async Task<HttpResponseMessage> GetTurnosAsync() =>
            await httpClient.GetAsync("api/Turno");

        public async Task<HttpResponseMessage> GetTurnosByUserIdAsync(string userId) =>
            await httpClient.GetAsync($"api/Turno/UserId/{userId}");

        public async Task<HttpResponseMessage> UpdateTurnoAsync(Turno turno) =>
            await httpClient.PatchAsync($"api/Turno/{turno.Id}", new StringContent(JsonSerializer.Serialize(turno), Encoding.UTF8, "application/json"));
    }
}
