using FluentValidation;

namespace PermissionsApp.Command.Application.Permissions.Commands.ModifyPermission
{
    public class ModifyPermissionCommandValidator : AbstractValidator<ModifyPermissionCommand>
    {
        public ModifyPermissionCommandValidator()
        {
            RuleFor(x => x.EmployerName)
                .MinimumLength(3)
                .MaximumLength(20);
            RuleFor(x => x.EmployerLastName)
                .MinimumLength(3)
                .MaximumLength(20);
            RuleFor(x => x.PermissionDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("The permission date must be today or in the future.");
        }
    }
}
