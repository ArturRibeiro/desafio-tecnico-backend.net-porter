namespace Meeting.Hub.Application.Commands.Reservas.Imp;

public class ReservaHandler(IReservaRepository reservaRepository)
    : IRequestHandler<CriaReservaCommand>, IRequestHandler<AlteraReservaCommand, Unit>, IRequestHandler<RemoveReservaCommand, Unit>
{
    public async Task Handle
        (CriaReservaCommand command, CancellationToken cancellationToken)
    {
        var reserva = await reservaRepository.GetAsync(expression: x => x.Sala.Nome == command.Nome, include: x => x.Sala.Reservas);
        
        if (reserva is not null) reserva.Sala.AddReserva(MapCommandParaNovaReserva(command));
        else reserva = MapCommandParaNovaReserva(command);
        
        reserva.Validate();
        await reservaRepository.SaveAsync(reserva);
    }
    
    public async Task<Unit> Handle(AlteraReservaCommand command, CancellationToken cancellationToken)
    {
        var reserva = await reservaRepository.GetAsync(x => x.Id == command.ReservaId);
        reserva.Alterar(command.Inicio, command.Fim);
        await reservaRepository.SaveAsync(reserva);
        return Unit.Value;
    }
    
    public async Task<Unit> Handle
        (RemoveReservaCommand command, CancellationToken cancellationToken)
    {
        await reservaRepository.RemoveAsync(x => x.Id == command.ReservaId);
        return Unit.Value;
    }
    
    private static Func<CriaReservaCommand, Reserva> MapCommandParaNovaReserva = command 
        => new Reserva(sala: new Sala(command.Nome), inicio: command.Inicio, fim: command.Fim
            , reservadoPor: command.ReservadoPor);
}