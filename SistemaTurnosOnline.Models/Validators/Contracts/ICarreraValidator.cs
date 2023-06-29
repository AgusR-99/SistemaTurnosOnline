namespace SistemaTurnosOnline.Shared.Validators.Contracts
{
    public interface ICarreraValidator
    {
        Task<bool> NameIsUnique(string name, string id);
        Task<bool> CodeIsUnique(string code, string id);
    }
}
