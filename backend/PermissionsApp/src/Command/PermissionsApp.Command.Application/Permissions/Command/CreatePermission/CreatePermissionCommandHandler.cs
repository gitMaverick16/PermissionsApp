using MediatR;

namespace PermissionsApp.Command.Application.Permissions.Command.CreatePermission
{
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, int>
    {
        public Task<int> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
