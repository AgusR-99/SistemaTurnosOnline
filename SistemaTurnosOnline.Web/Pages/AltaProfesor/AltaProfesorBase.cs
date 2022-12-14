using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Api.Extensions;
using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services.Contracts;

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
        [Parameter]
        public string ModalActivatedId { get; set; } = "activatedModal";
        public string ModalDeletedId { get; set; } = "deletedModal";
        [Parameter]
        public string idPassword { get; set; } = "passwordInput";
        [Parameter]
        public string idPasswordRe { get; set; } = "passwordInputRe";
        public Profesor Profesor { get; set; }
        public ProfesorForm ProfesorForm { get; set; } = new ProfesorForm();
        public List<Carrera> Carreras { get; set; }
        public List<CarreraForm> CarrerasForm { get; set; }
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
                text: "Se ha activado el usuario con exito"
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

        private async Task ShowModal(string id)
        {
            await Js.InvokeVoidAsync(identifier: "showModal", id);
        }
        private async Task ShowToast(string id)
        {
            await Js.InvokeVoidAsync(identifier: "showToast", id);
        }

        protected async Task ShowPassword(string id)
        {
            await Js.InvokeVoidAsync(identifier: "showPassword", id);
        }

        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return;
            }

            Profesor = await ProfesorService.GetProfesor(Id);
            Carreras = await CarreraService.GetCarreras();

            ProfesorForm = Profesor.ConvertToProfesorForm();

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
        }

        protected void Checkbox_Click(string id)
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
            catch (Exception)
            {
                throw;
            }

        }

        protected async Task ActivateProfesor_Click()
        {
            try
            {
                ProfesorForm.Estado = true;

                var activatedProfesor = await ProfesorService.UpdateProfesor(ProfesorForm);

                if (activatedProfesor != null)
                {
                    await ShowModal(ModalActivatedId);
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

        protected async Task DeleteProfesor_Click()
        {
            try
            {
                var deletedProfesor = ProfesorService.DeleteProfesor(Id);

                if (deletedProfesor != null)
                {
                    await ShowModal(ModalDeletedId);
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
                throw;
            }
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("profesor/readinactive");
        }
    }
}
