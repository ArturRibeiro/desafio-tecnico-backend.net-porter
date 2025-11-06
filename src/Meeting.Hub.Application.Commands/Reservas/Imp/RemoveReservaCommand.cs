namespace Meeting.Hub.Application.Commands.Reservas.Imp;

public record RemoveReservaCommand(long ReservaId) : IRequest<Unit>;
