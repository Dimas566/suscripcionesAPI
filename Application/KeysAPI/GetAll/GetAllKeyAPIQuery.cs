using ErrorOr;
using KeysAPI.Common;
using MediatR;

namespace Application.KeysAPI.GetAll;

public record GetAllKeyAPIQuery() : IRequest<ErrorOr<IReadOnlyList<KeyAPIResponse>>>;