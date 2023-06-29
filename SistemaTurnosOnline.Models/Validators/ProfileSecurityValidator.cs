using FluentValidation;
using SistemaTurnosOnline.Shared.Validators.Contracts;

namespace SistemaTurnosOnline.Shared.Validators
{
    public class ProfileSecurityValidator : AbstractValidator<ProfileSecurityForm>
    {
        private readonly IPasswordValidator passwordValidator;

        public ProfileSecurityValidator(IPasswordValidator passwordValidator)
        {
            this.passwordValidator = passwordValidator;

            RuleFor(form => form.Password)
                .NotEmpty();

            When(form => !string.IsNullOrWhiteSpace(form.Password), () =>
            {
                RuleFor(form => form.Password)
                    .MustAsync(async (user, password, CancellationToken) =>
                    {
                        return await this.passwordValidator.IsPasswordValid(user.Id, password);
                    })
                    .WithMessage("Contraseña es invalida");
            });

            RuleFor(form => form.NewPassword)
                .NotEmpty()
                .MinimumLength(4);

            RuleFor(form => form.NewPasswordConfirm)
                .Equal(form => form.NewPassword)
                .WithMessage("Las contraseñas no coinciden");
        }
    }
}
