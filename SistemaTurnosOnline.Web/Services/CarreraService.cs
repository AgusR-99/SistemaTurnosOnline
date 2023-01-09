using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Web.Services.Contracts;
using System.Text.Json;
using System.Text;

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

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status: {response.StatusCode} Message: {message}");
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

        public async Task<Carrera> DeleteCarrera(string id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/Carrera/{id}");
                
                return await ProcessCarreraResponseAsync(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Carrera> GetCarrera(string id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Carrera/{id}");

                return await ProcessCarreraResponseAsync(response);
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
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

        public async Task<Carrera> UpdateCarrera(Carrera carrera)
        {
            try
            {
                var carreraFormJson = new StringContent(JsonSerializer.Serialize(carrera), Encoding.UTF8, "application/json");

                var response = await httpClient.PatchAsync($"api/Carrera/{carrera.Id}", carreraFormJson);

                return await ProcessCarreraResponseAsync(response);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
