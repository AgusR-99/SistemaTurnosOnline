using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Turnos;

namespace SistemaTurnosOnline.Web.Services.ProfesorManagement.Contracts
{
    public interface IProfesorResponseProcessor
    {
        public Task<Profesor> ProcessProfesorResponseAsync(HttpResponseMessage response);

        public Task<ProfesorSecure> ProcessProfesorSecureResponseAsync(HttpResponseMessage response);

        public Task<IEnumerable<Profesor>> ProcessProfesorCollectionResponseAsync(HttpResponseMessage response);
    }
}
