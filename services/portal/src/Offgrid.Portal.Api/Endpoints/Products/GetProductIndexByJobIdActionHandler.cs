using Microsoft.AspNetCore.Mvc;
using Offgrid.Portal.ProductSearch.Application.Queries.GetIndexingJob;

namespace Offgrid.Portal.Api.Endpoints.Products;

public static class GetProductIndexByJobIdActionHandler
{
    public const string EndpointName = "GetProductIndexByJobId";

    public static async Task<IResult> GetProductIndexByJobIdAsync(
        [FromServices] IGetIndexingJobQueryHandler queryHandler,
        [FromRoute(Name = "jobId")] string jobId,
        CancellationToken token = default)
    {
        var result = await queryHandler.HandleAsync(Guid.Parse(jobId), token);

        return TypedResults.Ok(result);
    }
}
