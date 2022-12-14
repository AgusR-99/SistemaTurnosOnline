using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Web.Services.Contracts;

namespace SistemaTurnosOnline.Web.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly HttpClient httpClient;

        public ProfesorService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<Profesor> CreateProfesor(Profesor profesor)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProfesor(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Profesor> GetProfesor(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Profesor>> GetProfesores()
        {
            throw new NotImplementedException();
        }

        public Task<Profesor> UpdateProfesor(Profesor profesor)
        {
            throw new NotImplementedException();
        }
    }
}
