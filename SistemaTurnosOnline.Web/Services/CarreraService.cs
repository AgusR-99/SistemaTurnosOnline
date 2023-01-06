using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Web.Services.Contracts;

namespace SistemaTurnosOnline.Web.Services
{
    public class CarreraService : ICarreraService
    {
        private readonly HttpClient httpClient;
        public List<string> CarrerasValues { get; set; } = new List<string> { };

        public CarreraService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private async Task<Carrera> ProcessCarreraResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default(Carrera);
                }

                return await response.Content.ReadFromJsonAsync<Carrera>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status: {response.StatusCode} Message: {message}");
            }
        }

        public List<string> GetCarrerasValues()
        {
            return CarrerasValues;
        }

        public void SetCarrerasValues(List<string> carrerasValue)
        {
            CarrerasValues = carrerasValue;
        }

        public async Task<Carrera> CreateCarrera(Carrera carrera)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Carrera", carrera);

                return await ProcessCarreraResponseAsync(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Carrera> DeleteCarrera(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Carrera> GetCarrera(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Carrera>> GetCarreras()
        {
            try
            {
                var response = await httpClient.GetAsync("api/Carrera");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<Carrera>().ToList();
                    }

                    return await response.Content.ReadFromJsonAsync<List<Carrera>>();
                }
                else
                {
                    var message = await response.Content.ReadFromJsonAsync<Carrera>();
                    throw new Exception($"Http status: {response.StatusCode} Message: {message}");
                }
            }
            catch (Exception)
            {
                // Loguear excepcion
                throw;
            }
        }

        public Task<Carrera> UpdateCarrera(Carrera carrera)
        {
            throw new NotImplementedException();
        }
    }
}
