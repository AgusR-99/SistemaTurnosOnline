using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Extensions;
using SistemaTurnosOnline.Web.Components.ToastComponent.Parent;
using SistemaTurnosOnline.Web.Extensions;
using SistemaTurnosOnline.Web.Services.CarreraManagement;
using SistemaTurnosOnline.Web.Services.Contracts;
using SistemaTurnosOnline.Web.Utils.ToastFactoryUtils;
using SistemaTurnosOnline.Web.Utils.ToastFactoryUtils.Creators;
using System.Security.Claims;

namespace SistemaTurnosOnline.Web.Pages.DetallesProfesor
{
    public class DetallesProfesorBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IProfesorService ProfesorService { get; set; }
        [Inject]
        public ICarreraService CarreraService { get; set; }
        [Inject]
        public CarreraListManager CarreraListManager { get; set; }

        [Parameter]
        public string Id { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

        public ProfesorSecure Profesor { get; set; } = new ProfesorSecure();
        public List<Carrera> Carreras { get; set; }
        public List<CarreraForm> CarrerasForm { get; set; }
        public List<string> Roles { get; set; } = new() { "Admin", "Guest" };
        public bool FirstRender { get; set; } = true;
        public string _SelectedRol = "Guest";
        public string SelectedRol
        {
            get
            {
                return _SelectedRol;
            }
            set
            {
                _SelectedRol = value;

                if (_SelectedRol == "Admin" && !FirstRender)
                {
                    ModalAdminPrompt.ShowModal(Js);
                }
                else if (FirstRender) FirstRender = false;
            }
        }
        public string UserIdSession { get; set; }

        [CascadingParameter(Name = "ServerErrorToast")]
        private ToastModel ServerErrorToast { get; set; }

        public ToastModel UserUpdatedToast = ToastFactory.CreateToast(new UserUpdatedToastCreator());

        public string EliminarProfesorModal { get; set; } = "deletedModal";

        public string PasswordResetModal { get; set; } = "resetPasswordModal";

        public string ModalAdminPrompt { get; set; } = "adminPromptModal";

        public string NewPassword { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return;
            }

            Profesor = await ProfesorService.GetProfesor(Id);
            Carreras = await CarreraService.GetCarreras();

            CarrerasForm = Carreras.Select(carrera => carrera.ConvertToCarreraForm())
                  .ToList();

            if (Profesor.CarrerasId != null)
            {
                CarrerasForm.Where(carrera => Profesor.CarrerasId.Contains(carrera.Id))
                .ToList()
                .ForEach(carrera => carrera.IsChecked = true);
            }

            var carrerasValues = CarreraListManager.GetCarrerasValues();

            foreach (var carrera in CarrerasForm.Where(c => c.IsChecked))
            {
                carrerasValues.Add(carrera.Id);
            }

            CarreraListManager.SetCarrerasValues(carrerasValues);

            SelectedRol = Profesor.Rol;

            var authState = await AuthenticationState;

            UserIdSession = authState.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }

        protected async Task Checkbox_Click(string id)
        {
            try
            {
                var carrerasValues = CarreraListManager.GetCarrerasValues();

                if (carrerasValues.Contains(id))
                {
                    carrerasValues.Remove(id);
                }
                else
                {
                    carrerasValues.Add(id);
                }

                CarreraListManager.SetCarrerasValues(carrerasValues);
            }
            catch (Exception)
            {
                await ServerErrorToast.Show(Js);
            }

        }
        protected async Task UpdateProfesor_Click()
        {
            try
            {
                var CarrerasValues = CarreraListManager.GetCarrerasValues();

                var carrerasId =
                    from carrera in Carreras
                    join carrerasValues in CarrerasValues
                    on carrera.Id equals carrerasValues
                    select carrera.Id;

                Profesor.CarrerasId = carrerasId.ToList();

                Profesor.Rol = SelectedRol;

                var profesorToUpdate = await ProfesorService.UpdateProfesor(Profesor);

                await UserUpdatedToast.Show(Js);
            }
            catch (Exception)
            {
                await ServerErrorToast.Show(Js);
            }
        }
        protected async Task DeleteProfesor_Click()
        {
            try
            {
                var deletedProfesor = await ProfesorService.DeleteProfesor(Id);

                if (deletedProfesor != null)
                {
                    await EliminarProfesorModal.ShowModal(Js);
                }
            }
            catch (Exception)
            {
                await ServerErrorToast.Show(Js);
            }
        }

        protected async Task ResetPassword_Click()
        {
            try
            {
                NewPassword = await ProfesorService.ResetPassword(Id);

                await PasswordResetModal.ShowModal(Js);
            }
            catch (Exception)
            {
                await ServerErrorToast.Show(Js);
            }
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("profesor/readall");
        }
    }
}
