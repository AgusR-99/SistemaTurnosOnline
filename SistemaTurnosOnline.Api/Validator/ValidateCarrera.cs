using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Shared.Validators.Contracts;

namespace SistemaTurnosOnline.Api.Validator
{
    public class ValidateCarrera : ICarreraValidator
    {
        private readonly ICarreraRepository carreraRepository;

        public ValidateCarrera(ICarreraRepository carreraRepository)
        {
            this.carreraRepository = carreraRepository;
        }

        public async Task<bool> CodeIsUnique(string code, string id)
        {
            var carrera = await carreraRepository.GetCarreraByCode(code);

            return await Task.FromResult(carrera == null || carrera.Id == id);
        }

        public async Task<bool> NameIsUnique(string name, string id)
        {
            var carrera = await carreraRepository.GetCarreraByName(name);

            return await Task.FromResult(carrera == null || carrera.Id == id);
        }
    }
}
