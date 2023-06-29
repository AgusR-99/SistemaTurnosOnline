using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Web.Services.CarreraManagement.Contracts
{
    public interface ICarreraResponseProcessor
    {
        public Task<Carrera> ProcessCarreraResponseAsync(HttpResponseMessage response);

        public Task<List<Carrera>> ProcessCarrerasResponseAsync(HttpResponseMessage response);
    }
}
