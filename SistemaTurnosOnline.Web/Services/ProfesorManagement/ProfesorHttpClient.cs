using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services.ProfesorManagement.Contracts;
using System.Text;
using System.Text.Json;

namespace SistemaTurnosOnline.Web.Services.ProfesorManagement
{
    public class ProfesorHttpClient : IProfesorHttpClient
    {

        private readonly HttpClient httpClient;

        public ProfesorHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> CreateProfesorAsync(ProfesorForm profesorForm) =>
            await httpClient.PostAsJsonAsync("api/Profesor", profesorForm);

        public async Task<HttpResponseMessage> DeleteProfesorAsync(string id) =>
            await httpClient.DeleteAsync($"api/Profesor/{id}");

        public async Task<HttpResponseMessage> GetProfesorAsync(string id) =>
            await httpClient.GetAsync($"api/Profesor/{id}");

        public async Task<HttpResponseMessage> GetProfesorByDniAsync(string dni) =>
            await httpClient.GetAsync($"api/Profesor/GetByDni/{dni}");

        public async Task<HttpResponseMessage> GetProfesoresAsync() =>
            await httpClient.GetAsync("api/Profesor");

        public async Task<HttpResponseMessage> GetProfesoresInactiveAsync() =>
            await httpClient.GetAsync("api/Profesor/GetInactive");

        public async Task<HttpResponseMessage> ResetPassword(string id) =>
            await httpClient.PatchAsync($"api/Profesor/ResetPassword/{id}", new StringContent(JsonSerializer.Serialize(id), Encoding.UTF8, "application/json"));

        public async Task<HttpResponseMessage> UpdateProfesorAsync(ProfesorSecure profesorSecure) =>
            await httpClient.PatchAsync($"api/Profesor/{profesorSecure.Id}", new StringContent(JsonSerializer.Serialize(profesorSecure), Encoding.UTF8, "application/json"));

        public async Task<HttpResponseMessage> UpdateProfesorPasswordAsync(ProfileSecurityForm profileSecurityForm) =>
            await httpClient.PatchAsync($"api/Profesor/UpdatePassword", new StringContent(JsonSerializer.Serialize(profileSecurityForm), Encoding.UTF8, "application/json"));
    }
}
