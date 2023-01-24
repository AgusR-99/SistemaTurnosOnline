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

        public SignInForm Form { get; set; } = new SignInForm();
        public async Task SignIn_Click()
        {
            var userAccount = await ProfesorService.GetProfesorByDni(Form.Dni);

            if (userAccount == null || userAccount.Password != Form.Password || userAccount.Estado == false) return;

            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;

            await customAuthStateProvider.UpdateAuthenticationState(new UserSession
                {
                    Username = userAccount.Dni,
                    UserRole = userAccount.Rol
                });

            NavigationManager.NavigateTo("/", true);
        }
    }
}
