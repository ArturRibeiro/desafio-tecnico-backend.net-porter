namespace Meeting.Hub.Infrastructure.Interceptors.Audit;

public class AuditLog
{
    public int Id { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string EntityName { get; private set; }
    public string? EntityId { get; private set; }
    public string Action { get; private set; }
    public string? OldValues { get; private set; }
    public string? NewValues { get; private set; }

    public static AuditLog CreateInstance(DateTime timestamp, string entityName, string? entityId,
        string action, string? oldValues, string? newValues) =>
        new()
        {
            Timestamp = timestamp, EntityName = entityName, EntityId = entityId, Action = action,
            OldValues = oldValues, NewValues = newValues
        };
}