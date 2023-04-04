using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Components.ToastComponent.DangerToast;
using SistemaTurnosOnline.Web.Components.ToastComponent.SuccessToast;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications.ToastNotificationText;
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

        public static string ErrorMessage { get; set; } = string.Empty;

        public SuccessToastModel CreatedToast = new
            (
                Id: "success-toast",
                Title: ToastNotificationTitle.CreatedTitle,
                Text: CareerToastNotificationText.CareerCreated
            );

        public DangerToastModel ServerErrorToast = new
            (
                Id: "danger-toast",
                Title: ToastNotificationTitle.ServerErrorTitle,
                Text: ""
            );

        protected async Task CreateCarrera_Click()
        {
            try
            {
                Carrera.Id = "";

                var newCarrera = await CarreraService.CreateCarrera(Carrera);

                await CreatedToast.Show(Js);

                Carrera = new Carrera();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;

                await ServerErrorToast.Show(Js);
            }
        }
    }
}
