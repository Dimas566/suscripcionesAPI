using Domain.Keys;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Application.KeysAPI.Delete;

internal sealed class DeleteKeyAPICommandHandler : IRequestHandler<DeleteKeyAPICommand, ErrorOr<Unit>>
{
    private readonly IKeyRepository keyRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteKeyAPICommandHandler(IKeyRepository keyRepository, IUnitOfWork unitOfWork)
    {
        this.keyRepository = keyRepository ?? throw new ArgumentNullException(nameof(keyRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteKeyAPICommand command, CancellationToken cancellationToken)
    {
        if (await keyRepository.GetByIdAsync(new KeyID(command.Id)) is not KeyAPI keyAPI)
        {
            return Error.NotFound("KeyAPI.NotFound", "The Key API with the provide Id was not found.");
        }

        keyRepository.Delete(keyAPI);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
