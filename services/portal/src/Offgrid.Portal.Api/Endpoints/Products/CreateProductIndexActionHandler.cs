using Microsoft.AspNetCore.Mvc;
using Offgrid.Framework.AspNetCore.Http.Extensions;
using Offgrid.Portal.ProductSearch.Application.Commands.CreateIndexingJob;

namespace Offgrid.Portal.Api.Endpoints.Products;

public static class CreateProductIndexActionHandler
{
    public const string EndpointName = "CreateProductIndex";

    public static async Task<IResult> CreateProductIndexAsync(
        [FromServices] ICreateIndexingJobHandler createIndexingJob,
        HttpContext httpContext,
        CancellationToken token = default)
    {
        var _ = httpContext
            .Username()
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        var result = await createIndexingJob.HandleAsync(token);

        return TypedResults.Created($"/products/indexes/{result.JobId}", result);
    }
}
