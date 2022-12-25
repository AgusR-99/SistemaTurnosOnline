using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Web.Services.Contracts;

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

        public Task<Profesor> GetProfesor(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Profesor>> GetProfesores()
        {
            throw new NotImplementedException();
        }

        public Task<Profesor> UpdateProfesor(Profesor profesor)
        {
            throw new NotImplementedException();
        }
    }
}
