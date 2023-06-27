using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services;
using SistemaTurnosOnline.Web.Services.Contracts;

namespace SistemaTurnosOnline.Web.Pages.ListarProfesoresInactivos
{
    public class ListarProfesoresInactivosBase : ComponentBase, IAsyncDisposable, IDisposable
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public IProfesorService ProfesorService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        public List<Profesor> Profesores { get; set; }
        public List<string> Headers { get; set; } = new List<string> { "DNI", "Nombre", "Email", "" };

        [Parameter]
        public string TableId { get; set; } = "tabla-Profesores";

        private HubConnection HubConnection { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Profesores = (await ProfesorService.GetProfesoresInactive()).ToList();

            HubConnection = new HubConnectionBuilder().WithUrl(NavigationManager.ToAbsoluteUri("/inactiveUsersHub"))
                .Build();

            HubConnection.On<Profesor>("ReceiveInformation", (receiveInfo) =>
            {
                Profesores.Add(receiveInfo);

                Js.InvokeAsync<object>(identifier: "datatableRemoveSoft", "#" + TableId);

                InvokeAsync(StateHasChanged);
            });
            await HubConnection.StartAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (Profesores is not null && !firstRender)
            {
                await Js.InvokeAsync<object>(identifier: "datatableInit", "#" + TableId);
                await base.OnAfterRenderAsync(firstRender);
            }
        }

        public async ValueTask DisposeAsync()
        {
            await HubConnection.DisposeAsync();
        }

        public async void Dispose()
        {
            try
            {
                await Js.InvokeAsync<object>(identifier: "datatableRemove", "#" + TableId);
            }
            catch
            {

            }
        }
    }
}
