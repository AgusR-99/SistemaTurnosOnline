using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Validators.Contracts;
using System.Net;

namespace SistemaTurnosOnline.Web.Services.ValidationManagement
{
    public class ValidateCarreraService : ICarreraValidator
    {
        public HttpClient httpClient { get; }

        public ValidateCarreraService(HttpClient ttpClient)
        {
            httpClient = ttpClient;
        }

        private async Task<bool> ProcessValidationResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return false;
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return true;
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }

        public async Task<bool> NameIsUnique(string name)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Carrera/GetByName/{name}");

                return await ProcessValidationResponseAsync(response);
            }
            catch (Exception)
            {
                // Loguear excepcion
                throw;
            }
        }

        public async Task<bool> NameIsUnique(string name, string id)
        {
            var response = await httpClient.GetAsync($"api/Carrera/GetByName/{name}");

            var carrera = await response.Content.ReadFromJsonAsync<Carrera>();

            return await Task.FromResult(carrera.Id == null || carrera.Id == id);
        }

        public async Task<bool> CodeIsUnique(string code, string id)
        {
            var response = await httpClient.GetAsync($"api/Carrera/GetByCode/{code}");

            var carrera = await response.Content.ReadFromJsonAsync<Carrera>();

            return await Task.FromResult(carrera.Id == null || carrera.Id == id);
        }
    }
}
