namespace Meeting.Hub.Domain.Reservas.Validators;

public class SalaValidator : AbstractValidator<Sala>
{
    public SalaValidator()
    {
        RuleFor(s => s.Nome)
            .NotEmpty()
            .WithMessage("O nome da sala é obrigatório.");        
    }
}