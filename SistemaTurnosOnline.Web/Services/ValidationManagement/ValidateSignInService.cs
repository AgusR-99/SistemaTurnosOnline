using System.Net;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Validators.Contracts;

namespace SistemaTurnosOnline.Web.Services.ValidationManagement
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
                return true;
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }

        public async Task<bool> IsPasswordValid(SignInForm form, string formPassword)
        {
            var response = await httpClient.PostAsJsonAsync($"api/Profesor/SignIn", form);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.BadRequest)
            {
                return false;
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }
    }
}
