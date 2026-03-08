namespace Offgrid.Portal.ProductSearch.Indexor.Infrastructure.Configuration;

/// <summary>
/// Configuration options for the indexing process.
/// </summary>
public sealed record IndexingOptions
{
    /// <summary>
    /// The name of the configuration section in appsettings.json that corresponds to these options.
    /// </summary>
    public const string SectionName = "IndexingOptions";

    /// <summary>
    /// The polling interval in seconds for the indexing process.
    /// </summary>
    public int PollingIntervalSeconds { get; set; }

    /// <summary>
    /// The polling interval as a <see cref="TimeSpan"/> for the indexing process.
    /// </summary>
    public TimeSpan PollingInterval => TimeSpan.FromSeconds(PollingIntervalSeconds);
}
