namespace SistemaTurnosOnline.Shared.Validators.Contracts
{
    public interface ISignInValidator
    {
        Task<bool> AccountIsActive(string formDni);
        Task<bool> IsPasswordValid(SignInForm form, string formPassword);
    }
}
