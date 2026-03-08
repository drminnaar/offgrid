using Microsoft.AspNetCore.Mvc;
using Offgrid.Portal.ProductSearch.Application.Queries.GetCurrentIndexingJob;

namespace Offgrid.Portal.Api.Endpoints.Products;

public static class GetCurrentProductIndexJobActionHandler
{
    public const string EndpointName = "GetCurrentProductIndexJob";

    public static async Task<IResult> GetCurrentProductIndexJobAsync(
        [FromServices] IGetCurrentIndexingJobQueryHandler queryHandler,
        CancellationToken token = default)
    {
        var result = await queryHandler.HandleAsync(token);

        return TypedResults.Ok(result);
    }
}
