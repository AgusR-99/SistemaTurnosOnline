using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services.CarreraManagement.Contracts;
using System.Text;
using System.Text.Json;

namespace SistemaTurnosOnline.Web.Services.CarreraManagement
{
    public class CarreraHttpClient : ICarreraHttpClient
    {
        private readonly HttpClient httpClient;

        public CarreraHttpClient(HttpClient httpClient) =>
            this.httpClient = httpClient;

        public async Task<HttpResponseMessage> CreateCarreraAsync(Carrera carrera) =>
            await httpClient.PostAsJsonAsync("api/Carrera", carrera);

        public async Task<HttpResponseMessage> DeleteCarreraAsync(string id) =>
            await httpClient.DeleteAsync($"api/Carrera/{id}");

        public async Task<HttpResponseMessage> GetCarreraAsync(string id) =>
            await httpClient.GetAsync($"api/Carrera/{id}");

        public async Task<HttpResponseMessage> GetCarrerasAsync() =>
            await httpClient.GetAsync("api/Carrera");

        public async Task<HttpResponseMessage> GetCarrerasByUserIdAsync(string userId) =>
            await httpClient.GetAsync($"api/Carrera/GetByUserId/{userId}");

        public async Task<HttpResponseMessage> UpdateCarreraAsync(Carrera carrera) =>
            await httpClient.PatchAsync($"api/Carrera/{carrera.Id}", new StringContent(JsonSerializer.Serialize(carrera), Encoding.UTF8, "application/json"));
    }
}
