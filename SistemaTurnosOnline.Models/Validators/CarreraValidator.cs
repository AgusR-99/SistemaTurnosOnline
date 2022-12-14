using FluentValidation;
using SistemaTurnosOnline.Models;
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
                .NotEmpty()
                .MinimumLength(4)
                .MustAsync(async (name, CancellationToken) => 
                    await this.validator.NameIsUnique(name)).WithMessage("La carrera ya existe");
        }
    }
}
