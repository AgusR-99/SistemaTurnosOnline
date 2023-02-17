using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Extensions;
using SistemaTurnosOnline.Web.Services.Contracts;
using System.Security.Claims;

namespace SistemaTurnosOnline.Web.Pages.PerfilSeguridad
{
    public class PerfilSeguridadBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IProfesorService ProfesorService { get; set; }

        public ProfileSecurityForm ProfileSecurityForm { get; set; } = new ProfileSecurityForm();

        public FluentValidationValidator? _fluentValidationValidator;

        public string PasswordActualizado_Modal { get; set; } = "updatedModal";

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationState { get; set; }

        public ToastModel Toast { get; set; } =
            new(
                status: ToastModel.Status.Error,
                id: "toastError",
                headerClass: "bg-danger",
                icon: "oi oi-circle-x",
                title: "Error de server",
                time: "Ahora",
                text: "Se ha producido un error al enviar la solicitud"
        );

        protected async override Task OnInitializedAsync()
        {
            ProfileSecurityForm.Id = (await AuthenticationState)
                    .User
                    .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)
                    .Value;
        }

        protected async Task UpdateProfesor_Click()
        {
            try
            {
                var valid = await _fluentValidationValidator!.ValidateAsync(options => options.IncludeAllRuleSets());

                if (valid)
                {
                    var updatedProfesor = await ProfesorService.UpdateProfesorPassword(ProfileSecurityForm);

                    if (updatedProfesor != null)
                    {
                        await PasswordActualizado_Modal.ShowModal(Js);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Text = ex.Message;
                await Toast.Id.ShowToast(Js);
            }
            
        }

        protected void Navigate_Click()
        {
            NavigationManager.NavigateTo("/turno/user-items");
        }
    }
}
