namespace Meeting.Hub.Application.CommandQuerys.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplicationInfrastructureCommandQuerys(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}