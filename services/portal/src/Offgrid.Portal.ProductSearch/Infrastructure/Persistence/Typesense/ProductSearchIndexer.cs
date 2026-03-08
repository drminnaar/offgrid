using Microsoft.Extensions.Logging;
using Offgrid.Portal.Products.SyncJobProcessor.Infrastructure.Persistence.Typesense;
using Offgrid.Portal.ProductSearch.Domain.Entities;
using Offgrid.Portal.ProductSearch.Domain.Services;
using Typesense;

namespace Offgrid.Portal.ProductSearch.Infrastructure.Persistence.Typesense;

public sealed class ProductSearchIndexer : IProductSearchIndexer
{
    /// <summary>
    /// The name of the Typesense collection used for indexing products. This should match the
    /// collection name defined in the ProductSearchSchema class.
    /// </summary>
    public const string CollectionName = "products";

    private readonly ITypesenseClient _client;
    private readonly ILogger<ProductSearchIndexer> _logger;

    public ProductSearchIndexer(ITypesenseClient client, ILogger<ProductSearchIndexer> logger)
    {
        ArgumentNullException.ThrowIfNull(client, nameof(client));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        _client = client;
        _logger = logger;
    }

    public async Task ClearCollectionAsync()
    {
        try
        {
            await _client.TruncateCollection(CollectionName);
        }
        catch (TypesenseApiNotFoundException)
        {
            // collection doesn't exist, nothing to truncate
            _logger.LogInformation("Typesense collection '{TypesenseCollectionName}' does not exist, skipping truncation.", CollectionName);
        }
    }

    public async Task DeleteAndRecreateCollectionAsync(CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(CollectionName);
        try
        {
            if (await CollectionExistsAsync(CollectionName, cancellationToken))
            {
                await _client.DeleteCollection(CollectionName);
                _logger.LogInformation("Deleted existing Typesense collection '{TypesenseCollectionName}'.", CollectionName);
                var schema = ProductSearchSchema.GetSchema(CollectionName);
                await _client.CreateCollection(schema);
                _logger.LogInformation("Created Typesense collection '{TypesenseCollectionName}'.", CollectionName);
            }
        }
        catch (TypesenseApiNotFoundException)
        {
            // collection doesn't exist, nothing to delete
            _logger.LogInformation("Typesense collection '{TypesenseCollectionName}' does not exist, skipping deletion.", CollectionName);
        }
    }

    public async Task IndexCollectionAsync(
        IReadOnlyList<ProductSearchDocument> products,
        int batchSize = IProductSearchIndexer.DefaultBatchSize,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(CollectionName);

        await EnsureCollectionExistsAsync(cancellationToken);

        foreach (var batch in products.Chunk(batchSize))
        {
            await ImportProductsAsync(batch);
        }
    }

    private async Task<bool> CollectionExistsAsync(string collectionName, CancellationToken cancellationToken = default)
    {
        try
        {
            await _client.RetrieveCollection(collectionName, cancellationToken);
            return true;
        }
        catch (TypesenseApiNotFoundException)
        {
            return false;
        }
    }

    private async Task EnsureCollectionExistsAsync(CancellationToken cancellationToken = default)
    {
        if (await CollectionExistsAsync(CollectionName, cancellationToken))
        {
            return;
        }
        var schema = ProductSearchSchema.GetSchema(CollectionName);
        await _client.CreateCollection(schema);
        _logger.LogInformation("Typesense collection '{TypesenseCollectionName}' did not exist, created new collection.", CollectionName);
    }

    private async Task ImportProductsAsync(IReadOnlyList<ProductSearchDocument> products)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(CollectionName);
        ArgumentNullException.ThrowIfNull(products);
        var importResults = await _client.ImportDocuments(CollectionName, products, products.Count, ImportType.Upsert);

        if (importResults.Any(result => !result.Success))
        {
            var failedImports = importResults.Where(result => !result.Success).ToList();
            _logger.LogError("Failed to import {TypesenseFailedCount} products into Typesense collection '{TypesenseCollectionName}'", failedImports.Count, CollectionName);
            foreach (var failed in failedImports)
            {
                _logger.LogError("Product import failed: {TypesenseErrorMessage}", failed.Error);
            }
            throw new Exception($"Failed to import {failedImports.Count} products into Typesense collection '{CollectionName}'. See logs for details.");
        }
    }
}
