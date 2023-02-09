using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Turnos;
using SistemaTurnosOnline.Web.Extensions;
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

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

        [Parameter]
        public string TableId { get; set; } = "turnosTable";

        public List<string> Headers { get; set; } = new List<string> { "Orden en cola", "Descripcion", ""};

        public IEnumerable<Turno> Turnos { get; set; }

        public string FinalizarTurnoPromptModal { get; set; } = "FinishTaskPromptModal";

        public string FinalizarTurnoExitoModal { get; set; } = "FinishTaskSuccessModal";

        public ToastModel Toast { get; set; } =
            new(
                status: ToastModel.Status.Error,
                id: "toastError",
                headerClass: "bg-danger",
                @class: "toast",
                icon: "oi oi-circle-x",
                title: "Error de server",
                time: "Ahora",
                text: "Se ha producido un error al enviar la solicitud"
            );

        public string TurnoId { get; set; }

        public long Orden { get; set; }

        public long PosicionEnCola { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationState;

            var userId = authState.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            Turnos = await TurnoService.GetTurnosByUserId(userId);

            if (Turnos.Count() != 0) PosicionEnCola = Turnos.First().OrdenEnCola;
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

                if (deletedTurno != null) await FinalizarTurnoExitoModal.ShowModal(Js);
            }
            catch (Exception ex)
            {
                Toast.Text = ex.Message;
                await Toast.Id.ShowToast(Js);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            {
                await Js.InvokeAsync<object>(identifier: "datatableInit", "#" + TableId);
                await base.OnAfterRenderAsync(firstRender);
            }
        }

        //https://datatables.net/forums/discussion/56389/datatables-with-blazor
        public async void Dispose()
        {
            await Js.InvokeAsync<object>(identifier: "datatableRemove", "#" + TableId);
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("turno/user-items", true);
        }
    }
}
