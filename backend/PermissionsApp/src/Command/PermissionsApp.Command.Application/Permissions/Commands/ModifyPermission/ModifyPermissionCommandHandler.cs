using ErrorOr;
using MediatR;
using PermissionsApp.Command.Application.Common.Events;
using PermissionsApp.Command.Application.Common.Interfaces;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application.Permissions.Commands.ModifyPermission
{
    public class ModifyPermissionCommandHandler : IRequestHandler<ModifyPermissionCommand, ErrorOr<Permission>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionEventProducer _permissionEventProducer;

        private readonly Dictionary<int, string> PermissionTypes;
        public ModifyPermissionCommandHandler(
            IPermissionRepository permissionRepository,
            IUnitOfWork unitOfWork,
            IPermissionEventProducer permissionEventProducer)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;

            _permissionEventProducer = permissionEventProducer;

            PermissionTypes = new()
            {
                { 1, "Sick Leave"},
                { 2, "Vacation"},
                { 3, "Maternity Leave"},
                { 4, "Paternity Leave"},
                { 5, "Personal Leave"},
            };
        }
        public async Task<ErrorOr<Permission>> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByIdAsync(request.PermissionId);

            if (permission == null)
            {
                return Error.NotFound(description: "Permission not found");
            }

            permission.EmployeeName = request.EmployeeName;
            permission.EmployeeLastName = request.EmployeeLastName;
            permission.PermissionDate = request.PermissionDate;
            permission.PermissionTypeId = request.PermissionTypeId;

            await _permissionRepository.UpdateAsync(permission);

            await _unitOfWork.CommitChangesAsync();

            var @event = new PermissionEvent
            {
                Action = ActionType.Modify,
                Id = permission.Id,
                EmployeeName = permission.EmployeeName,
                EmployeeLastName = permission.EmployeeLastName,
                PermissionDate = permission.PermissionDate,
                PermissionTypeId = permission.PermissionTypeId,
                PermissionTypeDescription = PermissionTypes[permission.PermissionTypeId]
            };

            _permissionEventProducer.Produce("permissions_topic", @event);
            return permission;
        }
    }
}
