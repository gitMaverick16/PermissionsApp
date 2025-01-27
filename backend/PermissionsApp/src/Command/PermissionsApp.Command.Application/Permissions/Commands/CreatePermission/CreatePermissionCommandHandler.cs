using ErrorOr;
using MediatR;
using PermissionsApp.Command.Application.Common.Events;
using PermissionsApp.Command.Application.Common.Interfaces;
using PermissionsApp.Command.Domain.Permissions;

namespace PermissionsApp.Command.Application.Permissions.Commands.CreatePermission
{
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, ErrorOr<Permission>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionEventProducer _permissionEventProducer;

        private readonly Dictionary<int, string> PermissionTypes; 
        public CreatePermissionCommandHandler(
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
        public async Task<ErrorOr<Permission>> Handle(CreatePermissionCommand command, CancellationToken cancellationToken)
        {
            var permission = new Permission
            {
                EmployerName = command.EmployerName,
                EmployerLastName = command.EmployerLastName,
                PermissionDate = command.PermissionDate,
                PermissionTypeId = command.PermissionTypeId,
            };
            await _permissionRepository.AddPermissionAsync(permission);

            await _unitOfWork.CommitChangesAsync();

            var @event = new PermissionEvent
            {
                Action = ActionType.Add,
                Id = permission.Id,
                EmployerName = permission.EmployerName,
                EmployerLastName = permission.EmployerLastName,
                PermissionDate = permission.PermissionDate,
                PermissionTypeId = permission.PermissionTypeId,
                PermissionTypeDescription = PermissionTypes[permission.PermissionTypeId]
            };

            _permissionEventProducer.Produce("permissions_topic", @event);
            return permission;
        }
    }
}
