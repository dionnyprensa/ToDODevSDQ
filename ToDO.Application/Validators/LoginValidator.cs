using FluentValidation;
using System;
using System.Linq;
using ToDO.Application.Interfaces;
using ToDO.Domain.Entities;

namespace ToDO.Application.Validators
{
    public class LoginValidator : AbstractValidator<User>
    {
        private readonly IUserService _userService;

        public LoginValidator()
        {
        }

        public LoginValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(u => u.UserName)
                .Matches(@"^[a-zA-Z]+\w$").WithMessage("Solo letras.")
                .NotNull().WithMessage("Requerido.")
                .NotEmpty().WithMessage("Requerido.")
                .Length(4, 16).WithMessage("Debe tener entre 4 y 16 carácteres.")
                .Must(UniqueName).WithMessage("Usuario no encontrado");

            RuleFor(u => u.Password)
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,16}$")
                .WithMessage("Debe tener entre 8 y 16 carácteres. Al menos 1 Número.")
                .NotNull().WithMessage("Requerido.")
                .NotEmpty().WithMessage("Requerido.");
        }

        private bool UniqueName(string name)
        {
            var uName =
                _userService.GetAllUsers()
                    .SingleOrDefault(x => string.Equals(x.UserName, name, StringComparison.CurrentCultureIgnoreCase));

            return uName != null;
        }
    }
}