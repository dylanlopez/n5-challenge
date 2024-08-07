using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace N5.Api.Controllers;

using Domain.Dtos.Commands.Request;
using Domain.Dtos.Queries.Request;

[Route("api/[controller]")]
[ApiController]
public class PermisoController : ControllerBase
{
	private readonly IMediator _mediator;

	public PermisoController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<IActionResult> RequestPermission(string empleadoNombre,
		string empleadoApellido,
		DateTime fechaPermiso,
		int tipoPermisoId)
	{
		var request = new AddPermisoCommandRequest
		{
			EmpleadoNombre = empleadoNombre,
			EmpleadoApellido = empleadoApellido,
			FechaPermiso = fechaPermiso,
			TipoPermisoId = tipoPermisoId
		};

		await _mediator.Send(request);
		return Ok();
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdatePermission(int id, UpdatePermisoCommandRequest command)
	{
		if (id != command.Id)
		{
			return BadRequest();
		}

		await _mediator.Send(command);
		return NoContent();
	}

	[HttpGet]
	public async Task<IActionResult> GetPermissions()
	{
		var result = await _mediator.Send(new GetPermisosQueryRequest());
		return Ok(result);
	}
}
