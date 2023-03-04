using Microsoft.AspNetCore.SignalR.Client;
using SistemaTurnosOnline.Shared.Turnos;

namespace SistemaTurnosOnline.Web.Hubs.Contracts
{
    public interface ITurnoHubClient
    {
        public Task GetAndSendNextTurno(HubConnection hubConnection);
    }
}
