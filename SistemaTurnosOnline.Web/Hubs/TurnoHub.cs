using Microsoft.AspNetCore.SignalR;
using SistemaTurnosOnline.Shared.Turnos;

namespace SistemaTurnosOnline.Web.Hubs
{
    public class TurnoHub : Hub
    {
        public async Task Send(Turno turno)
        {
            await Clients.All.SendAsync("ReceiveInformation", turno);
        }
    }
}
