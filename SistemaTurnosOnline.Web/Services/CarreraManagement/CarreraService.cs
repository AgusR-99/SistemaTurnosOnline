using System.Text.Json;
using System.Text;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services.CarreraManagement.Contracts;

namespace SistemaTurnosOnline.Web.Services.CarreraManagement
{
    public class CarreraService : ICarreraService
    {
        private readonly CarreraHttpClient httpClient;
        private readonly CarreraResponseProcessor responseProcessor;

        public CarreraService(CarreraHttpClient httpClient, CarreraResponseProcessor responseProcessor, CarreraListManager listManager)
        {
            this.httpClient = httpClient;
            this.responseProcessor = responseProcessor;
        }

        public async Task<Carrera> CreateCarrera(Carrera carrera)
        {
            var response = await httpClient.CreateCarreraAsync(carrera);

            return await responseProcessor.ProcessCarreraResponseAsync(response);
        }

        public async Task<Carrera> DeleteCarrera(string id)
        {
            var response = await httpClient.DeleteCarreraAsync(id);

            return await responseProcessor.ProcessCarreraResponseAsync(response);
        }

        public async Task<Carrera> GetCarrera(string id)
        {
            var response = await httpClient.GetCarreraAsync(id);

            return await responseProcessor.ProcessCarreraResponseAsync(response);
        }

        public async Task<List<Carrera>> GetCarreras()
        {
            var response = await httpClient.GetCarrerasAsync();

            return await responseProcessor.ProcessCarrerasResponseAsync(response);
        }

        public async Task<List<Carrera>> GetCarrerasByUserId(string userId)
        {
            var response = await httpClient.GetCarrerasByUserIdAsync(userId);

            return await responseProcessor.ProcessCarrerasResponseAsync(response);
        }

        public async Task<Carrera> UpdateCarrera(Carrera carrera)
        {
            var response = await httpClient.UpdateCarreraAsync(carrera);

            return await responseProcessor.ProcessCarreraResponseAsync(response);
        }
    }
}
