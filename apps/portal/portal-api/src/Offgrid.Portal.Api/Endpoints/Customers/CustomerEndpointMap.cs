using Offgrid.Framework.AspNetCore.Http.Filters;
using Offgrid.Portal.Customers.Application.Commands.ChangeCustomerStatus;

namespace Offgrid.Portal.Api.Endpoints.Customers;

public static class CustomerEndpointMap
{
    private const string PREFIX = "/customers";

    public static WebApplication MapCustomerEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(PREFIX);
        group.MapChangeCustomerStatus();
        return app;
    }

    private static RouteGroupBuilder MapChangeCustomerStatus(this RouteGroupBuilder route)
    {
        route
            .MapPut("/{customerId}/status", ChangeCustomerStatusHandler.ChangeCustomerStatusAsync)
            .AddEndpointFilter<ValidationFilter<ChangeCustomerStatusCommand>>()
            .WithName(ChangeCustomerStatusHandler.EndpointName)
            .RequireAuthorization();
        return route;
    }
}
