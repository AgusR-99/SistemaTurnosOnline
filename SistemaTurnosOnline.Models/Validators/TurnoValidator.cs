using FluentValidation;
using SistemaTurnosOnline.Shared.Turnos;
using SistemaTurnosOnline.Shared.Validators.Contracts;

namespace SistemaTurnosOnline.Shared.Validators
{
    public class TurnoValidator : AbstractValidator<TurnoListado>
    {
        private readonly ITurnoValidator turnoValidator;

        public TurnoValidator(ITurnoValidator turnoValidator)
        {
            this.turnoValidator = turnoValidator;

            RuleFor(t => t.Orden)
                .NotEmpty();

            When(t => !string.IsNullOrWhiteSpace(t.Orden), () =>
            {
                RuleFor(t => t.Orden).MustAsync(async (TurnoListado, orden, CancellationToken) =>
                    {
                        return await this.turnoValidator.BeNotOutOfBounds(TurnoListado, orden);
                    })
                    .WithMessage("Orden fuera de rango")
                    .Must(orden =>
                    {
                        return BeDigit(orden);
                    })
                    .WithMessage("Orden debe ser un numero");
            });
        }

        private bool BeDigit(string valueToValidate)
        {
            return valueToValidate.All(char.IsDigit);
        }
    }
}
