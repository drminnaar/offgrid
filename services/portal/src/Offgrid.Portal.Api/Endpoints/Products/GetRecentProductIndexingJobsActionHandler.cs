using Microsoft.AspNetCore.Mvc;
using Offgrid.Portal.ProductSearch.Application.Queries.GetRecentIndexingJobs;

namespace Offgrid.Portal.Api.Endpoints.Products;

public static class GetRecentProductIndexingJobsActionHandler
{
    public const string EndpointName = "GetRecentProductIndexingJobs";

    public static async Task<IResult> GetRecentProductIndexingJobsAsync(
        [FromServices] IGetRecentIndexingJobsQueryHandler handler,
        [FromQuery(Name = "count")] int count,
        CancellationToken token = default)
    {
        var result = await handler.HandleAsync(count, token);

        return TypedResults.Ok(result);
    }
}
