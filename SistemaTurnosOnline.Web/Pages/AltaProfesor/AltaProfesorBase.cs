using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Extensions;
using SistemaTurnosOnline.Web.Components.ModalComponent;
using SistemaTurnosOnline.Web.Components.ModalComponent.ModalNotifications;
using SistemaTurnosOnline.Web.Components.ToastComponent.DangerToast;
using SistemaTurnosOnline.Web.Components.ToastComponent.Parent;
using SistemaTurnosOnline.Web.Components.ToastComponent.SuccessToast;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications.ToastNotificationText;
using SistemaTurnosOnline.Web.Extensions;
using SistemaTurnosOnline.Web.Services.CarreraManagement.Contracts;
using SistemaTurnosOnline.Web.Services.ProfesorManagement.Contracts;
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
                    SelectedAdminPrivilegesModal.Show(Js);
                }
            }
        }

        [CascadingParameter(Name = "ServerErrorToast")]
        private ToastModel ServerErrorToast { get; set; }

        public ModalModel IrreversibleActionModal = new
        (
            id: "irreversible-action-modal",
            label: "irreversible-action-modal-label",
            headerText: ModalHeaderText.IrreversibleAction,
            alertText: ModalAlertText.IrreversibleAction
        );

        public ModalModel SelectedAdminPrivilegesModal = new
        (
            id: "selected-admin-privileges-modal",
            label: "selected-admin-privileges-label",
            headerText: ModalHeaderText.SelectedAdminPrivileges,
            alertText: ModalAlertText.SelectedAdminPrivileges
        );

        public ModalModel RejectedUserModal = new
        (
            id: "rejected-user-modal",
            label: "rejected-user-label",
            headerText: ModalHeaderText.SuccessfulAction,
            alertText: ModalAlertText.RejectedUser
        );

        public ModalModel ApprovedUserModal = new
        (
            id: "approved-user-modal",
            label: "approved-user-label",
            headerText: ModalHeaderText.SuccessfulAction,
            alertText: ModalAlertText.ApprovedUser
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
            catch (Exception)
            {
                await ServerErrorToast.Show(Js);
            }

            _checkedCarrerasIds = CarrerasForm.GetCheckedCarrerasIds();
        }

        protected async Task Checkbox_Click(string id)
        {
            try
            {
                CarreraFormUtils.ToggleCarreraValue(_checkedCarrerasIds, id);
            }
            catch (Exception)
            {
                await ServerErrorToast.Show(Js);
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

                await ApprovedUserModal.Show(Js);
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
                var deletedProfesor = ProfesorService.DeleteProfesor(Id);

                await RejectedUserModal.Show(Js);
            }
            catch (Exception)
            {
                await ServerErrorToast.Show(Js);
            }
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("profesor/readinactive");
        }
    }
}
