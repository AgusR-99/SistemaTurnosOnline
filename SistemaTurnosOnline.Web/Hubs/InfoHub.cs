using Microsoft.AspNetCore.SignalR;
using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Web.Hubs
{
    public class InfoHub : Hub
    {
        public async Task Send(Profesor data)
        {
            await Clients.All.SendAsync("ReceiveInformation", data);
        }
    }
}
