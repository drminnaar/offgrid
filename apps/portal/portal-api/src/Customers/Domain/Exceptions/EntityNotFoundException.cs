namespace Offgrid.Customers.Domain.Exceptions;

[Serializable]
public sealed class EntityNotFoundException : Exception
{
    public EntityNotFoundException() { }
    public EntityNotFoundException(string message) : base(message) { }
    public EntityNotFoundException(string message, Exception inner) : base(message, inner) { }

    public string EntityKey { get; init; } = string.Empty;
    public string EntityType { get; init; } = string.Empty;
}
