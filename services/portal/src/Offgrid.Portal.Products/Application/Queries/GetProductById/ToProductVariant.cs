namespace Offgrid.Portal.Products.Application.Queries.GetProductById;

public static partial class GetProductByIdHandlerExtensions
{
    public static ProductVariant ToProductVariantDto(this Domain.Entities.ProductVariant productVariant) => new()
    {
        Sku = productVariant.Sku,
        Name = productVariant.Name,
        PriceModifier = productVariant.PriceModifier,
        Attributes = productVariant.Attributes,
        StockQuantity = productVariant.StockQuantity,
        ImageUrl = productVariant.ImageUrl,
    };
}
