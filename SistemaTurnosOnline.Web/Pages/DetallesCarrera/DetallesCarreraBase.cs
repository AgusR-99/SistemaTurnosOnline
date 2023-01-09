using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Web.Services.Contracts;

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
        public List<ToastModel> Toasts { get; set; } = new List<ToastModel>
        {
            new ToastModel(
                status: ToastModel.Status.Success,
                id: "toastActualizado",
                headerClass: "bg-success",
                @class: "toast",
                icon: "oi oi-circle-check",
                title: "Actualizacion exitosa",
                time: "Ahora",
                text: "Se ha enviado actualizado el usuario con exito"
            ),
            new ToastModel(
                status: ToastModel.Status.Error,
                id: "toastError",
                headerClass: "bg-danger",
                @class: "toast",
                icon: "oi oi-circle-x",
                title: "Error de server",
                time: "Ahora",
                text: "Se ha producido un error al enviar la solicitud"
            )
        };

        public string ModalId { get; set; } = "deletedModal";

        private async Task ShowToast(string id)
        {
            await Js.InvokeVoidAsync(identifier: "showToast", id);
        }

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

                if (carreraToUpdate != null)
                {
                    await ShowModal(ModalActivatedId);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                var toast = Toasts.Find(t => t.status == ToastModel.Status.Error);

                if (toast != null)
                {
                    await ShowToast(toast.Id);
                }
                else throw new NullReferenceException($"No se ha encontrado {nameof(ToastModel)} con {nameof(ToastModel.Status.Error)}:" +
                                                      $"asegurese que dicho parametro se encuentre presente en la lista");
            }

        }

        protected async Task DeleteCarrera_Click()
        {
            try
            {
                var deletedCarrera = CarreraService.DeleteCarrera(Id);

                if (deletedCarrera != null)
                {
                    await ShowModal(ModalId);
                }
                else
                {
                    var toast = Toasts.Find(t => t.status == ToastModel.Status.Error);

                    if (toast != null)
                    {
                        await ShowToast(toast.Id);
                    }
                    else throw new NullReferenceException($"No se ha encontrado {nameof(ToastModel)} con {nameof(ToastModel.Status.Error)}:" +
                                                          $"asegurese que dicho parametro se encuentre presente en la lista");
                }
            }
            catch (Exception)
            {
                var toast = Toasts.Find(t => t.status == ToastModel.Status.Error);

                if (toast != null)
                {
                    await ShowToast(toast.Id);
                }
                else throw new NullReferenceException($"No se ha encontrado {nameof(ToastModel)} con {nameof(ToastModel.Status.Error)}:" +
                                                      $"asegurese que dicho parametro se encuentre presente en la lista");
            }
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("carrera/readall");
        }
    }
}
