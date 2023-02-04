using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Validators.Contracts;

namespace SistemaTurnosOnline.Api.Validator
{
    public class ValidateSignIn : ISignInValidator
    {
        private readonly IProfesorRepository profesorRepository;

        public ValidateSignIn(IProfesorRepository profesorRepository)
        {
            this.profesorRepository = profesorRepository;
        }
        public async Task<bool> AccountIsActive(string formDni)
        {
            var profesor = await profesorRepository.GetProfesorByParam(formDni, p => p.Dni);

            return profesor is { Estado: true };
        }

        public async Task<bool> IsPasswordValid(SignInForm form, string formPassword)
        {
            var profesor = await profesorRepository.GetProfesorByParam(form.Dni, p => p.Dni);

            return profesor != null ? BCrypt.Net.BCrypt.Verify(formPassword, profesor.Password) : false;
        }
    }
}
