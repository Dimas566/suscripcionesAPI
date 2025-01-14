using ErrorOr;
using KeysAPI.Common;
using MediatR;

namespace Application.KeysAPI.GetById;

public record GetKeyAPIByIdQuery(Guid Id) : IRequest<ErrorOr<KeyAPIResponse>>;