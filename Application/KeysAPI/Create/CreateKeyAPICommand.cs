using Domain.Keys;
using ErrorOr;
using MediatR;

namespace Application.KeysAPI.Create;


public record CreateKeyAPICommand(
    string Key,
    KeyType KeyType,
    bool Active,
    string UserId
) : IRequest<ErrorOr<Guid>>;