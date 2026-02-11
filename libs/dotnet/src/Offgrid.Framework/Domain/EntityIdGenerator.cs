namespace Offgrid.Framework.Domain;

public sealed class EntityIdGenerator : IEntityIdGenerator
{
    public Guid GenerateEntityId() => Guid.CreateVersion7();
}
