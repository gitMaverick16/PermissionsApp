using ErrorOr;
using MediatR;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application.Permissions.Commands.ModifyPermission
{
    public record ModifyPermissionCommand(
        int permissionId, 
        string employerName, 
        string employerLastName, 
        DateTime permissionDate, 
        int permissionTypeId) : IRequest<ErrorOr<Permission>>;
}
