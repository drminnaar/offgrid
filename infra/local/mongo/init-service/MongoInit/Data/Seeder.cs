using Microsoft.Extensions.Logging;

namespace MongoInit.Data;

public sealed class Seeder
{
    private readonly ILogger<Seeder> _logger;
    private readonly ProductCollection _productCollection;
    private readonly ProductFile _productFile;

    public Seeder(ILogger<Seeder> logger, ProductCollection productCollection, ProductFile productFile)
    {
        _logger = logger;
        _productCollection = productCollection;
        _productFile = productFile;
    }

    public async Task SeedAsync(string environment, bool writeFile)
    {
        _logger.LogInformation("🚀  [{Environment}] Starting MongoDB seeding process...", environment);

        var products = new List<Product>();
        var fixedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var currentUnixTime = new DateTimeOffset(fixedDate).ToUnixTimeSeconds();

        // Generate Kayak products
        var kayakProducts = KayakDataGenerator.GenerateKayakData(currentUnixTime);
        _logger.LogInformation("🛶  Generated {KayakCount} kayak products.", kayakProducts.Count);
        products.AddRange(kayakProducts);

        // Generate Surfboard products
        var surfboardProducts = SurfboardDataGenerator.GenerateSurfboardData(currentUnixTime);
        _logger.LogInformation("🏄🏻‍♂️  Generated {SurfboardCount} surfboard products.", surfboardProducts.Count);
        products.AddRange(surfboardProducts);

        // Generate Bike products
        var bikeProducts = BikeDataGenerator.GenerateBikes(currentUnixTime);
        _logger.LogInformation("🚲  Generated {BikeCount} bike products.", bikeProducts.Count);
        products.AddRange(bikeProducts);

        if (writeFile)
        {
            await _productFile.SaveProductsAsync(products);
        }
        else
        {
            await _productCollection.SaveProductsAsync(products);
        }

        _logger.LogInformation("✅  Seeding completed successfully.");
    }
}
