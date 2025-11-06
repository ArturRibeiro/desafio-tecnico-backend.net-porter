namespace Meeting.Hub.Domain.Unit.Tests.Reservas.Fakers;

public class DeveAdicionarUmaReservaValidaQuandoNaoHaConflitosFaker : TheoryData<Reserva, Reserva>
{
    public DeveAdicionarUmaReservaValidaQuandoNaoHaConflitosFaker()
    {
        var sala = Builder<Sala>.CreateNew().Build();

        var reserva = Builder<Reserva>.CreateNew()
            .With(x => x.Sala, sala)
            .With(x => x.DataInicio, DateTime.Now)
            .With(x => x.DataFim, DateTime.Now.AddHours(1))
            .Build();

        Add(reserva, reserva);
    }
}