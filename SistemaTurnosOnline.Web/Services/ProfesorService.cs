using SistemaTurnosOnline.Web.Services.Contracts;
using System.Text.Json;
using System.Text;
using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Web.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly HttpClient httpClient;

        public ProfesorService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private async Task<Profesor> ProcessProfesorResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default(Profesor);
                }

                return await response.Content.ReadFromJsonAsync<Profesor>();
            }
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status: {response.StatusCode} Message: {message}");
        }

        private async Task<ProfesorSecure> ProcessProfesorSecureResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default(ProfesorSecure);
                }

                return await response.Content.ReadFromJsonAsync<ProfesorSecure>();
            }
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status: {response.StatusCode} Message: {message}");
        }

        private async Task<IEnumerable<Profesor>> ProcessProfesorCollectionResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<Profesor>();
                }

                return await response.Content.ReadFromJsonAsync<IEnumerable<Profesor>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }

        public async Task<Profesor> CreateProfesor(ProfesorForm profesorForm)
        {
            var response = await httpClient.PostAsJsonAsync("api/Profesor", profesorForm);

            return await ProcessProfesorResponseAsync(response);
        }

        public async Task<string> ResetPassword(string id)
        {
            var profesorFormJson = new StringContent(JsonSerializer.Serialize(id), Encoding.UTF8, "application/json");

            var response = await httpClient.PatchAsync($"api/Profesor/ResetPassword/{id}", profesorFormJson);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<Profesor> DeleteProfesor(string id)
        {
            var response = await httpClient.DeleteAsync($"api/Profesor/{id}");

            return await ProcessProfesorResponseAsync(response);
        }

        public async Task<ProfesorSecure> GetProfesor(string id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Profesor/{id}");

                return await ProcessProfesorSecureResponseAsync(response);
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<Profesor> GetProfesorByDni(string dni)
        {
            var response = await httpClient.GetAsync($"api/Profesor/GetByDni/{dni}");

            return await ProcessProfesorResponseAsync(response);
        }

        public async Task<IEnumerable<Profesor>> GetProfesores()
        {
            try
            {
                var response = await httpClient.GetAsync("api/Profesor");

                return await ProcessProfesorCollectionResponseAsync(response);
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<Profesor>> GetProfesoresInactive()
        {
            try
            {
                var response = await httpClient.GetAsync("api/Profesor/GetInactive");

                return await ProcessProfesorCollectionResponseAsync(response);
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<Profesor> UpdateProfesor(ProfesorSecure profesorSecure)
        {
            try
            {
                var profesorFormJson = new StringContent(JsonSerializer.Serialize(profesorSecure), Encoding.UTF8, "application/json");

                var response = await httpClient.PatchAsync($"api/Profesor/{profesorSecure.Id}", profesorFormJson);

                return await ProcessProfesorResponseAsync(response);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
