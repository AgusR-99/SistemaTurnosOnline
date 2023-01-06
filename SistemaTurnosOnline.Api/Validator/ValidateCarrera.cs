using SistemaTurnosOnline.Api.Repositories;
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

        public async Task<bool> NameIsUnique(string name)
        {
            var carrera = await carreraRepository.GetCarreraByName(name);

            if (carrera == null) return true;

            return false;
        }
    }
}
