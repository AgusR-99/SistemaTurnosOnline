using Microsoft.AspNetCore.SignalR.Client;

namespace SistemaTurnosOnline.Web.Hubs.Contracts
{
    public interface ITurnoHubClient
    {
        public Task GetAndSendNextTurno(HubConnection hubConnection);

        public Task SendUpdateQueueState(HubConnection hubConnection);
    }
}
