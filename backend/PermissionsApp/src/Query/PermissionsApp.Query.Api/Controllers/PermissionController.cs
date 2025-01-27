using MediatR;
using Microsoft.AspNetCore.Mvc;
using PermissionsApp.Query.Application.Permissions.Queries.GetAllPermissions;
using PermissionsApp.Query.Application.Permissions.Queries.GetPermission;
using PermissionsApp.Query.Contracts.Permissions;

namespace PermissionsApp.Query.Api.Controllers
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPermission(int id)
        {
            var query = new GetPermissionQuery(id);

            var getPermissionResult = await _mediator.Send(query);

            return getPermissionResult.MatchFirst(
                permission => Ok(new PermissionResponse(
                    getPermissionResult.Value.Id,
                    getPermissionResult.Value.EmployerName,
                    getPermissionResult.Value.EmployerLastName,
                    getPermissionResult.Value.PermissionDate,
                    getPermissionResult.Value.PermissionType.Id,
                    getPermissionResult.Value.PermissionType.Description)),
                error => Problem());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            var query = new GetAllPermissionsQuery();

            var getPermissionResult = await _mediator.Send(query);

            return getPermissionResult.Match(
                permissions => Ok(
                    permissions.ConvertAll(
                        permission => new PermissionResponse(
                            permission.Id, 
                            permission.EmployerName, 
                            permission.EmployerLastName,
                            permission.PermissionDate,
                            permission.PermissionType.Id,
                            permission.PermissionType.Description))),
                _ => Problem());
        }
    }
}
