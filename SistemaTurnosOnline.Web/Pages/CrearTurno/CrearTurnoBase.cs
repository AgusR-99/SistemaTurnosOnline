using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Turnos;
using SistemaTurnosOnline.Web.Components.ToastComponent.Parent;
using SistemaTurnosOnline.Web.Extensions;
using SistemaTurnosOnline.Web.Services.Contracts;
using SistemaTurnosOnline.Web.Services.TurnoManagement.Contracts;

namespace SistemaTurnosOnline.Web.Pages.CrearTurno
{
    public class CrearTurnoBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public ITurnoService TurnoService { get; set; }

        [Inject]
        public ICarreraService CarreraService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public TurnoForm TurnoForm { get; set; } = new TurnoForm();

        public List<Carrera> CarrerasProfesor { get; set; }

        public string TurnoCreado_Modal { get; set; } = "createdModal";

        public static string CarreraCheckedNoneValue { get; set; } = "0";

        public string SelectedCarreraId { get; set; } = CarreraCheckedNoneValue;

        public bool UserHasCareers { get; set; } = false;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

        [CascadingParameter(Name = "ServerErrorToast")]
        public ToastModel ServerErrorToast { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationState;

            TurnoForm.UserId = authState.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            CarrerasProfesor = (List<Carrera>)await CarreraService.GetCarrerasByUserId(TurnoForm.UserId);

            if (CarrerasProfesor.Count > 0 && CarrerasProfesor[0] != null)
                UserHasCareers = true;
        }

        protected async Task CreateTurno_Click()
        {
            try
            {
                var authState = await AuthenticationState;

                TurnoForm.UserId = authState.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                TurnoForm.CarreraId = SelectedCarreraId;

                await TurnoService.CreateTurno(TurnoForm);

                await TurnoCreado_Modal.ShowModal(Js);
            }
            catch (Exception ex)
            {
                await ServerErrorToast.Show(Js);
            }
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("turno/user-items");
        }
    }
}