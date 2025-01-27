using ErrorOr;
using MediatR;
using PermissionsApp.Command.Application.Common.Interfaces;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application.Permissions.Commands.ModifyPermission
{
    public class ModifyPermissionCommandHandler : IRequestHandler<ModifyPermissionCommand, ErrorOr<Permission>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ModifyPermissionCommandHandler(
            IPermissionRepository permissionRepository,
            IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Permission>> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByIdAsync(request.permissionId);

            if (permission == null)
            {
                return Error.NotFound(description: "Permission not found");
            }

            permission.EmployerName = request.employerName;
            permission.EmployerLastName = request.employerLastName;
            permission.PermissionDate = request.permissionDate;
            permission.PermissionTypeId = request.permissionTypeId;

            await _permissionRepository.UpdateAsync(permission);

            await _unitOfWork.CommitChangesAsync();
            return permission;
        }
    }
}
