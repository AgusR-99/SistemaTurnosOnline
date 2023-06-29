using System.Text.Json;
using System.Text;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Web.Services.ProfesorManagement.Contracts;

namespace SistemaTurnosOnline.Web.Services.ProfesorManagement
{
    public class ProfesorService : IProfesorService
    {
        private readonly ProfesorHttpClient httpClient;
        private readonly ProfesorResponseProcessor responseProcessor;

        public ProfesorService(ProfesorHttpClient httpClient, ProfesorResponseProcessor responseProcessor)
        {
            this.httpClient = httpClient;
            this.responseProcessor = responseProcessor;
        }

        public async Task<Profesor> CreateProfesor(ProfesorForm profesorForm)
        {
            var response = await httpClient.CreateProfesorAsync(profesorForm);

            return await responseProcessor.ProcessProfesorResponseAsync(response);
        }

        public async Task<string> ResetPassword(string id)
        {
            var response = await httpClient.ResetPassword(id);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<Profesor> DeleteProfesor(string id)
        {
            var response = await httpClient.DeleteProfesorAsync(id);

            return await responseProcessor.ProcessProfesorResponseAsync(response);
        }

        public async Task<ProfesorSecure> GetProfesor(string id)
        {
            var response = await httpClient.GetProfesorAsync(id);

            return await responseProcessor.ProcessProfesorSecureResponseAsync(response);
        }

        public async Task<Profesor> GetProfesorByDni(string dni)
        {
            var response = await httpClient.GetProfesorByDniAsync(dni);

            return await responseProcessor.ProcessProfesorResponseAsync(response);
        }

        public async Task<IEnumerable<Profesor>> GetProfesores()
        {
            var response = await httpClient.GetProfesoresAsync();

            return await responseProcessor.ProcessProfesorCollectionResponseAsync(response);
        }

        public async Task<IEnumerable<Profesor>> GetProfesoresInactive()
        {
            var response = await httpClient.GetProfesoresInactiveAsync();

            return await responseProcessor.ProcessProfesorCollectionResponseAsync(response);
        }

        public async Task<Profesor> UpdateProfesor(ProfesorSecure profesorSecure)
        {
            var response = await httpClient.UpdateProfesorAsync(profesorSecure);

            return await responseProcessor.ProcessProfesorResponseAsync(response);
        }

        public async Task<Profesor> UpdateProfesorPassword(ProfileSecurityForm profileSecurityForm)
        {
            var response = await httpClient.UpdateProfesorPasswordAsync(profileSecurityForm);

            return await responseProcessor.ProcessProfesorResponseAsync(response);
        }
    }
}
