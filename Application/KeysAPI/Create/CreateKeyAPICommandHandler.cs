using Domain.Keys;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.KeysAPI.Create;


internal sealed class CreateKeyAPICommandHandler : IRequestHandler<CreateKeyAPICommand, ErrorOr<Guid>>
{
    private readonly IKeyRepository keyRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateKeyAPICommandHandler(IKeyRepository keyRepository, IUnitOfWork unitOfWork)
    {
        this.keyRepository = keyRepository ?? throw new ArgumentNullException(nameof(keyRepository));
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Guid>> Handle(CreateKeyAPICommand command, CancellationToken cancellationToken)
    {

        var keyAPI = new KeyAPI(
        new KeyID(Guid.NewGuid()),
        command.Key,
        command.KeyType,
        true,
        command.UserId
        );

        keyRepository.Add(keyAPI);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return keyAPI.IdKey.Guid;
    }
}
