using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Extensions;
using SistemaTurnosOnline.Web.Components.ToastComponent.DangerToast;
using SistemaTurnosOnline.Web.Components.ToastComponent.SuccessToast;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications.ToastNotificationText;
using SistemaTurnosOnline.Web.Extensions;
using SistemaTurnosOnline.Web.Services.Contracts;
using SistemaTurnosOnline.Web.Utils;

namespace SistemaTurnosOnline.Web.Pages.AltaProfesor
{
    public class AltaProfesorBase : ComponentBase
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

        public string ModalActivatedId { get; set; } = "activatedModal";
        public string ModalDeletedId { get; set; } = "deletedModal";
        public string ModalAdminPrompt { get; set; } = "adminPromptModal";
        public ProfesorSecure Profesor { get; set; } = new ProfesorSecure();
        public List<Carrera> Carreras { get; set; }
        public List<CarreraForm> CarrerasForm { get; set; }

        public static readonly List<UserRole> userRoles = UserRoleUtils.GetUserRoles();

        private List<string> _checkedCarrerasIds = new();

        public UserRole _selectedRol = userRoles.FindUserRole(UserRole.Guest);
        public UserRole SelectedRol
        {
            get
            {
                return _selectedRol;
            }
            set
            {
                _selectedRol = value;

                if (_selectedRol == UserRole.Admin)
                {
                    ModalAdminPrompt.ShowModal(Js);
                }
            }
        }

        public SuccessToast SuccessSentToast = new
        (
            Id: "success-sent-toast",
            Text: UserToastNotificationText.Activated,
            Title: ToastNotificationTitle.ActivatedTitle
        );

        public DangerToast ServerErrorToast = new
        (
            Id: "server-error-toast",
            Text: GenericToastNotificationText.ServerErrorText,
            Title: ToastNotificationTitle.ServerErrorTitle
        );

        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrWhiteSpace(Id)) return;

            Profesor = await ProfesorService.GetProfesor(Id);

            Carreras = await CarreraService.GetCarreras();

            CarrerasForm = Carreras.Select(carrera => carrera.ConvertToCarreraForm())
                  .ToList();

            try
            {
                CarrerasForm.CheckCarrerasById(Profesor.CarrerasId!);
            }
            catch (Exception ex)
            {
                await ServerErrorToast.ShowServerErrorToast(ex, Js);
            }

            _checkedCarrerasIds = CarrerasForm.GetCheckedCarrerasIds();
        }

        protected async Task Checkbox_Click(string id)
        {
            try
            {
                CarreraFormUtils.ToggleCarreraValue(_checkedCarrerasIds, id);
            }
            catch (Exception ex)
            {
                await ServerErrorToast.ShowServerErrorToast(ex, Js);
            }
        }

        protected async Task ActivateProfesor_Click()
        {
            try
            {
                Profesor.CarrerasId = _checkedCarrerasIds;

                Profesor.Estado = true;

                Profesor.Rol = SelectedRol.ToRoleString();

                var activatedProfesor = await ProfesorService.UpdateProfesor(Profesor);

                await ModalActivatedId.ShowModal(Js);
            }
            catch (Exception ex)
            {
                await ServerErrorToast.ShowServerErrorToast(ex, Js);
            }
        }

        protected async Task DeleteProfesor_Click()
        {
            try
            {
                var deletedProfesor = ProfesorService.DeleteProfesor(Id);

                await ModalDeletedId.ShowModal(Js);
            }
            catch (Exception ex)
            {
                await ServerErrorToast.ShowServerErrorToast(ex, Js);
            }
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("profesor/readinactive");
        }
    }
}
