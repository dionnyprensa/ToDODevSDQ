using FluentValidation;
using ToDO.Infraestructure.DataModel;

namespace ToDO.Application.Validators
{
    public class TaskValidator : AbstractValidator<TaskDataModel>
    {
        public TaskValidator()
        {
            RuleFor(t => t.Title)
                .NotNull().WithMessage("Requerido.")
                .NotEmpty().WithMessage("Requerido.")
                .Length(1, 32).WithMessage("Debe tener entre 1 y 32 caracteres.");

            RuleFor(t => t.Description)
                .NotNull().WithMessage("Requerido.")
                .NotEmpty().WithMessage("Requerido.")
                .Length(1, 254).WithMessage("Maximo 254 caracteres.");

            RuleFor(t => t.User)
                .NotNull().WithMessage("Requerido.")
                .NotEmpty().WithMessage("Requerido.");
        }
    }
}