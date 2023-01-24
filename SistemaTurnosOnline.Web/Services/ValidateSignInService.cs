using System.Net;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Validators.Contracts;

namespace SistemaTurnosOnline.Web.Services
{
    public class ValidateSignInService : ISignInValidator
    {
        private readonly HttpClient httpClient;

        public ValidateSignInService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<bool> AccountIsActive(string formDni)
        {
            var response = await httpClient.GetAsync($"api/Profesor/GetByDni/Active/{formDni}");

            if (response.IsSuccessStatusCode)
            {
                return false;
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return true;
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }

        public async Task<bool> IsPasswordValid(SignInForm form, string formPassword)
        {
            var response = await httpClient.GetAsync($"api/Profesor/ValidatePassword/Active/{form.Dni}/{formPassword}");

            if (response.IsSuccessStatusCode)
            {
                return false;
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return true;
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }
    }
}
