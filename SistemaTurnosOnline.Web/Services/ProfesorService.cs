using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Web.Services.Contracts;
using System.Text.Json;
using System.Text;

namespace SistemaTurnosOnline.Web.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly HttpClient httpClient;

        public ProfesorService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Profesor> CreateProfesor(ProfesorForm profesorForm)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Profesor", profesorForm);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(Profesor);
                    }

                    return await response.Content.ReadFromJsonAsync<Profesor>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {response.StatusCode} Message: {message}");
                }
            }
            catch (Exception)
            {
                // Loguear excepcion
                throw;
            }
        }

        public Task DeleteProfesor(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Profesor> GetProfesor(string id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Profesor/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return default(Profesor);
                    }

                    return await response.Content.ReadFromJsonAsync<Profesor>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<Profesor>> GetProfesores()
        {
            try
            {
                var response = await httpClient.GetAsync("api/Profesor");

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
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<Profesor> UpdateProfesor(ProfesorForm profesorForm)
        {
            try
            {
                var profesorFormJson = new StringContent(JsonSerializer.Serialize(profesorForm), Encoding.UTF8, "application/json");

                var response = await httpClient.PatchAsync($"api/Profesor/{profesorForm.Id}", profesorFormJson);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Profesor>();
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
