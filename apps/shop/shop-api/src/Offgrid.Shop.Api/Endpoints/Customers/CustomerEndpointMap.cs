using Offgrid.Framework.AspNetCore.Http.Filters;
using Offgrid.Shop.Customers.Application.Commands.UpsertCustomer;

namespace Offgrid.ShopApi.Endpoints.Customers;

public static class CustomerEndpointMap
{
    private const string PREFIX = "/customers";

    public static WebApplication MapCustomerEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(PREFIX);
        group.MapUpsertCustomer();
        return app;
    }

    private static RouteGroupBuilder MapUpsertCustomer(this RouteGroupBuilder route)
    {
        route
            .MapPost("", UpsertCustomerHandler.UpsertCustomerAsync)
            .AddEndpointFilter<ValidationFilter<UpsertCustomerCommand>>()
            .WithName(UpsertCustomerHandler.EndpointName)
            .RequireAuthorization();
        return route;
    }
}
