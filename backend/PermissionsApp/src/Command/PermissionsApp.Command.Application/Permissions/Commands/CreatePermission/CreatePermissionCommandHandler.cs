using ErrorOr;
using MediatR;
using PermissionsApp.Command.Application.Common.Interfaces;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application.Permissions.Commands.CreatePermission
{
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, ErrorOr<Permission>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePermissionCommandHandler(
            IPermissionRepository permissionRepository,
            IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Permission>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = new Permission
            {
                EmployerName = request.EmployerName,
                EmployerLastName = request.EmployerLastName,
                PermissionDate = request.PermissionDate,
                PermissionTypeId = request.PermissionTypeId
            };
            await _permissionRepository.AddPermissionAsync(permission);

            await _unitOfWork.CommitChangesAsync();
            return permission;
        }
    }
}
