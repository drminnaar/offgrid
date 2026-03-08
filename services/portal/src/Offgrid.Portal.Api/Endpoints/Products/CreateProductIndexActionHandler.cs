using Microsoft.AspNetCore.Mvc;
using Offgrid.Portal.ProductSearch.Application.Commands.CreateIndexingJob;

namespace Offgrid.Portal.Api.Endpoints.Products;

public static class CreateProductIndexActionHandler
{
    public const string EndpointName = "CreateProductIndex";

    public static async Task<IResult> CreateProductIndexAsync(
        [FromServices] ICreateIndexingJobHandler createIndexingJob,
        CancellationToken token = default)
    {
        var result = await createIndexingJob.HandleAsync(token);

        return TypedResults.Created($"/products/indexes/{result.JobId}", result);
    }
}
