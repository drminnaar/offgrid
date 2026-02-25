namespace Offgrid.Portal.Products.Application.Queries.GetProductVariants;

public static partial class GetProductVariantsExtensions
{
    public static ProductVariantInfo ToProductVariantInfo(this Domain.Entities.ProductVariant variant)
    {
        ArgumentNullException.ThrowIfNull(variant, nameof(variant));

        return new ProductVariantInfo
        {
            Sku = variant.Sku,
            Name = variant.Name,
            PriceModifier = variant.PriceModifier,
            Attributes = variant.Attributes,
            StockQuantity = variant.StockQuantity,
            ImageUrl = variant.ImageUrl
        };
    }
}
