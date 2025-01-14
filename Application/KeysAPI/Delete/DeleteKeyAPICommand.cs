using ErrorOr;
using MediatR;

namespace Application.KeysAPI.Delete;

public record DeleteKeyAPICommand(Guid Id) : IRequest<ErrorOr<Unit>>;