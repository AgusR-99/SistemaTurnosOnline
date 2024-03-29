﻿using FluentValidation;
using SistemaTurnosOnline.Shared.Validators.Contracts;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace SistemaTurnosOnline.Shared.Validators
{
    public class ProfesorValidator : AbstractValidator<ProfesorForm>
    {
        private readonly IValidateProfesor validation;

        public ProfesorValidator(IValidateProfesor validation)
        {
            this.validation = validation;

            // Reglas para DNI
            Expression<Func<ProfesorForm, string>> DniExpression = p => p.Dni;

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
            Expression<Func<ProfesorForm, string>> NombreExpression = p => p.Nombre;

            RulesForParameter(
                ProfesorParam.Field.Nombre,
                NombreExpression,
                p => !string.IsNullOrWhiteSpace(p.Nombre),
                false,
                false,
                true
                );

            // Reglas para Email
            Expression<Func<ProfesorForm, string>> EmailExpression = p => p.Email;

            RulesForParameter(
                ProfesorParam.Field.Email,
                EmailExpression,
                p => !string.IsNullOrWhiteSpace(p.Email),
                true
                );

            // Reglas para Password
            Expression<Func<ProfesorForm, string>> PasswordExpression = p => p.Password;

            RulesForParameter(
                ProfesorParam.Field.Password,
                PasswordExpression,
                p => !string.IsNullOrWhiteSpace(p.Password),
                false,
                false,
                false,
                4
                );

            // Reglas para la confirmacion de password
            Expression<Func<ProfesorForm, string>> PasswordConfirmExpression = p => p.PasswordConfirm;

            RulesForParameter(
                ProfesorParam.Field.PasswordConfirm,
                PasswordConfirmExpression,
                p => !string.IsNullOrWhiteSpace(p.PasswordConfirm),
                false,
                false,
                false,
                0
                );
        }

        private void RulesForParameter(
            ProfesorParam.Field field,
            Expression<Func<ProfesorForm, string>> expression,
            Func<ProfesorForm, bool> condition,
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
                // Si es confirmacion de password, evaluar si este miembro es igual al miembro 'Password'
                if (field == ProfesorParam.Field.PasswordConfirm)
                {
                    RuleFor(expression)
                    .Equal(p => p.Password)
                    .WithMessage($"'{GetMemberName(expression)}' no coincide con la contraseña");
                }

                // Si es email, evaluar si tiene forma de email y si es unico
                else if (field == ProfesorParam.Field.Email)
                {
                    RuleFor(expression)
                        .EmailAddress()
                        .MustAsync(async (ProfesorForm, email, CancellationToken) =>
                        {
                            return await BeUniqueEmail(ProfesorForm, email);
                        })
                        .WithMessage($"'{GetMemberName(expression)}' no puede repetirse");
                }

                // Si es email, evaluar si tiene forma de dni y si es unico
                else if (field == ProfesorParam.Field.Dni)
                {
                    RuleFor(expression)
                        .MustAsync(async (ProfesorForm, dni, CancellationToken) =>
                        {
                            return await BeUniqueDni(ProfesorForm, dni);
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

        // Modificado para que devuelva nombre del miembro de una expresion
        // https://stackoverflow.com/questions/273941/get-property-name-and-type-using-lambda-expression
        private string GetMemberName<T>(Expression<Func<ProfesorForm, T>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member != null)
                return member.Member.Name;

            throw new ArgumentException("Expression is not a member access", "expression");
        }

        private async Task<bool> BeUniqueEmail(ProfesorForm profesor, string email)
        {
            if (profesor.Id == null)
            {
                return await validation.EmailIsUnique(email);
            }

            return await validation.EmailIsUnique(email, profesor.Id);
        }

        private async Task<bool> BeUniqueDni(ProfesorForm profesor, string dni)
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
    }
}
