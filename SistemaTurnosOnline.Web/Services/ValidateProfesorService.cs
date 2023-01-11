using SistemaTurnosOnline.Shared.Validators.Contracts;
using System.Net;

namespace SistemaTurnosOnline.Web.Services
{
    public class ValidateProfesorService : IValidateProfesor
    {
        private readonly HttpClient httpClient;

        public ValidateProfesorService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private async Task<bool> responseStatus (HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return false;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return true;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }

        public async Task<bool> DniIsUnique(string dni, string id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Profesor/GetByDni/{id}/{dni}");

                return await responseStatus(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> EmailIsUnique(string email, string id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Profesor/GetByEmail/{id}/{email}");

                return await responseStatus(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> EmailIsUnique(string email)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Profesor/GetByEmail/{email}");

                return await responseStatus(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DniIsUnique(string dni)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Profesor/GetByDni/{dni}");

                return await responseStatus(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
