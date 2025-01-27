﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using PermissionsApp.Command.Application.Permissions.Commands.CreatePermission;
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
            var createPermissionResult = await _mediator.Send(command);
            return createPermissionResult.MatchFirst(
                permission => Ok(new PermissionResponse(permission.Id)),
                error => Problem());
        }
    }
}
