using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared.Turnos;
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

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

        [Parameter]
        public string TableId { get; set; } = "turnosTable";

        public List<string> Headers { get; set; } = new List<string> { "Orden en cola", "Descripcion"};

        public IEnumerable<Turno> Turnos { get; set; }

        public long PosicionEnCola { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationState;

            var userId = authState.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            Turnos = await TurnoService.GetTurnosByUserId(userId);

            PosicionEnCola = Turnos.First().OrdenEnCola;
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
    }
}
