using ErrorOr;
using MediatR;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application.Permissions.Commands.CreatePermission
{
    public record CreatePermissionCommand(
        string EmployerName,
        string EmployerLastName,
        DateTime PermissionDate,
        int PermissionTypeId) : IRequest<ErrorOr<Permission>>;
}
