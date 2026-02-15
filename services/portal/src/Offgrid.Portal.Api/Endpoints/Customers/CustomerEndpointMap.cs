using Offgrid.Framework.AspNetCore.Http.Filters;
using Offgrid.Portal.Customers.Application.Commands.ReinstateCustomer;
using Offgrid.Portal.Customers.Application.Commands.SuspendCustomer;

namespace Offgrid.Portal.Api.Endpoints.Customers;

public static class CustomerEndpointMap
{
    internal const string PREFIX = "/customers";

    public static WebApplication MapCustomerEndpoints(this WebApplication app)
    {
        app
            .MapGroup(PREFIX)
            .MapGetCustomerById()
            .MapGetAllCustomers()
            .MapReinstateCustomer()
            .MapSuspendCustomer();
        return app;
    }

    private static RouteGroupBuilder MapGetCustomerById(this RouteGroupBuilder route)
    {
        route
            .MapGet("/{customerId}", GetCustomerByIdActionHandler.GetCustomerByIdAsync)
            .WithName(GetCustomerByIdActionHandler.EndpointName)
            .RequireAuthorization();
        return route;
    }

    private static RouteGroupBuilder MapGetAllCustomers(this RouteGroupBuilder route)
    {
        route
            .MapGet("", GetAllCustomersActionHandler.GetAllCustomersAsync)
            .WithName(GetAllCustomersActionHandler.EndpointName)
            .RequireAuthorization();
        return route;
    }

    private static RouteGroupBuilder MapReinstateCustomer(this RouteGroupBuilder route)
    {
        route
            .MapPost("/{customerId}/reinstate", ReinstateCustomerActionHandler.ReinstateCustomerAsync)
            .AddEndpointFilter<ValidationFilter<ReinstateCustomerCommand>>()
            .WithName(ReinstateCustomerActionHandler.EndpointName)
            .RequireAuthorization();
        return route;
    }

    private static RouteGroupBuilder MapSuspendCustomer(this RouteGroupBuilder route)
    {
        route
            .MapPost("/{customerId}/suspend", SuspendCustomerActionHandler.SuspendCustomerAsync)
            .AddEndpointFilter<ValidationFilter<SuspendCustomerCommand>>()
            .WithName(SuspendCustomerActionHandler.EndpointName)
            .RequireAuthorization();
        return route;
    }
}
