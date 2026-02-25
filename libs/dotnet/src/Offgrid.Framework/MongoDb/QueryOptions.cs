using MongoDB.Driver;

namespace Offgrid.Framework.MongoDb;

/// <summary>
/// Represents the options for querying a MongoDB collection.
/// </summary>
/// <typeparam name="TMongoEntity">The type of the MongoDB entity.</typeparam>
public sealed record QueryOptions<TMongoEntity> where TMongoEntity : class, IMongoEntity
{
    /// <summary>
    /// The default page number for the query.
    /// </summary>
    public const int DefaultPage = 1;

    /// <summary>
    /// The default page size for the query.
    /// </summary>
    public const int DefaultPageSize = 20;

    /// <summary>
    /// Gets or sets the page number for the query.
    /// </summary>
    /// <value>The page number.</value>
    public int Page { get; set; } = DefaultPage;

    /// <summary>
    /// Gets or sets the page size for the query.
    /// </summary>
    /// <value>The page size.</value>
    public int PageSize { get; set; } = DefaultPageSize;

    /// <summary>
    /// Gets or sets the collation strength for the query.
    /// </summary>
    /// <remarks>
    /// Collation strength determines how string comparison is performed.
    /// Strength 1 (Primary): Base character comparison (case and accent insensitive)
    /// Strength 2 (Secondary): Case insensitive, accent sensitive
    /// Strength 3 (Tertiary): Case sensitive, accent sensitive
    /// Strength 4 (Quaternary): Case sensitive, accent sensitive, and punctuation sensitive
    /// </remarks>
    /// <value>The collation strength.</value>
    public CollationStrength Collation { get; set; } = CollationStrength.Primary;

    /// <summary>
    /// Gets or sets the sort definition for the query.
    /// <remarks>
    /// The default sort is by Id in descending order. You can customize this to sort by any field(s) as needed.
    /// </remarks>
    /// </summary>
    /// <value>The sort definition.</value>
    public SortDefinition<TMongoEntity> SortDefinition { get; set; } = Builders<TMongoEntity>.Sort.Descending(e => e.Id);
}
