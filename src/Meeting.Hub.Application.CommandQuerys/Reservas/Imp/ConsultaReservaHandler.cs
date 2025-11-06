namespace Meeting.Hub.Application.CommandQuerys.Reservas.Imp;

public class ConsultaReservaHandler(IApplicationDbContextReading dbContextReading) : IRequestHandler<ConsultaReservaCommand, IEnumerable<ReservaMessageResponse>>
{
    public async Task<IEnumerable<ReservaMessageResponse>> Handle(ConsultaReservaCommand command
        , CancellationToken cancellationToken)
        => await dbContextReading
            .DbSet<Reserva>()
            .Where(GetExpression(command))
            .Select(reserva => new ReservaMessageResponse(reserva.Id.GetValueOrDefault()
                , reserva.DataInicio
                , reserva.DataFim
                , reserva.ReservadoPor
                , reserva.Sala.Id.GetValueOrDefault()
                , reserva.Sala.Nome))
            .ToListAsync(cancellationToken);

    private Func<ConsultaReservaCommand, Expression<Func<Reserva, bool>>> GetExpression = command => command switch
    {
        { DataInicio: not null, DataFim: not null } => x => x.DataInicio.Date >= command.DataInicio.Value.Date && x.DataFim.Date <= command.DataFim.Value.Date,
        { ReservaId: not null } => x => x.Id == command.ReservaId.Value,
        _ => x => true
    };
}