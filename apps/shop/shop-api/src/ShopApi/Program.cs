using Offgrid.ShopApi.Extensions;
using Offgrid.ShopApi.ExceptionHandlers;
using Offgrid.ShopApi.Endpoints.Customers;
using Offgrid.ShopApi.Endpoints.Root;
using Offgrid.ShopApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// configure general API services
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandlers();

// configure API security
builder.Services.AddCorsUsingConfig(builder.Configuration);
builder.Services.AddKeycloakAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

// configure module services
builder.Services.AddCommonServices();
builder.Services.AddDbContext(builder.Configuration, builder.Environment);
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
