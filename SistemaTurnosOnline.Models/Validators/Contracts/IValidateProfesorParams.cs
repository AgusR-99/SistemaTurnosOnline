using System.Linq.Expressions;

namespace SistemaTurnosOnline.Models.Validators.Contracts
{
    public interface IValidateProfesor
    {
        Task<bool> ValidateEmail(string email);
        Task<bool> ValidateDni(string dni);
    }
}
