using ErrorOr;
using MediatR;
using PermissionsApp.Query.Domain.Permissions;

namespace PermissionsApp.Query.Application.Permissions.Queries.GetAllPermissions
{
    public record GetAllPermissionsQuery : IRequest<ErrorOr<List<Permission>>>;
}
