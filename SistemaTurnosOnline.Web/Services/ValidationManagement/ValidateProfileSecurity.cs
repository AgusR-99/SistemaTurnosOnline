using Blazorise;
using SistemaTurnosOnline.Shared.Validators.Contracts;
using System.Net.Http;
using System.Net;
using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Web.Services.ValidationManagement
{
    public class ValidateProfileSecurity : IPasswordValidator
    {
        private readonly HttpClient httpClient;

        public ValidateProfileSecurity(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> IsPasswordValid(string userId, string password)
        {
            var passwordValidationModel = new PasswordValidationModel() { UserId = userId, Password = password };

            var response = await httpClient.PostAsJsonAsync($"api/Profesor/PasswordValidation", passwordValidationModel);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            if (response.StatusCode is HttpStatusCode.UnprocessableEntity or HttpStatusCode.BadRequest)
            {
                return false;
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception(message);
        }
    }
}
