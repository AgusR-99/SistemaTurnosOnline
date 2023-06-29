using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Components.ToastComponent.Parent;
using SistemaTurnosOnline.Web.Services.CarreraManagement.Contracts;
using SistemaTurnosOnline.Web.Utils.ToastFactoryUtils;
using SistemaTurnosOnline.Web.Utils.ToastFactoryUtils.Creators;

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

        public ToastModel CreatedToast = ToastFactory.CreateToast(new CareerCreatedToastCreator());

        [CascadingParameter(Name = "ServerErrorToast")]
        private ToastModel ServerErrorToast { get; set; }

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
