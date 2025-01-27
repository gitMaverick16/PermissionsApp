using MediatR;
using Microsoft.AspNetCore.Mvc;
using PermissionsApp.Command.Application.Permissions.Command.CreatePermission;
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
                request.PermissionId);
            var permissionId = await _mediator.Send(command);
            var response = new PermissionResponse(permissionId);
            return Ok(response);
        }
    }
}
