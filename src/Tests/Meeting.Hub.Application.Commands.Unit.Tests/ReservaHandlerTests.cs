namespace Meeting.Hub.Application.Commands.Unit.Tests;

public class ReservaHandlerTests
{
    private Mock<IReservaRepository> reservaRepository = new();
    
    [Theory(DisplayName = "Deve criar reserva com sucesso.")]
    [ClassData(typeof(DeveCriarReservaValidaFaker))]
    public async Task Handle_DeveCriarReservaValida(CriaReservaCommand command)
    {
        // Arrange

        // Stub's
        reservaRepository.Setup(x => x.SaveAsync(It.IsAny<Reserva>())).ReturnsAsync(It.IsAny<Reserva>());
        
        var handler = new ReservaHandler(reservaRepository.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        reservaRepository.Verify(r => r.SaveAsync(It.Is<Reserva>(s =>
            s.Sala.Nome == command.Nome &&
            s.DataInicio == command.Inicio &&
            s.DataFim == command.Fim
        )), Times.Once);
    }
}