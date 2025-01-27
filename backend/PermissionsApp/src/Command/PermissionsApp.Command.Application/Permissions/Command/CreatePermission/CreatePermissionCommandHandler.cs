using ErrorOr;
using MediatR;

namespace PermissionsApp.Command.Application.Permissions.Command.CreatePermission
{
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, ErrorOr<int>>
    {
        public async Task<ErrorOr<int>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            return 123;
        }
    }
}
