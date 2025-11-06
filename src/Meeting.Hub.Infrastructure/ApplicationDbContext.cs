using Meeting.Hub.Infrastructure.Interceptors.Audit;

namespace Meeting.Hub.Infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext 
{
    private const string SCHEMA = "meeting";
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // A partir da versão 6.0, o Npgsql passou a mapear:
        // DateTime em UTC → para timestamp with time zone (timestamptz)
        // DateTime Local ou Unspecified → para timestamp without time zone
        //     Enviar um DateTime que não esteja em UTC para um campo timestamptz gera exceção.
        //     DateTime também é suportado para timestamptz, mas apenas com offset zero (UTC).
        //     Antes da versão 6.0, o timestamptz era convertido para horário local ao ser lido.
        //     Há melhorias e mudanças incompatíveis detalhadas nos breaking changes da versão 6.0.
        //https://www.npgsql.org/doc/types/datetime.html
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Sala> Salas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SCHEMA);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    
    private IDbContextTransaction dbContextTransaction;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        dbContextTransaction = await Database.BeginTransactionAsync(cancellationToken);
    }
    
    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);
            dbContextTransaction?.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (dbContextTransaction != null) DisposeTransaction();
        }
    }
    
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        try
        {
            await dbContextTransaction?.RollbackAsync(cancellationToken);
        }
        finally
        {
            DisposeTransaction();
        }
    }
    
    private void DisposeTransaction()
    {
        try
        {
            dbContextTransaction.Dispose();
            dbContextTransaction = null;
        }
        catch
        {
            // Optionally handle or log any exceptions that occur during disposal.
        }
    }

    public void Seed()
    {
        var sala = new Sala("Sala de Treinamento");
        var dateTime = DateTime.Now.Date;
        sala.AddReserva(new Reserva(dateTime.AddHours(1), dateTime.AddHours(2), "Artur Ribeiro"));
        sala.AddReserva(new Reserva(dateTime.AddHours(3), dateTime.AddHours(4), "Artur Ribeiro"));
        sala.AddReserva(new Reserva(dateTime.AddHours(5), dateTime.AddHours(6), "Artur Ribeiro"));
        sala.AddReserva(new Reserva(dateTime.AddHours(7), dateTime.AddHours(8), "Artur Ribeiro"));
        sala.AddReserva(new Reserva(dateTime.AddHours(9), dateTime.AddHours(10), "Artur Ribeiro"));
        sala.AddReserva(new Reserva(dateTime.AddHours(11), dateTime.AddHours(12), "Artur Ribeiro"));
        sala.AddReserva(new Reserva(dateTime.AddHours(13), dateTime.AddHours(14), "Artur Ribeiro"));
        sala.AddReserva(new Reserva(dateTime.AddHours(15), dateTime.AddHours(16), "Artur Ribeiro"));
        sala.AddReserva(new Reserva(dateTime.AddHours(17), dateTime.AddHours(18), "Artur Ribeiro"));
        sala.AddReserva(new Reserva(dateTime.AddHours(19), dateTime.AddHours(20), "Artur Ribeiro"));
        Salas.Add(sala);
        SaveChanges();
    }
}