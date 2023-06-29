namespace SistemaTurnosOnline.Shared.Validators.Contracts
{
    public interface IValidateProfesor
    {
        Task<bool> EmailIsUnique(string email);
        Task<bool> EmailIsUnique(string email, string id);
        Task<bool> DniIsUnique(string dni);
        Task<bool> DniIsUnique(string dni, string id);
    }
}
