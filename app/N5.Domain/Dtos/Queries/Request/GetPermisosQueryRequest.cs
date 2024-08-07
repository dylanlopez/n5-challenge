using MediatR;

namespace N5.Domain.Dtos.Queries.Request;

using Entities;

public class GetPermisosQueryRequest : IRequest<List<Permiso>> { }
