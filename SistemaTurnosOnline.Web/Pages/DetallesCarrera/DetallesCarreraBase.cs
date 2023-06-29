using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Components.ToastComponent.Parent;
using SistemaTurnosOnline.Web.Services.CarreraManagement.Contracts;

namespace SistemaTurnosOnline.Web.Pages.DetallesCarrera
{
    public class DetallesCarreraBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ICarreraService CarreraService { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string ModalActivatedId { get; set; } = "activatedModal";
        [Parameter]
        public string ModalDeletedId { get; set; } = "deletedModal";

        public Carrera Carrera { get; set; } = new Carrera();

        [CascadingParameter(Name = "ServerErrorToast")]
        private ToastModel ServerErrorToast { get; set; }

        public string ModalId { get; set; } = "deletedModal";

        private async Task ShowModal(string id)
        {
            await Js.InvokeVoidAsync(identifier: "showModal", id);
        }

        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return;
            }

            Carrera = await CarreraService.GetCarrera(Id);
        }

        protected async Task UpdateCarrera_Click()
        {
            try
            {
                var carreraToUpdate = await CarreraService.UpdateCarrera(Carrera);

                await ShowModal(ModalActivatedId);
            }
            catch (Exception)
            {
                await ServerErrorToast.Show(Js);
            }
        }

        protected async Task DeleteCarrera_Click()
        {
            try
            {
                var deletedCarrera = CarreraService.DeleteCarrera(Id);

                await ShowModal(ModalId);
            }
            catch (Exception)
            {
                await ServerErrorToast.Show(Js);
            }
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("carrera/readall");
        }
    }
}
