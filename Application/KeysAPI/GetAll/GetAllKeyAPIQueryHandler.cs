using KeysAPI.Common;
using Domain.Keys;
using MediatR;
using ErrorOr;

namespace Application.KeysAPI.GetAll;


internal sealed class GetAllKeyAPIQueryHandler : IRequestHandler<GetAllKeyAPIQuery, ErrorOr<IReadOnlyList<KeyAPIResponse>>>
{
    private readonly IKeyRepository keyRepository;

    public GetAllKeyAPIQueryHandler(IKeyRepository keyRepository)
    {
        this.keyRepository = keyRepository ?? throw new ArgumentNullException(nameof(keyRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<KeyAPIResponse>>> Handle(GetAllKeyAPIQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<KeyAPI> keyAPI = await keyRepository.GetAll();

        return keyAPI.Select(keyAPI => new KeyAPIResponse(
                keyAPI.IdKey.Guid,
                keyAPI.Key,
                keyAPI.KeyType.ToString(),
                keyAPI.Active,
                keyAPI.UserId
            )).ToList();
    }
}