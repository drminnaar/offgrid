namespace MongoInit.Data;

public static class PriceDetailsGenerator
{
    public static (decimal basePrice, decimal currentPrice, bool isOnSale, int salePercentage) GeneratePriceDetails(
        int fromPrice,
        int toPrice,
        double chanceOfSale,
        int minSalePercentage,
        int maxSalePercentage,
        Random random)
    {
        var basePrice = Math.Round(random.Next(fromPrice, toPrice) - 0.01m, 2);
        var isOnSale = random.NextDouble() < chanceOfSale;

        var salePercentage = 0;
        if (isOnSale)
        {
            var input = random.Next(minSalePercentage, maxSalePercentage);
            var remainder = input % 5;
            salePercentage = input - remainder;
        }

        var currentPrice = isOnSale ? Math.Round(basePrice * (1 - salePercentage / 100m), 2) : basePrice;

        return (basePrice, currentPrice, isOnSale, salePercentage);
    }
}
