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

        public async Task<bool> DniIsUnique(string dni, string id)
        {
            Expression<Func<Profesor, string>> field = p => p.Dni;
            var profesor = await profesorRepository.GetProfesorByParam(dni, field);

            if (profesor == null || profesor.Id == id)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DniIsUnique(string dni)
        {
            Expression<Func<Profesor, string>> field = p => p.Dni;
            var profesor = await profesorRepository.GetProfesorByParam(dni, field);

            if (profesor == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> EmailIsUnique(string email, string id)
        {
            Expression<Func<Profesor, string>> field = p => p.Email;
            var profesor = await profesorRepository.GetProfesorByParam(email, field);

            if (profesor == null || profesor.Id == id)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> EmailIsUnique(string email)
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
