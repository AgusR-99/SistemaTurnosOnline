using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services.Contracts;

namespace SistemaTurnosOnline.Web.Pages.CrearCarrera
{
    public class CrearCarreraBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        [Inject]
        public ICarreraService CarreraService { get; set; }

        public Carrera Carrera { get; set; } = new Carrera();

        public List<ToastModel> Toasts { get; set; } = new()
        {
            new ToastModel(
                status: ToastModel.Status.Success,
                id: "toastActualizado",
                headerClass: "bg-success",
                icon: "oi oi-circle-check",
                title: "Actualizacion exitosa",
                time: "Ahora",
                text: "Se ha creado la carrera con exito"
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
        private async Task ShowToast(string id)
        {
            await Js.InvokeVoidAsync(identifier: "showToast", id);
        }

        protected async Task CreateCarrera_Click()
        {
            try
            {
                Carrera.Id = "";

                var newCarrera = await CarreraService.CreateCarrera(Carrera);

                if (newCarrera != null)
                {
                    var toast = Toasts.Find(t => t.status == ToastModel.Status.Success);

                    if (toast != null)
                    {
                        await ShowToast(toast.Id);
                        typeof(Carrera).GetProperties()
                            .Where(p => p.PropertyType == typeof(string))
                            .ToList()
                            .ForEach(p => p.SetValue(Carrera, string.Empty, null));
                    }
                    else throw new NullReferenceException($"No se ha encontrado {nameof(ToastModel)} con {nameof(ToastModel.Status.Success)}" +
                                                          $"asegurese que dicho parametro se encuentre presente en la lista");
                }
                else
                {
                    throw new Exception();
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
    }
}
