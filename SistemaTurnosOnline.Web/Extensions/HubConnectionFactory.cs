using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Components.Authorization;

namespace SistemaTurnosOnline.Web.Extensions
{
    public static class HubConnectionFactory
    {
        /// <summary>
        /// Creates a new instance of HubConnection for the specified hub URL using the provided NavigationManager.
        /// </summary>
        /// <param name="hubUrl">The URL of the hub.</param>
        /// <param name="navigationManager">The NavigationManager instance to use for building hub connection URLs.</param>
        /// <returns>The newly created HubConnection instance.</returns>
        public static HubConnection CreateHubConnection(string hubUrl, NavigationManager navigationManager)
        {
            return new HubConnectionBuilder()
                .WithUrl(navigationManager.ToAbsoluteUri(hubUrl))
                .Build();
        }
    }
}
