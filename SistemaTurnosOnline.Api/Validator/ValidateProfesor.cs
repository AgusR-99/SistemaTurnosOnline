using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Models.Validators.Contracts;
using System.Linq.Expressions;

namespace SistemaTurnosOnline.Api.Validator
{
    public class ValidateProfesor : IValidateProfesor
    {
        private readonly IProfesorRepository profesorRepository;

        public ValidateProfesor(IProfesorRepository profesorRepository)
        {
            this.profesorRepository = profesorRepository;
        }

        public async Task<bool> ValidateDni(string dni)
        {
            Expression<Func<Profesor, string>> field = p => p.Dni;
            var profesor = await profesorRepository.GetProfesorByParam(dni, field);

            if (profesor == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> ValidateEmail(string email)
        {
            Expression<Func<Profesor, string>> field = p => p.Email;
            var profesor = await profesorRepository.GetProfesorByParam(email, field);

            if (profesor == null)
            {
                return true;
            }

            return false;
        }
    }
}
