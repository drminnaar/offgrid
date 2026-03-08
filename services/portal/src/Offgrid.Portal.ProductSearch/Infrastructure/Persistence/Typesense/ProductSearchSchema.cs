using Typesense;

namespace Offgrid.Portal.Products.SyncJobProcessor.Infrastructure.Persistence.Typesense;

public sealed class ProductSearchSchema
{
    public static Schema GetSchema(string collectionName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(collectionName);
        return new Schema(
            name: collectionName,
            fields:
            [
                // identity fields
                new("id", FieldType.String, optional: false, facet: false),
                new("productId", FieldType.String, optional: false, facet: false),
                new("productSku", FieldType.String, optional: false, facet: false),
                new("variantSku", FieldType.String, optional: false, facet: false),

                // searchable fields
                new("name", FieldType.String, optional: false, facet: false),
                new("variantName", FieldType.String, optional: false, facet: false),
                new("description", FieldType.String, optional: false, facet: false),
                new("type", FieldType.String, optional: false, facet: true),
                new("brand", FieldType.String, optional: false, facet: true),
                new("category", FieldType.String, optional: false, facet: true),
                new("subcategory", FieldType.String, optional: false, facet: true),

                // free-form array of strings that can be used for search
                new("features", FieldType.StringArray, optional: false, facet: false),

                // pricing fields
                new(name: "isOnSale", type: FieldType.Bool, optional: false, facet: true),
                new(name: "salePercentage", type: FieldType.Int32, optional: false, facet: false),
                new(name: "basePrice", type: FieldType.Float, optional: false, facet: false),
                new(name: "currentPrice", type: FieldType.Float, optional: false, facet: false),

                // object that allows filtering on specs.weight, specs.frameSize etc. at query time
                new("specifications", FieldType.Object, optional: true, facet: false),

                // variant facets
                new Field("color",    FieldType.String, optional: false, facet: true),
                new Field("colorHex",FieldType.String, optional: false, facet: false) { Index = false },
                new Field("size",     FieldType.String, optional: true, facet: true),
                new Field("package",  FieldType.String, optional: true, facet: true),
                new Field("buildKit", FieldType.String, optional: true, facet: true),
                new Field("finSetup", FieldType.String, optional: true, facet: true),

                // stock
                new Field("totalStock", FieldType.Int32, optional: false, facet: false),
                new Field("hasStock",   FieldType.Bool,  optional: false, facet: true),

                // sorting
                new Field("createdAtUnixTimeSeconds", FieldType.Int64, optional: false, facet: false),
                new Field("updatedAtUnixTimeSeconds", FieldType.Int64, optional: false, facet: false),

                // metadata
                new Field("imageUrl", FieldType.String, optional: false, facet: false) { Index = false },
            ],
            defaultSortingField: "currentPrice"
        )
        {
            EnableNestedFields = true
        };
    }
}
