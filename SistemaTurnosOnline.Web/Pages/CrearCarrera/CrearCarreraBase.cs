using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Extensions;
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

        public FluentValidationValidator? _fluentValidationValidator;

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

        protected async Task CreateCarrera_Click()
        {
            try
            {
                Carrera.Id = "";

                var newCarrera = await CarreraService.CreateCarrera(Carrera);

                await Toasts[0].Id.ShowToast(Js);

                Carrera = new Carrera();
            }
            catch (Exception ex)
            {
                Toasts[1].Text = ex.Message;
                await Toasts[1].Id.ShowToast(Js);
            }
        }
    }
}
