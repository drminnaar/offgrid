using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace MongoInit.Data;

public sealed class ProductFile
{
    private readonly ILogger<ProductFile> _logger;
    private readonly string _filePath;

    public ProductFile(ILogger<ProductFile> logger, IOptions<Configuration.FileOptions> options)
    {
        _logger = logger;
        _filePath = options.Value.FilePath;
    }

    public async Task SaveProductsAsync(List<Product> products)
    {
        var json = products.ToJson(new JsonWriterSettings
        {
            OutputMode = JsonOutputMode.CanonicalExtendedJson,
            Indent = true
        });

        // Serialize products to JSON and save to file
        var filePath = Path.Combine(_filePath, "products.json");
        await File.WriteAllTextAsync(filePath, json);
        _logger.LogInformation("💾 Saved {ProductCount} products to the file '{FilePath}'.", products.Count, filePath);
    }
}
