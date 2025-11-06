namespace Meeting.Hub.Application.CommandQuerys.Reservas.Imp;

public record ConsultaReservaCommand(
    long? ReservaId,
    DateTime? DataInicio,
    DateTime? DataFim) : IRequest<IEnumerable<ReservaMessageResponse>>;
