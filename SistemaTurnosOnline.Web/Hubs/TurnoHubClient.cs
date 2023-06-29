using Microsoft.AspNetCore.SignalR.Client;
using SistemaTurnosOnline.Shared.Turnos;
using SistemaTurnosOnline.Web.Hubs.Contracts;
using SistemaTurnosOnline.Web.Services;
using SistemaTurnosOnline.Web.Services.TurnoManagement.Contracts;

namespace SistemaTurnosOnline.Web.Hubs
{
    public class TurnoHubClient : ITurnoHubClient
    {
        private readonly ITurnoService turnoService;


        public TurnoHubClient(ITurnoService turnoService)
        {
            this.turnoService = turnoService;
        }

        private async Task Send(Turno turno, HubConnection hubConnection) => await hubConnection.InvokeAsync("Send", turno);

        public async Task GetAndSendNextTurno(HubConnection hubConnection)
        {
            var turnosAllUsers = await turnoService.GetTurnos();

            if (turnosAllUsers.Any())
            {
                var nextTurno = turnosAllUsers.Single(t => t.OrdenEnCola == 1);

                await Send(nextTurno, hubConnection);
            }
        }

        public async Task SendUpdateQueueState(HubConnection hubConnection) => await hubConnection.InvokeAsync("SendUpdateQueueState");
    }
}
