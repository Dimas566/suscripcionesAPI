using FluentValidation;

namespace Application.KeysAPI.Create;


public class CreateKeyAPICommandValidator : AbstractValidator<CreateKeyAPICommand> {

    public CreateKeyAPICommandValidator()
    {
        RuleFor(r => r.Key)
        .NotEmpty()
        .MaximumLength(60)
        .WithName("Nombre llave");
    }
}