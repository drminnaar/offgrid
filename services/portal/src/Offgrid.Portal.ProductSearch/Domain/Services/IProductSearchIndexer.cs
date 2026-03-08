using Offgrid.Portal.ProductSearch.Domain.Entities;

namespace Offgrid.Portal.ProductSearch.Domain.Services;

/// <summary>
/// Defines the contract for a product search indexer that interacts with Typesense. This interface
/// abstracts the underlying implementation details of how products are indexed, allowing for
/// flexibility and separation of concerns. The methods in this interface cover the essential
/// operations needed to manage the product search index, including clearing existing data,
/// recreating the collection, and indexing new products in batches. Implementations of this
/// interface should ensure that the collection name used for indexing matches the one defined in
/// the ProductSearchSchema class to maintain consistency across the application.
/// </summary>
public interface IProductSearchIndexer
{
    /// <summary>
    /// The default batch size (number of documents) for indexing products in Typesense. This value
    /// is chosen to balance memory usage and indexing performance. Adjust as needed based on the
    /// size of your product documents and available resources.
    /// </summary>
    public const int DefaultBatchSize = 500;

    /// <summary>
    /// Clears all products from the Typesense collection without deleting the collection itself.
    /// If the collection does not exist, this method does nothing.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ClearCollectionAsync();

    /// <summary>
    /// Deletes the Typesense collection if it exists, and then recreates it with the correct schema. 
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAndRecreateCollectionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Indexes a collection of products in Typesense. The products are indexed in batches to
    /// optimize performance and resource usage. If the collection does not exist, it will be
    /// created.
    /// </summary>
    /// <param name="products">The collection of products to index.</param>
    /// <param name="batchSize">The number of products to index in each batch.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task IndexCollectionAsync(IReadOnlyList<ProductSearchDocument> products, int batchSize = DefaultBatchSize, CancellationToken cancellationToken = default);
}
