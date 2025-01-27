using ErrorOr;
using MediatR;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application.Permissions.Commands.ModifyPermission
{
    public class ModifyPermissionCommandBehavior : IPipelineBehavior<ModifyPermissionCommand, ErrorOr<Permission>>
    {
        public async Task<ErrorOr<Permission>> Handle(
            ModifyPermissionCommand request,
            RequestHandlerDelegate<ErrorOr<Permission>> next,
            CancellationToken cancellationToken)
        {
            var validator = new ModifyPermissionCommandValidator();

            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }
            return await next();
        }
    }
}
