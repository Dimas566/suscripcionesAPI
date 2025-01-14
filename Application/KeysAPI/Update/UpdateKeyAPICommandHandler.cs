using Domain.Keys;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.KeysAPI.Update;


internal sealed class UpdateKeyAPICommandHandler : IRequestHandler<UpdateKeyAPICommand, ErrorOr<Guid>>
{
    private readonly IKeyRepository keyRepository;
    private readonly IUnitOfWork unitOfWork;

    public UpdateKeyAPICommandHandler(IKeyRepository keyRepository, IUnitOfWork unitOfWork)
    {
        this.keyRepository = keyRepository ?? throw new ArgumentNullException(nameof(keyRepository));
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Guid>> Handle(UpdateKeyAPICommand command, CancellationToken cancellationToken)
    {

        if (!await keyRepository.ExistsAsync(new KeyID(command.Id)))
        {
            return Error.NotFound("KeyAPI.NotFound", "The key api Id was not found.");
        }


        KeyAPI keyAPI = KeyAPI.UpdateKeyAPI(command.Id, command.Key, command.KeyType, command.Active, command.UserId);

        keyRepository.Update(keyAPI);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return keyAPI.IdKey.Guid;
    }
}
