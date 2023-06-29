using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services.CarreraManagement.Contracts;

namespace SistemaTurnosOnline.Web.Services.CarreraManagement
{
    public class CarreraResponseProcessor : ICarreraResponseProcessor
    {
        public async Task<Carrera> ProcessCarreraResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default;
                }

                return await response.Content.ReadFromJsonAsync<Carrera>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status: {response.StatusCode} Message: {message}");
        }

        public async Task<List<Carrera>> ProcessCarrerasResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<Carrera>().ToList();
                }

                return await response.Content.ReadFromJsonAsync<List<Carrera>>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status: {response.StatusCode} Message: {message}");
        }
    }
}
