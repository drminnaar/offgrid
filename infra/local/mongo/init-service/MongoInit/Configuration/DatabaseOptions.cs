namespace MongoInit.Configuration;

public class DatabaseOptions
{
    public const string ConfigurationSectionName = "MongoDatabaseOptions";
    public required string ConnectionString { get; init; }
    public required string DatabaseName { get; init; }
}
