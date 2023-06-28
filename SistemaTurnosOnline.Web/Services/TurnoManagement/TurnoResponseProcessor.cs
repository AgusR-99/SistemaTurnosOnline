using SistemaTurnosOnline.Shared.Turnos;
using SistemaTurnosOnline.Web.Services.TurnoManagement.Contracts;

namespace SistemaTurnosOnline.Web.Services.TurnoManagement
{
    public class TurnoResponseProcessor : ITurnoResponseProcessor
    {
        public async Task<IEnumerable<Turno>> ProcessTurnoCollectionResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return Enumerable.Empty<Turno>();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Turno>>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status: {response.StatusCode} Message: {message}");
        }

        public async Task<Turno> ProcessTurnoResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default;
                }

                return await response.Content.ReadFromJsonAsync<Turno>();
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status: {response.StatusCode} Message: {message}");
        }
    }
}
