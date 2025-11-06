namespace Meeting.Hub.Application.CommandQuerys.Unit.Tests;

public class ConsultaReservaHandlerTests
{
    private readonly Mock<IApplicationDbContextReading> _dbContextReadingMock = new();
    private readonly ConsultaReservaHandler _handler;
    
    public ConsultaReservaHandlerTests() => _handler = new ConsultaReservaHandler(_dbContextReadingMock.Object);

    [Theory(DisplayName = "Deve retornar reservas dentro do intervalo de datas informado")]
    [ClassData(typeof(DeveRetornarReservasDentroIntervaloFaker))]
    [ClassData(typeof(DeveRetornarReservasPorIdFaker))]
    public async Task DeveRetornarReservasDentroDoIntervaloDeDatas(IQueryable<Reserva>? reservas, DateTime? dataInicio, DateTime? dataFim, int? reservaId, int quantidadeEsperada)
    {
        // Arrange

        // Stub's
        _dbContextReadingMock.Setup(x => x.DbSet<Reserva>()).Returns(reservas);

        var command = new ConsultaReservaCommand(ReservaId: reservaId, DataInicio: dataInicio, DataFim: dataFim);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().HaveCount(quantidadeEsperada);
    }
}