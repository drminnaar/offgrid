using Offgrid.Portal.ProductSearch.Domain.Entities;

namespace Offgrid.Portal.ProductSearch.Domain.Services;

/// <summary>
/// Defines the contract for a product catalog service that provides access to available products.
/// This interface abstracts the underlying data source and retrieval logic, allowing for
/// flexibility in how products are stored and accessed. The primary responsibility of this service
/// is to provide a method for retrieving a list of products that are currently available for sale,
/// which can then be indexed by the product search indexer.
/// </summary>
public interface IProductCatalog
{
    /// <summary>
    /// Retrieves a list of available products from the catalog. This method filters out any
    /// products that are not currently available for sale, ensuring that only products that
    /// can be purchased are returned.
    /// <remarks>
    /// The implementation of this method should consider factors such as stock availability,
    /// product status, and any other relevant criteria to determine whether a product is
    /// considered "available". This allows the product search indexer to focus on indexing
    /// only those products that customers can actually buy, improving the relevance and
    /// accuracy of search results.
    /// </remarks>
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A list of available products.</returns>
    Task<IReadOnlyList<Product>> GetAvailableProductsAsync(CancellationToken cancellationToken = default);
}
