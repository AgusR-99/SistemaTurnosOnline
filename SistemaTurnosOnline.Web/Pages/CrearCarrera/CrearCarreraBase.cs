using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Components.ToastComponent.DangerToast;
using SistemaTurnosOnline.Web.Components.ToastComponent.Parent;
using SistemaTurnosOnline.Web.Components.ToastComponent.SuccessToast;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications;
using SistemaTurnosOnline.Web.Components.ToastComponent.ToastNotifications.ToastNotificationText;
using SistemaTurnosOnline.Web.Services.Contracts;
using SistemaTurnosOnline.Web.Utils;

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

        public ToastModel CreatedToast = ToastFactoryLegacy.CreateCareerCreatedToast();

        [CascadingParameter(Name = "ServerErrorToast")]
        public ToastModel ServerErrorToast { get; set; }

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
