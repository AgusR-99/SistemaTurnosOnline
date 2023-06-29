using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services.ProfesorManagement.Contracts;

namespace SistemaTurnosOnline.Web.Services.ProfesorManagement
{
    public class ProfesorResponseProcessor : IProfesorResponseProcessor
    {
        public async Task<IEnumerable<Profesor>> ProcessProfesorCollectionResponseAsync(HttpResponseMessage response)
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

        public async Task<ProfesorSecure> ProcessProfesorSecureResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default;
                }

                return await response.Content.ReadFromJsonAsync<ProfesorSecure>();
            }
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status: {response.StatusCode} Message: {message}");
        }

        public async Task<Profesor> ProcessProfesorResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default;
                }

                return await response.Content.ReadFromJsonAsync<Profesor>();
            }
            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status: {response.StatusCode} Message: {message}");
        }
    }
}
