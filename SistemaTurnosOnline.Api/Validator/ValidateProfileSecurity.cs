using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Shared.Validators.Contracts;

namespace SistemaTurnosOnline.Api.Validator
{
    public class ValidateProfileSecurity : IPasswordValidator
    {
        private readonly IProfesorRepository profesorRepository;

        public ValidateProfileSecurity(IProfesorRepository profesorRepository)
        {
            this.profesorRepository = profesorRepository;
        }

        public async Task<bool> IsPasswordValid(string userId, string password)
        {
            var profesor = await profesorRepository.GetProfesor(userId);

            return profesor != null ? BCrypt.Net.BCrypt.Verify(password, profesor.Password) : false;
        }
    }
}
