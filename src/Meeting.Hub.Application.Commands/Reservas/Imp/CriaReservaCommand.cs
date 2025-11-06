namespace Meeting.Hub.Application.Commands.Reservas.Imp;

public record CriaReservaCommand : IRequest
{
    [Required]
    public string Nome { get; init; }

    [Required]
    public DateTime Inicio { get; init; }

    [Required]
    public DateTime Fim { get; init; }

    [Required]
    public string ReservadoPor { get; init; }
}
