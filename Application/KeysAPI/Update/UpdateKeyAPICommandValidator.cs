using FluentValidation;

namespace Application.KeysAPI.Update;


public class UpdateKeyAPICommandValidator : AbstractValidator<UpdateKeyAPICommand> {

    public UpdateKeyAPICommandValidator()
    {
        RuleFor(r => r.Key)
        .NotEmpty()
        .MaximumLength(60)
        .WithName("Nombre llave");
    }
}