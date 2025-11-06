namespace Meeting.Hub.Application.Commands.Reservas.Imp;

public record AlteraReservaCommand(long ReservaId, DateTime Inicio, DateTime Fim) : IRequest<Unit>;