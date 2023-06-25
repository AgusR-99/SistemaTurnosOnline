using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Turnos;
using SistemaTurnosOnline.Web.Extensions;
using SistemaTurnosOnline.Web.Hubs.Contracts;
using SistemaTurnosOnline.Web.Services.Contracts;
using System.Security.Claims;

namespace SistemaTurnosOnline.Web.Pages.ListarTurnosUsuario
{
    public class ListarTurnosUsuarioBase : ComponentBase, IDisposable
    {
        [Inject]
        public ITurnoService TurnoService { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ITurnoHubClient TurnoHubClient { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

        [Parameter]
        public string TableId { get; set; } = "turnosTable";

        public List<string> Headers { get; set; } = new List<string> { "Orden en cola", "Descripcion", ""};

        public IEnumerable<Turno> Turnos { get; set; }

        public string FinalizarTurnoPromptModal { get; set; } = "FinishTaskPromptModal";

        public string FinalizarTurnoExitoModal { get; set; } = "FinishTaskSuccessModal";

        private HubConnection HubConnection;


        private HubConnection TurnoQueueUpdateHubConnection;
        
        public ToastModelLegacy Toast { get; set; } =
            new(
                status: ToastModelLegacy.Status.Error,
                id: "toastError",
                headerClass: "bg-danger",
                icon: "oi oi-circle-x",
                title: "Error de server",
                time: "Ahora",
                text: "Se ha producido un error al enviar la solicitud"
            );

        public string TurnoId { get; set; }

        public long Orden { get; set; }

        public long PosicionEnCola { get; set; }
        public long TurnosRestantes { get; set; }

        private async Task GetRemainingTurnsInQueueForUser(string userId)
        {
            Turnos = await TurnoService.GetTurnosByUserId(userId);

            if (Turnos.Count() != 0) PosicionEnCola = Turnos.First().OrdenEnCola;

            TurnosRestantes = PosicionEnCola - 1;
        }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationState;

            var userId = AuthStateUtils.GetUserIdFromAuthState(authState);

            await GetRemainingTurnsInQueueForUser(userId);

            HubConnection = HubConnectionFactory.CreateHubConnection("/turnohub", NavigationManager);

            TurnoQueueUpdateHubConnection = HubConnectionFactory.CreateHubConnection("/turnoqueuehub", NavigationManager);

            TurnoQueueUpdateHubConnection.On("UpdateQueue", async () =>
            {
                await GetRemainingTurnsInQueueForUser(userId);

                await InvokeAsync(StateHasChanged);
            });

            await HubConnection.StartAsync();

            await TurnoQueueUpdateHubConnection.StartAsync();
        }

        protected async Task FinishTaskPrompt_Click(string turnoId, long orden)
        {
            TurnoId = turnoId;
            Orden = orden;

            await FinalizarTurnoPromptModal.ShowModal(Js);
        }

        protected async Task FinishTask_Click()
        {
            try
            {
                var deletedTurno = await TurnoService.DeleteTurno(TurnoId);

                if (deletedTurno != null)
                {
                    if (deletedTurno.OrdenEnCola == 1)
                    {
                        await TurnoHubClient.GetAndSendNextTurno(HubConnection);
                    }

                    await TurnoHubClient.SendUpdateQueueState(TurnoQueueUpdateHubConnection);

                    var newTurnos = Turnos.ToList();

                    newTurnos.Remove(newTurnos
                        .First(turno => turno.Id == deletedTurno.Id));

                    if (!newTurnos.Any())
                    {
                        await Js.InvokeAsync<object>(identifier: "datatableRemove", "#" + TableId);
                    }

                    await InvokeAsync(StateHasChanged);
                }
                if (deletedTurno != null)
                {
                    if (deletedTurno.OrdenEnCola == 1)
                    {
                        await TurnoHubClient.GetAndSendNextTurno(HubConnection);
                    }

                    await TurnoHubClient.SendUpdateQueueState(TurnoQueueUpdateHubConnection);

                    await FinalizarTurnoExitoModal.ShowModal(Js);
                }
            }
            catch (Exception ex)
            {
                Toast.Text = ex.Message;
                await Toast.Id.ShowToast(Js);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (Turnos is not null && !firstRender)
            {
                await Js.InvokeAsync<object>(identifier: "datatableInit", "#" + TableId);
                await base.OnAfterRenderAsync(firstRender);
            }
        }

        //https://datatables.net/forums/discussion/56389/datatables-with-blazor
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

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("turno/user-items", true);
        }


        public async ValueTask DisposeAsync()
        {
            await HubConnection.DisposeAsync();
        }
    }
}
