using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Extensions;
using SistemaTurnosOnline.Web.Services.Contracts;

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
        [Parameter]
        public string idPassword { get; set; } = "passwordInput";
        public string idPasswordRe { get; set; } = "passwordInputRe";
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return;
            }

            Profesor = await ProfesorService.GetProfesor(Id);
            ProfesorForm = Profesor.ConvertToProfesorForm();
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

        }

        public async Task ShowPassword(string id)
        {
            await Js.InvokeVoidAsync(identifier: "showPassword", id);
        }

        private async Task ShowToast(string id)
        {
            await Js.InvokeVoidAsync(identifier: "showToast", id);
        }

        private async Task ShowModal(string id)
        {
            await Js.InvokeVoidAsync(identifier: "showModal", id);
        }
        private async Task HideModal(string id)
        {
            await Js.InvokeVoidAsync(identifier: "hideModal", id);
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

                ProfesorForm.CarrerasId = carrerasId.ToList();

                var profesorToUpdate = await ProfesorService.UpdateProfesor(ProfesorForm);

                if (profesorToUpdate != null)
                {
                    var toast = Toasts.Find(t => t.status == ToastModel.Status.Success);

                    if (toast != null)
                    {
                        await ShowToast(toast.Id);
                    }
                    else throw new NullReferenceException($"No se ha encontrado {nameof(ToastModel)} con {nameof(ToastModel.Status.Success)}" +
                        $"asegurese que dicho parametro se encuentre presente en la lista");
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
                    $"asegurese que dicho parametro se encuentre presente en la lista"); ErrorMessage = ex.Message;
            }
        }
        protected async Task DeleteProfesor_Click()
        {
            try
            {
                var deletedProfesor = ProfesorService.DeleteProfesor(Id);

                if (deletedProfesor != null)
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
                throw;
            }
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("profesor/readall");
        }
    }
}
