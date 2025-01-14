using FluentValidation;

namespace Application.KeysAPI.Delete;

public class DeleteKeyAPICommandValidator : AbstractValidator<DeleteKeyAPICommand>
{
    public DeleteKeyAPICommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty();
    }
}