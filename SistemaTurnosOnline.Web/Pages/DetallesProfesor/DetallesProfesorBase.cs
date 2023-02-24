using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Extensions;
using SistemaTurnosOnline.Web.Extensions;
using SistemaTurnosOnline.Web.Services.Contracts;
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
        public List<ToastModel> Toasts { get; set; } = new List<ToastModel>
        {
              new ToastModel(
                status: ToastModel.Status.Success,
                id: "toastActualizado",
                headerClass: "bg-success",
                icon: "oi oi-circle-check",
                title: "Actualizacion exitosa",
                time: "Ahora",
                text: "Se ha actualizado el usuario con exito"
                ),
             new ToastModel(
                status: ToastModel.Status.Error,
                id: "toastError",
                headerClass: "bg-danger",
                icon: "oi oi-circle-x",
                title: "Error de server",
                time: "Ahora",
                text: "Se ha producido un error al enviar la solicitud"
                )
        };
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

            var carrerasValues = CarreraService.GetCarrerasValues();

            foreach (var carrera in CarrerasForm.Where(c => c.IsChecked))
            {
                carrerasValues.Add(carrera.Id);
            }

            CarreraService.SetCarrerasValues(carrerasValues);

            SelectedRol = Profesor.Rol;

            var authState = await AuthenticationState;

            UserIdSession = authState.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }

        protected async Task Checkbox_Click(string id)
        {
            try
            {
                var carrerasValues = CarreraService.GetCarrerasValues();

                if (carrerasValues.Contains(id))
                {
                    carrerasValues.Remove(id);
                }
                else
                {
                    carrerasValues.Add(id);
                }

                CarreraService.SetCarrerasValues(carrerasValues);
            }
            catch (Exception ex)
            {
                var toast = Toasts.Find(t => t.status == ToastModel.Status.Error);

                toast.Text = ex.Message;
                await toast.Id.ShowToast(Js);
            }

        }
        protected async Task UpdateProfesor_Click()
        {
            try
            {
                var CarrerasValues = CarreraService.GetCarrerasValues();

                var carrerasId =
                    from carrera in Carreras
                    join carrerasValues in CarrerasValues
                    on carrera.Id equals carrerasValues
                    select carrera.Id;

                Profesor.CarrerasId = carrerasId.ToList();

                Profesor.Rol = SelectedRol;

                var profesorToUpdate = await ProfesorService.UpdateProfesor(Profesor);

                if (profesorToUpdate != null)
                {
                    var toast = Toasts.Find(t => t.status == ToastModel.Status.Success);

                    if (toast != null) await toast.Id.ShowToast(Js);
                }
            }
            catch (Exception ex)
            {
                var toast = Toasts.Find(t => t.status == ToastModel.Status.Error);

                if (toast != null)
                {
                    toast.Text = ex.Message;
                    await toast.Id.ShowToast(Js);
                }
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
            catch (Exception ex)
            {
                var toast = Toasts.Find(t => t.status == ToastModel.Status.Error);

                toast.Text = ex.Message;
                await toast.Id.ShowToast(Js);
            }
        }

        protected async Task ResetPassword_Click()
        {
            try
            {
                NewPassword = await ProfesorService.ResetPassword(Id);

                await PasswordResetModal.ShowModal(Js);
            }
            catch (Exception ex)
            {
                var toast = Toasts.Find(t => t.status == ToastModel.Status.Error);

                toast.Text = ex.Message;
                await toast.Id.ShowToast(Js);
            }
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("profesor/readall");
        }
    }
}
