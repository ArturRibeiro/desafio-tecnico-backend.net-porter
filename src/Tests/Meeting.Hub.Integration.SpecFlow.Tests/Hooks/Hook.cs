namespace Meeting.Hub.Integration.SpecFlow.Tests.Hooks;

[Binding]
public class Hook
{
    private static CustomWebApplicationFactory Factory => new CustomWebApplicationFactory();
    public static HttpClient Client => Factory.CreateClient();

    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:alpine")
        .WithDatabase("testdb")
        .WithUsername("postgres")
        .WithPassword("password")
        .WithAutoRemove(true)
        .Build();
    
    public static string ConnectionString;

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        await _postgreSqlContainer.StartAsync();
        ConnectionString = _postgreSqlContainer.GetConnectionString();
        Console.WriteLine($"DefaultConnection: {ConnectionString}");
        
        using var scope = Factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var sala = CreateSala("Sala de Treinamento");
        sala.AddReserva(CreateReserva("10/11/2025 08:00", "15/11/2025 09:45", "Artur Ribeiro"));
        sala.AddReserva(CreateReserva("10/11/2025 10:00", "15/11/2025 11:15", "Artur Ribeiro"));
        sala.AddReserva(CreateReserva("10/11/2025 11:30", "15/11/2025 12:00", "Artur Ribeiro"));
        sala.AddReserva(CreateReserva("11/02/2025 14:00", "11/02/2025 15:00", "Artur Ribeiro"));
        context.Salas.Add(sala);
        (context.SaveChanges() > 0).Should().BeTrue();
    }

    public static Func<string, DateTime> ParseDate = data 
        => DateTime.ParseExact(data, "dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture);
    
    private static Func<string, Sala> CreateSala = nome 
        => new Sala(nome);

    // private static Func<Sala, string, string, string, Sala> CreateReserva =
    //     (sala, inicio, fim, reservadoPor) 
    //         => sala.AddReserva(new Reserva(ParseDate(inicio), ParseDate(fim), reservadoPor));
    
    private static Func<string, string, string, Reserva> CreateReserva =
        (inicio, fim, reservadoPor) 
            => new Reserva(ParseDate(inicio), ParseDate(fim), reservadoPor);

    [AfterScenario]
    public async Task AfterScenario()
    {
        await _postgreSqlContainer.DisposeAsync();
    }
}