namespace Meeting.Hub.Application.CommandQuerys.Unit.Tests.Fakers;

public class DeveRetornarReservasDentroIntervaloFaker : TheoryData<IQueryable<Reserva>, DateTime?, DateTime?, int?, int>
{
    public DeveRetornarReservasDentroIntervaloFaker()
    {
        var sala = Builder<Sala>.CreateNew().Build();
        var reservas = Builder<Reserva>
            .CreateListOfSize(10)
            .All()
            .With(x => x.Sala, sala)
            .With(x => x.DataInicio, DateTime.Now)
            .With(x => x.DataFim, DateTime.Now.AddHours(1))
            .Build().BuildMock();
        
        Add(reservas, new DateTime(2025, 1, 1), new DateTime(2025, 11, 12), null, 10);
    }
}

public class DeveRetornarReservasPorIdFaker : TheoryData<IQueryable<Reserva>, DateTime?, DateTime?, int?, int>
{
    public DeveRetornarReservasPorIdFaker()
    {
        var sala = Builder<Sala>.CreateNew().Build();
        var reservas = Builder<Reserva>
            .CreateListOfSize(10)
            .All()
            .With(x => x.Sala, sala)
            .With(x => x.DataInicio, DateTime.Now)
            .With(x => x.DataFim, DateTime.Now.AddHours(1))
            .Build().BuildMock();
        
        Add(reservas, null, null, 2, 1);
    }
}