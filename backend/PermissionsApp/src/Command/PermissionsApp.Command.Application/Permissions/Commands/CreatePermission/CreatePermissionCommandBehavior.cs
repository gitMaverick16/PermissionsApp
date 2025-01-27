using ErrorOr;
using MediatR;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application.Permissions.Commands.CreatePermission
{
    public class CreatePermissionCommandBehavior : IPipelineBehavior<CreatePermissionCommand, ErrorOr<Permission>>
    {
        public async Task<ErrorOr<Permission>> Handle(
            CreatePermissionCommand request,
            RequestHandlerDelegate<ErrorOr<Permission>> next,
            CancellationToken cancellationToken)
        {
            var validator = new CreatePermissionCommandValidator();

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
