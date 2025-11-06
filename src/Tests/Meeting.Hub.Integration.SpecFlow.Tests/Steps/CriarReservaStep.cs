namespace Meeting.Hub.Integration.SpecFlow.Tests.Steps;

[Binding]
[Collection("CustomWebApplicationFactory")]
public class CriarReservaStep : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly ScenarioContext _scenarioContext;
    private readonly int QuantidadeReservas = 4;

    public CriarReservaStep(CustomWebApplicationFactory factory, ScenarioContext scenarioContext)
    {
        _factory = factory;
        _scenarioContext = scenarioContext;
        _factory.ApplicationDbContext.Should().NotBeNull();
    }

    [Given(@"que já existe algumas reservas")]
    public async Task DadoQueJaExisteUmaReservaParaADasAsPor()
    {
        // Seed aplicado ??
        
        var salas = await _factory.ApplicationDbContext
            .Salas.Include(x => x.Reservas)
            .ToListAsync();
        
        salas.Should().HaveCountGreaterThanOrEqualTo(1);
        salas.SelectMany(x => x.Reservas).ToList().Should()
            .HaveCountGreaterThanOrEqualTo(QuantidadeReservas);

    }

    [When(@"eu tento criar uma nova reserva para a (.*) das (.*) às (.*) por (.*)")]
    public async Task QuandoEuTentoCriarUmaNovaReservaParaADasAsPor(string sala, string dataInicio, string dataFim, string reservadoPor)
    {
        var command = new CriaReservaCommand()
        {
            Nome = sala
            , Inicio = Hook.ParseDate(dataInicio)
            , Fim = Hook.ParseDate(dataFim)
            , ReservadoPor = reservadoPor
        };
        
        var result = await _factory.SendAsync<Result>(command, "criar");

        _scenarioContext.Add(nameof(Result), result);
    }

    [Then(@"a reserva não deve ser salva")]
    public async Task EntaoAReservaDeveSerSalvaComSucesso()
    {
        var result = _scenarioContext.Get<Result>(nameof(Result));
        result.Should().NotBeNull();
        result.Title.Should().Be("one or more validation errors occurred.");
        result.Errors.Should().HaveCount(1);
        result.Errors[0].Should().Be("Conflito de horário para a sala Sala de Treinamento Data: 10/11/2025 horário de início: 08:00 e fim: 09:45.");

        var salas = await _factory.ApplicationDbContext
            .Salas.Include(x => x.Reservas)
            .ToListAsync();
        
        salas.Should().HaveCountGreaterThanOrEqualTo(1);
        salas.SelectMany(x => x.Reservas).ToList().Should()
            .HaveCountGreaterThanOrEqualTo(QuantidadeReservas);
    }
}