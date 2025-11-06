namespace Meeting.Hub.Infrastructure.Interceptors.Audit;

internal sealed class AuditLogInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null) LogEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void LogEntities(DbContext context)
    {
        var entities = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added 
                        || e.State == EntityState.Modified 
                        || e.State == EntityState.Deleted);
        
        foreach (var entry in entities)
        {
            var auditLog = AuditLog.CreateInstance(timestamp: DateTime.UtcNow
                , entityName: entry.Entity.GetType().Name
                , entityId: GetEntityId(entry)
                , action: entry.State.ToString()
                , oldValues: GetOldValues(entry.State, entry.OriginalValues)
                , newValues: GetNewValues(entry.State, entry.CurrentValues));

            Log.Information("AuditLog {@Audit}", JsonSerializer.Serialize(auditLog));
        }
    }

    private static readonly Func<EntityEntry, string?> GetEntityId
        = entry
            => entry.State != EntityState.Added ? GetPrimaryKey(entry) : null;

    private static readonly Func<EntityState, PropertyValues, string?> GetOldValues 
        = (state, values) 
            => IsEntityStateOldValues(state) ? Serializar(ConvertDictionary(values)) : null;
    
    private static readonly Func<EntityState, PropertyValues, string?> GetNewValues
        = (state, values) 
            => IsEntityStateNewValues(state) ? Serializar(ConvertDictionary(values)) : null;

    private static readonly Func<EntityState, bool> IsEntityStateOldValues
        = state 
            => state is EntityState.Modified or EntityState.Deleted;

    private static readonly Func<EntityState, bool> IsEntityStateNewValues
        = state 
            => state is EntityState.Added or EntityState.Modified;

    private static string? GetPrimaryKey(EntityEntry entry)
    {
        var pk = entry.Metadata.FindPrimaryKey();
        if (pk == null) return null;
        var valores = pk.Properties.Select(p => entry.Property(p.Name).CurrentValue?.ToString());
        return string.Join(",", valores);
    }
    
    private static readonly Func<PropertyValues, Dictionary<string, object?>> ConvertDictionary 
        = values 
            => values.Properties.ToDictionary(p => p.Name, p => values[p.Name]);
    
    private static readonly Func<Dictionary<string, object?>, string> Serializar 
        = dict 
            => JsonSerializer.Serialize(dict);
}