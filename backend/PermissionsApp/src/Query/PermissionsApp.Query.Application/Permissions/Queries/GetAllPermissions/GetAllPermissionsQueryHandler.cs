using ErrorOr;
using MediatR;
using PermissionsApp.Query.Application.Common.Interfaces;
using PermissionsApp.Query.Domain.Permissions;

namespace PermissionsApp.Query.Application.Permissions.Queries.GetAllPermissions
{
    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, ErrorOr<List<Permission>>>
    {
        private readonly IPermissionRepository _permissionRepository;
        public GetAllPermissionsQueryHandler(
            IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public async Task<ErrorOr<List<Permission>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetAllAsync();
            return permissions;
        }
    }
}
