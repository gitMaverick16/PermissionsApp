using ErrorOr;
using MediatR;
using PermissionsApp.Query.Domain.Permissions;

namespace PermissionsApp.Query.Application.Permissions.Queries.GetPermission
{
    public record GetPermissionQuery
        (int Id): IRequest<ErrorOr<Permission>>;
}
