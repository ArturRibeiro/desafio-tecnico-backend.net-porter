using Meeting.Hub.Infrastructure.Interceptors.Audit;

namespace Meeting.Hub.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<AuditLogInterceptor>();
        services.AddApplicationInfrastructureCommandQuerys();
        services.AddApplicationInfrastructureCommand();
        services.AddScoped<IReservaRepository, ReservaRepository>();
        services.AddDbContext<IApplicationDbContextReading, ApplicationDbContextReading>(x => x.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
            (sp, options) => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                 .AddInterceptors(sp.GetRequiredService<AuditLogInterceptor>()));
    }
    
    public static void Seed(this IServiceProvider @this, IConfiguration app)
    {
        if (app.GetSection("Environment").Value == "Test") return;
        var context =  @this.GetRequiredService<ApplicationDbContext>();
        // //if (!context.Database.CanConnect())
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Seed();
    }
}