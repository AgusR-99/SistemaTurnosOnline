using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Web.Services.CarreraManagement.Contracts
{
    public interface ICarreraHttpClient
    {
        Task<HttpResponseMessage> CreateCarreraAsync(Carrera carrera);
        Task<HttpResponseMessage> DeleteCarreraAsync(string id);
        Task<HttpResponseMessage> GetCarreraAsync(string id);
        Task<HttpResponseMessage> GetCarrerasAsync();
        Task<HttpResponseMessage> GetCarrerasByUserIdAsync(string userId);
        Task<HttpResponseMessage> UpdateCarreraAsync(Carrera carrera);
    }
}
