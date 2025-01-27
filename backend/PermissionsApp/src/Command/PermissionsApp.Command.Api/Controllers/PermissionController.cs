using MediatR;
using Microsoft.AspNetCore.Mvc;
using PermissionsApp.Command.Application.Permissions.Commands.CreatePermission;
using PermissionsApp.Command.Application.Permissions.Commands.DeletePermission;
using PermissionsApp.Command.Application.Permissions.Commands.ModifyPermission;
using PermissionsApp.Command.Contracts.Permissions;

namespace PermissionsApp.Command.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly ISender _mediator;

        public PermissionController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermission(CreatePermissionRequest request)
        {
            var command = new CreatePermissionCommand(
                request.EmployerName,
                request.EmployerLastName,
                request.PermissionDate,
                request.PermissionTypeId);
            var createPermissionResult = await _mediator.Send(command);
            return createPermissionResult.MatchFirst(
                permission => Ok(new PermissionResponse(permission.Id)),
                error => Problem());
        }

        [HttpDelete("{permissionId:int}")]
        public async Task<IActionResult> DeletePermission(int permissionId)
        {
            var command = new DeletePermissionCommand(permissionId);
            var deletePermissionResult = await _mediator.Send(command);
            return deletePermissionResult.Match<IActionResult>(
                _ => NoContent(),
                _ => Problem()
                );
        }

        [HttpPut("{permissionId:int}")]
        public async Task<IActionResult> ModifyPermission(int permissionId, ModifyPermissionRequest request)
        {
            var command = new ModifyPermissionCommand(
                permissionId,
                request.EmployerName,
                request.EmployerLastName,
                request.PermissionDate,
                request.PermissionTypeId
            );

            var modifyPermissionResult = await _mediator.Send(command);

            return modifyPermissionResult.MatchFirst(
                permission => Ok(new PermissionResponse(permission.Id)),
                error => Problem());
        }
    }
}
