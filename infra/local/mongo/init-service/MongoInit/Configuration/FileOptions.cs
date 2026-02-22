namespace MongoInit.Configuration;

public sealed class FileOptions
{
    public const string ConfigurationSectionName = "FileOptions";

    public string FilePath { get; set; } = string.Empty;
}
