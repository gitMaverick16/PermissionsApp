using ErrorOr;
using MediatR;

namespace PermissionsApp.Command.Application.Permissions.Commands.DeletePermission
{
    public record DeletePermissionCommand(int Id) : IRequest<ErrorOr<Deleted>>;
}
