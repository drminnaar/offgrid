using Offgrid.Framework.MongoDb;

namespace Offgrid.Portal.Products.Application.Queries.GetProducts;

public sealed record GetProductsQuery : IMongoQuery
{
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 10;

    public int Page { get; init; } = DefaultPageNumber;

    public int PageSize { get; init; } = DefaultPageSize;
}
