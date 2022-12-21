using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Web.Services.Contracts;
using System.Collections.Generic;
using System.Runtime.Serialization;

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

        public List<string> GetCarrerasValues()
        {
            return CarrerasValues;
        }

        public void SetCarrerasValues(List<string> carrerasValue)
        {
            CarrerasValues = carrerasValue;
        }

        public Task<Carrera> CreateCarrera(Carrera carrera)
        {
            throw new NotImplementedException();
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

                    List < Carrera > carreraList = await response.Content.ReadFromJsonAsync<List<Carrera>>();
                    return carreraList;
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
