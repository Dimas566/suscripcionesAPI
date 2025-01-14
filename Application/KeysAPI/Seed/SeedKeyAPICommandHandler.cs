using Domain.Keys;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.KeysAPI.Seed;

internal sealed class SeedKeyAPICommandHandler : IRequestHandler<SeedKeyAPICommand, ErrorOr<Unit>>
{
    private readonly IKeyRepository keyRepository;
    private readonly IUnitOfWork unitOfWork;

    public SeedKeyAPICommandHandler(IKeyRepository keyRepository, IUnitOfWork unitOfWork)
    {
        this.keyRepository = keyRepository ?? throw new ArgumentNullException(nameof(keyRepository));
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(SeedKeyAPICommand command, CancellationToken cancellationToken)
    {
        var random = new Random();
        var keyTypes = Enum.GetValues(typeof(KeyType)).Cast<KeyType>().ToArray();

        // Generar los registros
        for (int i = 0; i < command.Count; i++)
        {
            var keyAPI = new KeyAPI(
                new KeyID(Guid.NewGuid()),
                GenerateLicenseKey(), 
                keyTypes[random.Next(keyTypes.Length)],        
                true,                                         
                Guid.NewGuid().ToString()                     
            ); 

            keyRepository.Add(keyAPI);
        }

        // Guardar los cambios
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private string GenerateLicenseKey()
    {
        // Formato del nombre de clave: XXXX-XXXX-XXXX-XXXX (16 caracteres)
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();

        return string.Join("-", Enumerable.Range(0, 4).Select(_ =>
            new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray())));
    }
}
