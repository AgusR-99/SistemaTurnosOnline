using SistemaTurnosOnline.Shared.Turnos;
using System.Text.Json;
using System.Text;
using SistemaTurnosOnline.Web.Services.TurnoManagement.Contracts;

namespace SistemaTurnosOnline.Web.Services.TurnoManagement
{
    public class TurnoService : ITurnoService
    {
        private readonly TurnoHttpClient httpClient;
        private readonly TurnoResponseProcessor responseProcessor;

        public TurnoService(TurnoHttpClient httpClient, TurnoResponseProcessor responseProcessor)
        {
            this.httpClient = httpClient;
            this.responseProcessor = responseProcessor;
        }

        public async Task<IEnumerable<Turno>> GetTurnos()
        {
            var response = await httpClient.GetTurnosAsync();

            return await responseProcessor.ProcessTurnoCollectionResponseAsync(response);
        }

        public async Task<Turno> GetTurno(string id)
        {
            var response = await httpClient.GetTurnoAsync(id);

            return await responseProcessor.ProcessTurnoResponseAsync(response);
        }

        public async Task<IEnumerable<Turno>> GetTurnosByUserId(string userId)
        {
            var response = await httpClient.GetTurnosByUserIdAsync(userId);

            return await responseProcessor.ProcessTurnoCollectionResponseAsync(response);
        }

        public async Task<Turno> CreateTurno(TurnoForm turnoForm)
        {
            var response = await httpClient.CreateTurnoAsync(turnoForm);

            return await responseProcessor.ProcessTurnoResponseAsync(response);
        }

        public async Task<Turno> UpdateTurno(Turno turno)
        {
            var response = await httpClient.UpdateTurnoAsync(turno);

            return await responseProcessor.ProcessTurnoResponseAsync(response);
        }

        public async Task<Turno> DeleteTurno(string id)
        {
            var response = await httpClient.DeleteTurnoAsync(id);

            return await responseProcessor.ProcessTurnoResponseAsync(response);
        }
    }
}
