namespace Meeting.Hub.Domain.Reservas.Validators;

public class ReservaValidator : AbstractValidator<Reserva>
{
    public ReservaValidator()
    {
        RuleFor(s => s.Sala).SetValidator(new SalaValidator());
        
        RuleFor(s => s.ReservadoPor)
            .NotEmpty()
            .WithMessage("O responsável pela reserva é obrigatório.");

        RuleFor(s => s.DataInicio)
            .LessThan(s => s.DataFim)
            .WithMessage("A data de início deve ser anterior à data de fim.");

        RuleFor(s => s)
            .Must(NaoConflitarComReservasExistentes)
            .WithMessage(s => $"Conflito de horário para a sala {s.Sala.Nome} Data: {ShortDateString(s)} horário de início: {HorarioInicial(s)} e fim: {HorarioFim(s)}.");
    }

    private static readonly Func<Reserva, bool> NaoConflitarComReservasExistentes = reserva =>
    {
        var reservaNova = reserva.Sala.Reservas.FirstOrDefault(x => x.IsNew);
        return !reserva.Sala.Reservas.Any(r => !r.IsNew && reservaNova.DataInicio < r.DataFim && r.DataInicio < reservaNova.DataFim);
    };

    private static readonly Func<Reserva, string> ShortDateString = reserva => reserva.Sala.Reservas.First(x => x.IsNew).DataInicio.Date.ToShortDateString();

    private static readonly Func<Reserva, string> HorarioInicial = reserva => reserva.Sala.Reservas.First(x => x.IsNew).DataInicio.ToString("HH:mm");

    private static readonly Func<Reserva, string> HorarioFim = reserva => reserva.Sala.Reservas.First(x => x.IsNew).DataFim.ToString("HH:mm");
}