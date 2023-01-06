using Blazorise;
using SistemaTurnosOnline.Shared.Validators.Contracts;
using System.Net;
using System.Net.Http;

namespace SistemaTurnosOnline.Web.Services
{
    public class ValidateCarreraService : ICarreraValidator
    {
        public HttpClient httpClient { get; }

        public ValidateCarreraService(HttpClient ttpClient)
        {
            this.httpClient = ttpClient;
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
    }
}
