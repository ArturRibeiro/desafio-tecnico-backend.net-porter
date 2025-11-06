namespace Meeting.Hub.Application.Commands.Unit.Tests.Fakers;

public class DeveCriarReservaValidaFaker : TheoryData<CriaReservaCommand>
{
    public DeveCriarReservaValidaFaker()
    {
        var command = new CriaReservaCommand()
        {
            Nome = "Sala A",
            Inicio = new DateTime(2025, 11, 2, 14, 0, 0),
            Fim = new DateTime(2025, 11, 2, 15, 0, 0),
            ReservadoPor = "Artur Ribeiro"
        };
        
        Add(command);
    }
}