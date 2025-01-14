
using Domain.Keys;
using ErrorOr;
using KeysAPI.Common;
using MediatR;

namespace Application.KeysAPI.GetById;

internal sealed class GetKeyAPIByIdQueryHandler : IRequestHandler<GetKeyAPIByIdQuery, ErrorOr<KeyAPIResponse>>
{
    private readonly IKeyRepository keyRepository;

    public GetKeyAPIByIdQueryHandler(IKeyRepository keyRepository)
    {
        this.keyRepository = keyRepository ?? throw new ArgumentNullException(nameof(keyRepository));
    }

    public async Task<ErrorOr<KeyAPIResponse>> Handle(GetKeyAPIByIdQuery query, CancellationToken cancellationToken)
    {
        if (await keyRepository.GetByIdAsync(new KeyID(query.Id)) is not KeyAPI keyAPI)
        {
            return Error.NotFound("KeyAPI.NotFound", "The KeyAPI with the provide Id was not found.");
        }

        return new KeyAPIResponse(
            keyAPI.IdKey.Guid, 
            keyAPI.Key, 
            keyAPI.KeyType.ToString(),
            keyAPI.Active,
            keyAPI.UserId);
    }
}