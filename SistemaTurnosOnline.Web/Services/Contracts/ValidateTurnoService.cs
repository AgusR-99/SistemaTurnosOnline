using System.Net;
using SistemaTurnosOnline.Shared.Turnos;
using SistemaTurnosOnline.Shared.Validators.Contracts;

namespace SistemaTurnosOnline.Web.Services.Contracts
{
    public class ValidateTurnoService : ITurnoValidator
    {
        private readonly HttpClient httpClient;

        public ValidateTurnoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> BeNotOutOfBounds(TurnoListado turnoListado, string orden)
        {
            var response = await httpClient.GetAsync($"api/Turno/ValidateOutOfBounds/{orden}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                return false;
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }
    }
}
