using ErrorOr;
using MediatR;

namespace Application.KeysAPI.Seed;

public sealed class SeedKeyAPICommand : IRequest<ErrorOr<Unit>>
{
    public int Count { get; set; }

    public SeedKeyAPICommand(int count)
    {
        Count = count;
    }
}
