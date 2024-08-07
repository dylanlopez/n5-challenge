using MediatR;

namespace N5.Application.Handlers.Queries;

using Domain.Dtos.Queries.Request;
using Domain.Entities;
using Interfaces.Repositories;

public class GetPermisosQueryHandler : IRequestHandler<GetPermisosQueryRequest, List<Permiso>>
{
	private readonly IPermisoRepository _repository;

	public GetPermisosQueryHandler(IPermisoRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<Permiso>> Handle(GetPermisosQueryRequest request, CancellationToken cancellationToken)
	{
		return await _repository.GetPermisos();
	}
}
