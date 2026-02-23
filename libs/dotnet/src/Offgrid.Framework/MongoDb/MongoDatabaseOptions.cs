namespace Offgrid.Framework.MongoDb;

public sealed record MongoDatabaseOptions
{
    public const string ConfigurationSectionName = "MongoDatabaseOptions";
    public required string ConnectionString { get; init; }
    public required string DatabaseName { get; init; }
}
