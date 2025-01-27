using FluentValidation;

namespace PermissionsApp.Command.Application.Permissions.Commands.CreatePermission
{
    public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
    {
        public CreatePermissionCommandValidator()
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
