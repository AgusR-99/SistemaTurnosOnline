using FluentValidation;
using SistemaTurnosOnline.Shared.Validators.Contracts;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace SistemaTurnosOnline.Shared.Validators
{
    public class ProfesorSecureValidator : AbstractValidator<ProfesorSecure>
    {
        private readonly IValidateProfesor validation;

        // Modificado para que devuelva nombre del miembro de una expresion
        // https://stackoverflow.com/questions/273941/get-property-name-and-type-using-lambda-expression
        private string GetMemberName<T>(Expression<Func<ProfesorSecure, T>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member != null)
                return member.Member.Name;

            throw new ArgumentException("Expression is not a member access", "expression");
        }

        private async Task<bool> BeUniqueEmail(ProfesorSecure profesor, string email)
        {
            if (profesor.Id == null)
            {
                return await validation.EmailIsUnique(email);
            }

            return await validation.EmailIsUnique(email, profesor.Id);
        }
        private async Task<bool> BeUniqueDni(ProfesorSecure profesor, string dni)
        {
            if (profesor.Id == null)
            {
                return await validation.DniIsUnique(dni);
            }

            return await validation.DniIsUnique(dni, profesor.Id);
        }

        private bool BeDigit(string valueToValidate)
        {
            return valueToValidate.All(char.IsDigit);
        }

        private bool BeLetter(string valueToValidate)
        {
            return Regex.IsMatch(valueToValidate, @"^[a-zA-Z,\s]+$");
        }

        public ProfesorSecureValidator(IValidateProfesor validation)
        {
            this.validation = validation;

            // Reglas para DNI
            Expression<Func<ProfesorSecure, string>> DniExpression = p => p.Dni;

            RulesForParameter(
                ProfesorParam.Field.Dni,
                DniExpression,
                p => !string.IsNullOrWhiteSpace(p.Dni),
                true,
                true,
                false,
                6
                );

            // Reglas para nombre
            Expression<Func<ProfesorSecure, string>> NombreExpression = p => p.Nombre;

            RulesForParameter(
                ProfesorParam.Field.Nombre,
                NombreExpression,
                p => !string.IsNullOrWhiteSpace(p.Nombre),
                false,
                false,
                true
                );

            // Reglas para Email
            Expression<Func<ProfesorSecure, string>> EmailExpression = p => p.Email;

            RulesForParameter(
                ProfesorParam.Field.Email,
                EmailExpression,
                p => !string.IsNullOrWhiteSpace(p.Email),
                true
                );
        }

        private void RulesForParameter(
            ProfesorParam.Field field,
            Expression<Func<ProfesorSecure, string>> expression,
            Func<ProfesorSecure, bool> condition,
            bool isUnique = false,
            bool isNumber = false,
            bool isLetter = false,
            int minLength = 2
            )
        {
            // Si es solo numero y solo letra, tirar una excepcion. NO PUEDE SER LAS DOS COSAS AL MISMO TIEMPO!
            if (isNumber && isLetter)
            {
                throw new ArgumentException($"Parametros '{nameof(isLetter)}' y '{nameof(isNumber)}' no pueden estar activos al mismo tiempo");
            }

            // El campo no puede estar vacio y debe cumplir con la longitud de caracteres minima
            RuleFor(expression)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(minLength);

            // Mientras se cumpla una condicion establecida, ejecutar todo esto de abajo
            When(condition, () =>
            {
                if (field == ProfesorParam.Field.Email)
                {
                    RuleFor(expression)
                        .EmailAddress()
                        .MustAsync(async (ProfesorSecure, email, CancellationToken) =>
                        {
                            return await BeUniqueEmail(ProfesorSecure, email);
                        })
                        .WithMessage($"'{GetMemberName(expression)}' no puede repetirse");
                }

                // Si es email, evaluar si tiene forma de dni y si es unico
                else if (field == ProfesorParam.Field.Dni)
                {
                    RuleFor(expression)
                        .MustAsync(async (ProfesorSecure, dni, CancellationToken) =>
                        {
                            return await BeUniqueDni(ProfesorSecure, dni);
                        })
                        .WithMessage($"'{GetMemberName(expression)}' no puede repetirse");
                }

                // Si es numero, evaluar si solo tiene numeros
                if (isNumber)
                {
                    RuleFor(expression)
                    .Must(
                        (valueToValidate) =>
                        {
                            return BeDigit(valueToValidate);
                        }).WithMessage($"'{GetMemberName(expression)}' solo puede contener numeros");
                }

                // Si es letra, evaluar si solo es letra
                else if (isLetter)
                {
                    RuleFor(expression)
                    .Must(
                        (valueToValidate) =>
                        {
                            return BeLetter(valueToValidate);
                        }).WithMessage($"'{GetMemberName(expression)}' solo puede contener letras");
                }
            });
        }
    }
}
