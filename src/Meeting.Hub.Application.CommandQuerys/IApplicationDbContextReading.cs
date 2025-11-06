namespace Meeting.Hub.Application.CommandQuerys;

public interface IApplicationDbContextReading
{
    IQueryable<TEntity> DbSet<TEntity>() where TEntity : class;
}