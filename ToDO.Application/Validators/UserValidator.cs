using FluentValidation;
using System;
using System.Linq;
using ToDO.Application.Interfaces;
using ToDO.Infraestructure.DataModel;

namespace ToDO.Application.Validators
{
    public class UserValidator : AbstractValidator<UserDataModel>
    {
        private readonly IUserService _userService;

        public UserValidator()
        {
        }

        public UserValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(u => u.UserName)
                .Matches(@"^[a-zA-Z]+\w$").WithMessage("Solo letras.")
                .NotNull().WithMessage("Requerido.")
                .NotEmpty().WithMessage("Requerido.")
                .Length(4, 16).WithMessage("Debe tener entre 4 y 16 letras.")
                .Must(UniqueName).WithMessage("Usuario no disponible");

            RuleFor(u => u.Password)
                .Matches(@"^^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,16}$")
                .WithMessage("Debe tener entre 8 y 16 letras. Al menos 1 Mayúscula y 1 Número.")
                .NotNull().WithMessage("Requerido.")
                .NotEmpty().WithMessage("Requerido.");

            RuleFor(u => u.FirstName)
                .Matches(@"^[a-zA-Z ]+\w$").WithMessage("Solo letras.")
                .NotNull().WithMessage("Requerido.")
                .NotEmpty().WithMessage("Requerido.")
                .Length(4, 32).WithMessage("Debe tener entre 4 y 32 letras.");

            RuleFor(u => u.LastName)
                .Matches(@"^[a-zA-Z ]+\w$").WithMessage("Solo letras.")
                .NotNull().WithMessage("Requerido.")
                .NotEmpty().WithMessage("Requerido.")
                .Length(4, 64).WithMessage("Debe tener entre 4 y 64 letras.");
        }

        private bool UniqueName(string name)
        {
            var uName =
                _userService.GetAllUsers()
                    .SingleOrDefault(x => string.Equals(x.UserName, name, StringComparison.CurrentCultureIgnoreCase));

            return uName == null;
        }
    }
}