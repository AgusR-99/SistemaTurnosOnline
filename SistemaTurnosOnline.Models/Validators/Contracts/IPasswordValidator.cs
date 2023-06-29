namespace SistemaTurnosOnline.Shared.Validators.Contracts
{
    public interface IPasswordValidator
    {
        public Task<bool> IsPasswordValid(string userId, string password);
    }
}
