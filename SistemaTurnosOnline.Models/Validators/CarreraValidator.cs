using FluentValidation;
using SistemaTurnosOnline.Shared.Validators.Contracts;

namespace SistemaTurnosOnline.Shared.Validators
{
    public class CarreraValidator : AbstractValidator<Carrera>
    {
        private readonly ICarreraValidator validator;

        public CarreraValidator(ICarreraValidator validator)
        {
            this.validator = validator;

            RuleFor(c => c.Nombre)
                .NotEmpty();

            When(c => !string.IsNullOrWhiteSpace(c.Nombre), () =>
            {
                RuleFor(c => c.Nombre)
                .MinimumLength(4)
                .MustAsync(async (carrera, name, CancellationToken) =>
                    await this.validator.NameIsUnique(name, carrera.Id)).WithMessage("La carrera ya existe");
            });

            RuleFor(c => c.Codigo)
                .NotEmpty();

            When(c => !string.IsNullOrWhiteSpace(c.Codigo), () =>
            {
                RuleFor(c => c.Codigo)
                .MustAsync(async (carrera, codigo, CancellationToken) =>
                    await this.validator.CodeIsUnique(codigo, carrera.Id)).WithMessage("El codigo ya existe");
            });
        }
    }
}
