using FluentValidation;
using SistemaTurnosOnline.Shared.Validators.Contracts;

namespace SistemaTurnosOnline.Shared.Validators
{
    public class SignInValidator : AbstractValidator<SignInForm>
    {
        private readonly ISignInValidator validator;

        public SignInValidator(ISignInValidator validator)
        {
            this.validator = validator;

            RuleFor(s => s.Dni)
                .NotEmpty();

            When(s => !string.IsNullOrWhiteSpace(s.Dni), () =>
            {
                RuleFor(s => s.Dni)
                    .MustAsync(async (dni, CancellationToken) =>
                    {
                        return !await this.validator.AccountIsActive(dni);
                    })
                    .WithMessage("Cuenta no esta activada o no existe");
            });

            RuleFor(s => s.Password)
                .NotEmpty();

            When(s => !string.IsNullOrWhiteSpace(s.Password), () =>
            {
                RuleFor(s => s.Password)
                    .MustAsync(async (form, password, CancellationToken) =>
                    {
                        return !await this.validator.IsPasswordValid(form, password);
                    })
                    .WithMessage("Contraseña es invalida");
            });
        }
    }
}
