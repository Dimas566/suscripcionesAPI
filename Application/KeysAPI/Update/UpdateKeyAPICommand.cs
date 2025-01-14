using Domain.Keys;
using ErrorOr;
using MediatR;

namespace Application.KeysAPI.Update;


public record UpdateKeyAPICommand(
    Guid Id,
    string Key,
    KeyType KeyType,
    bool Active,
    string UserId
) : IRequest<ErrorOr<Guid>>;