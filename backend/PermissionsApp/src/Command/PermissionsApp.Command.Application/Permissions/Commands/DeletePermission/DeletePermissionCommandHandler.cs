using ErrorOr;
using MediatR;
using PermissionsApp.Command.Application.Common.Events;
using PermissionsApp.Command.Application.Common.Interfaces;

namespace PermissionsApp.Command.Application.Permissions.Commands.DeletePermission
{
    public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, ErrorOr<Deleted>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionEventProducer _permissionEventProducer;

        public DeletePermissionCommandHandler(
            IPermissionRepository permissionRepository,
            IUnitOfWork unitOfWork,
            IPermissionEventProducer permissionEventProducer)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
            _permissionEventProducer = permissionEventProducer;
        }
        public async Task<ErrorOr<Deleted>> Handle(DeletePermissionCommand command, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByIdAsync(command.Id);

            if (permission is null)
            {
                return Error.NotFound(description: "Permission not found");
            }
            await _permissionRepository.RemovePermissionAsync(permission);
            await _unitOfWork.CommitChangesAsync();


            var @event = new PermissionEvent
            {
                Action = ActionType.Delete,
                Id = permission.Id,
            };

            _permissionEventProducer.Produce("permissions_topic", @event);
            return Result.Deleted;
        }
    }
}
