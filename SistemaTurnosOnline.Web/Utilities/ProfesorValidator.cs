using FluentValidation;
using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Web.Services;
using SistemaTurnosOnline.Web.Services.Contracts;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SistemaTurnosOnline.Web.Utilities
{
    public class ProfesorValidator : AbstractValidator<ProfesorForm>
    {
        private readonly IProfesorService profesorService;

        public ProfesorValidator(IProfesorService profesorService)
        {
            this.profesorService = profesorService;

            Expression<Func<ProfesorForm, string>> DniExpression = p => p.Dni;

            RulesForParameter(
                AttributeCheck.Attribute.Dni,
                DniExpression,
                p => !string.IsNullOrWhiteSpace(p.Dni),
                true,
                true,
                false,
                6
                );


            Expression<Func<ProfesorForm, string>> NombreExpression = p => p.Nombre;

            RulesForParameter(
                AttributeCheck.Attribute.Nombre,
                NombreExpression,
                p => !string.IsNullOrWhiteSpace(p.Nombre),
                false,
                false,
                true
                );


            Expression<Func<ProfesorForm, string>> EmailExpression = p => p.Email;

            RulesForParameter(
                AttributeCheck.Attribute.Email,
                EmailExpression,
                p => !string.IsNullOrWhiteSpace(p.Email),
                true
                );


            Expression<Func<ProfesorForm, string>> PasswordExpression = p => p.Password;

            RulesForParameter(
                AttributeCheck.Attribute.Password,
                PasswordExpression,
                p => !string.IsNullOrWhiteSpace(p.Password)
                );


            Expression<Func<ProfesorForm, string>> PasswordConfirmExpression = p => p.PasswordConfirm;

            RulesForParameter(
                AttributeCheck.Attribute.PasswordConfirm,
                PasswordConfirmExpression,
                p => !string.IsNullOrWhiteSpace(p.PasswordConfirm),
                false,
                false,
                false,
                0
                );
        }

        private void RulesForParameter(
            AttributeCheck.Attribute attribute,
            Expression<Func<ProfesorForm, string>> expression,
            Func<ProfesorForm, bool> condition,
            bool isUnique = false,
            bool isNumber = false,
            bool isLetter = false,
            int minLength = 2
            )
        {
            if (isNumber && isLetter)
            {
                throw new ArgumentException($"Parametros '{nameof(isLetter)}' y '{nameof(isNumber)}' no pueden estar activos al mismo tiempo");
            }
            RuleFor(expression)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(minLength);

            When(condition, () =>
            {
                // Si es confirmacion de password
                if (attribute == AttributeCheck.Attribute.PasswordConfirm)
                {
                    RuleFor(expression)
                    .Equal(p => p.Password)
                    .WithMessage($"'{GetMemberName(expression)}' no coincide con la contraseña");
                }
                // Si es email
                else if (attribute == AttributeCheck.Attribute.Email)
                {
                    RuleFor(expression)
                        .EmailAddress();
                }

                // Si es numero
                if (isNumber)
                {
                    RuleFor(expression)
                    .Must(
                        (valueToValidate) =>
                        {
                            return BeDigit(valueToValidate);
                        }).WithMessage($"'{GetMemberName(expression)}' solo puede contener numeros");
                }
                // Si es letra
                else if (isLetter)
                {
                    RuleFor(expression)
                    .Must(
                        (valueToValidate) =>
                        {
                            return BeLetter(valueToValidate);
                        }).WithMessage($"'{GetMemberName(expression)}' solo puede contener letras");
                }

                // Si es unico
                if (isUnique)
                {
                    RuleFor(expression)
                    .MustAsync(
                        async (valueToValidate, cancellation) =>
                        {
                            return await BeUnique(valueToValidate, attribute, cancellation);
                        }).WithMessage($"'{GetMemberName(expression)}' ya se encuentra en uso");
                }
            });
        }

        // Modificado para que devuelva nombre del miembro de una expresion
        // https://stackoverflow.com/questions/273941/get-property-name-and-type-using-lambda-expression
        private string GetMemberName<T>(Expression<Func<ProfesorForm, T>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member != null)
                return member.Member.Name;

            throw new ArgumentException("Expression is not a member access", "expression");
        }

        //TODO: crear clase statica con estos metodos de aca abajo
        //TODO: cambiar parametro enum a uno generico

        private async Task<bool> BeUnique(string value, AttributeCheck.Attribute Check, CancellationToken cancellation)
        {
            try
            {
                if (AttributeCheck.Attribute.IsDefined(typeof(AttributeCheck.Attribute), Check))
                {
                    var isDuplicated = await profesorService.IsDuplicated(value, Check);

                    if (!isDuplicated)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool BeDigit(string valueToValidate)
        {
            return valueToValidate.All(char.IsDigit);
        }

        private bool BeLetter(string valueToValidate)
        {
            return Regex.IsMatch(valueToValidate, @"^[a-zA-Z,\s]+$");
        }
    }
}
