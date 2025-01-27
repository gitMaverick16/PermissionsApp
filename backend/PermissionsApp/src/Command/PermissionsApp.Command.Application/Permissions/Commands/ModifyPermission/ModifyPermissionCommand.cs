using ErrorOr;
using MediatR;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application.Permissions.Commands.ModifyPermission
{
    public record ModifyPermissionCommand(
        int PermissionId, 
        string EmployeeName, 
        string EmployeeLastName, 
        DateTime PermissionDate, 
        int PermissionTypeId) : IRequest<ErrorOr<Permission>>;
}
