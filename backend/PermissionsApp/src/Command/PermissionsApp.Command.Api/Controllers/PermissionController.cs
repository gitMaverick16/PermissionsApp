using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
                permission => CreatedAtAction(null, new PermissionResponse(permission.Id)),
                errors => Problem(errors));
        }

        [HttpDelete("{permissionId:int}")]
        public async Task<IActionResult> DeletePermission(int permissionId)
        {
            var command = new DeletePermissionCommand(permissionId);
            var deletePermissionResult = await _mediator.Send(command);
            return deletePermissionResult.Match<IActionResult>(
                _ => NoContent(),
                errors => Problem(errors)
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
                errors => Problem(errors));
        }

        public IActionResult Problem(List<Error> errors)
        {
            if(errors.Count is 0)
            {
                return Problem();
            }

            if(errors.All(error => error.Type == ErrorType.Validation))
            {
                return ValidationProblem(errors);
            }
            return Problem(errors[0]);
        }
        public IActionResult Problem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
            return Problem(statusCode: statusCode, detail: error.Description);
        }

        public IActionResult ValidationProblem(List<Error> errors) {
            var modelStateDictionary = new ModelStateDictionary();
            foreach(var error in errors)
            {
                modelStateDictionary.AddModelError(
                    error.Code,
                    error.Description);
            }
            return ValidationProblem(modelStateDictionary);
        }
    }
}
