using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Authentication;
using SistemaTurnosOnline.Web.Services.Contracts;

namespace SistemaTurnosOnline.Web.Pages.SignIn
{
    public class SignInBase : ComponentBase
    {
        [Inject]
        public IProfesorService ProfesorService { get; set; }
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }

        public FluentValidationValidator? _fluentValidationValidator;

        public SignInForm Form { get; set; } = new SignInForm();
        public async Task SignIn_Click()
        {
        // OnValidSubmit callback no esperará automáticamente la validación asíncrona,
        // por eso mismo se debe esperar explicitamente mediante este metodo
        // https://github.com/Blazored/FluentValidation/issues/38
            var valid = await _fluentValidationValidator!.ValidateAsync(options => options.IncludeAllRuleSets());

            if (valid)
            {
                var userAccount = await ProfesorService.GetProfesorByDni(Form.Dni);

                var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;

                await customAuthStateProvider.UpdateAuthenticationState(new UserSession
                {
                    UserId = userAccount.Id,
                    UserRole = userAccount.Rol
                });

                NavigationManager.NavigateTo("/", true); Console.WriteLine("Form Submitted Successfully!");
            }
        }
    }
}
