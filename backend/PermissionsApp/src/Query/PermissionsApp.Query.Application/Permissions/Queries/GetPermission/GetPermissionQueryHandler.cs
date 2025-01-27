using ErrorOr;
using MediatR;
using PermissionsApp.Query.Application.Common.Interfaces;
using PermissionsApp.Query.Domain.Permissions;

namespace PermissionsApp.Query.Application.Permissions.Queries.GetPermission
{
    public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, ErrorOr<Permission>>
    {
        private readonly IPermissionRepository _permissionRepository;
        public GetPermissionQueryHandler(
            IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository; 
        }
        public async Task<ErrorOr<Permission>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByIdAsync(request.Id);
            return permission is null
                ? Error.NotFound(description: "Permission not found")
                : permission;
        }
    }
}
