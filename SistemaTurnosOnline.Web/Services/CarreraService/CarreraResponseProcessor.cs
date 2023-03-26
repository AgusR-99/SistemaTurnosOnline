using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Web.Services.CarreraService
{
    public class CarreraResponseProcessor
    {
        public async Task<Carrera> ProcessCarreraResponseAsync(HttpResponseMessage response)
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
