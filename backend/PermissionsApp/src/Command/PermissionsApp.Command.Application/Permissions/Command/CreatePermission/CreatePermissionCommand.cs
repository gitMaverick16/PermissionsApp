using MediatR;

namespace PermissionsApp.Command.Application.Permissions.Command.CreatePermission
{
    public record CreatePermissionCommand(
        string EmployerName,
        string EmployerLastName,
        DateTime PermissionDate,
        int PermissionId) : IRequest<int>;
}
