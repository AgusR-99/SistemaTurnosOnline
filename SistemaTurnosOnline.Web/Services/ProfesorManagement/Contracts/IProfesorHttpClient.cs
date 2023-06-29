using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Web.Services.ProfesorManagement.Contracts
{
    public interface IProfesorHttpClient
    {
        public Task<HttpResponseMessage> CreateProfesorAsync(ProfesorForm profesorForm);

        public Task<HttpResponseMessage> DeleteProfesorAsync(string id);

        public Task<HttpResponseMessage> UpdateProfesorAsync(ProfesorSecure profesorSecure);

        public Task<HttpResponseMessage> UpdateProfesorPasswordAsync(ProfileSecurityForm profileSecurityForm);

        public Task<HttpResponseMessage> GetProfesoresAsync();

        public Task<HttpResponseMessage> GetProfesoresInactiveAsync();

        public Task<HttpResponseMessage> GetProfesorAsync(string id);

        public Task<HttpResponseMessage> GetProfesorByDniAsync(string dni);

        public Task<HttpResponseMessage> ResetPassword(string id);
    }
}
