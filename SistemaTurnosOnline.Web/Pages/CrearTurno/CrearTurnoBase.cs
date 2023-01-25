using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Extensions;
using SistemaTurnosOnline.Web.Services.Contracts;

namespace SistemaTurnosOnline.Web.Pages.CrearTurno
{
    public class CrearTurnoBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public ITurnoService TurnoService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public TurnoForm TurnoForm { get; set; } = new TurnoForm();

        public string TurnoCreado_Modal { get; set; } = "createdModal";

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

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

        protected async Task CreateTurno_Click()
        {
            try
            {
                var authState = await AuthenticationState;
                TurnoForm.UserId = authState.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await TurnoService.CreateTurno(TurnoForm);

                await TurnoCreado_Modal.ShowModal(Js);
            }
            catch (Exception ex)
            {
                Toast.Text = ex.Message;
                await Toast.Id.ShowToast(Js);
            }
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("turno/readall");
        }
    }
}