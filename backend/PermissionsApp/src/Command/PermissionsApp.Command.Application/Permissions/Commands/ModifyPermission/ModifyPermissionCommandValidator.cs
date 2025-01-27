using FluentValidation;

namespace PermissionsApp.Command.Application.Permissions.Commands.ModifyPermission
{
    public class ModifyPermissionCommandValidator : AbstractValidator<ModifyPermissionCommand>
    {
        public ModifyPermissionCommandValidator()
        {
            RuleFor(x => x.EmployeeName)
                .MinimumLength(3)
                .MaximumLength(20);
            RuleFor(x => x.EmployeeLastName)
                .MinimumLength(3)
                .MaximumLength(20);
            RuleFor(x => x.PermissionDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("The permission date must be today or in the future.");
        }
    }
}
