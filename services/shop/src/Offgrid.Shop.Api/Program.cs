using Offgrid.Framework.Configuration.Extensions;
using Offgrid.Framework.AspNetCore.Http.Middleware.Extensions;
using Offgrid.Framework.EntityFrameworkCore.Extensions;
using Offgrid.Shop.Customers.Infrastructure.Persistence;
using Offgrid.Shop.Api.Endpoints.Customers;
using Offgrid.Shop.Api.Endpoints.Root;
using Offgrid.Shop.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// configure general API services
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandlers();

// configure security API services
builder.Services.AddCors(builder.Configuration);
builder.Services.AddKeycloakAuth(
    builder.Configuration,
    enableJwtBearerEventLogging: !builder.Environment.IsProduction());
builder.Services.AddAuthorization();

// configure module services
builder.Services.AddOffgridDbContext<IAppDbContext, AppDbContext>(
    builder.Configuration,
    enableDetailedErrors: !builder.Environment.IsProduction(),
    enableSensitiveDataLogging: !builder.Environment.IsProduction());
builder.Services.AddCustomerServices();

var app = builder.Build();

// configure middleware pipeline relating to error handling
app.UseExceptionHandler();

// configure middleware pipeline relating to security
app.UseCors();
app.UseAuthentication();
app.UseUnauthorizedProblemDetailsMiddleware();
app.UseAuthorization();

// configure endpoints
app.MapRootEndpoint();
app.MapCustomerEndpoints();

app.Run();
