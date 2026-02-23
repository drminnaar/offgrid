using Offgrid.Framework.AspNetCore.Http.Middleware.Extensions;
using Offgrid.Framework.Configuration.Extensions;
using Offgrid.Framework.EntityFrameworkCore.Extensions;
using Offgrid.Portal.Api.Endpoints.Customers;
using Offgrid.Portal.Api.Endpoints.ProductBrands;
using Offgrid.Portal.Api.Endpoints.ProductCategories;
using Offgrid.Portal.Api.Endpoints.Products;
using Offgrid.Portal.Api.Endpoints.ProductTypes;
using Offgrid.Portal.Api.Endpoints.Root;
using Offgrid.Portal.Api.Extensions;
using Offgrid.Portal.Customers.Infrastructure.Persistence;

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
builder.Services.AddProductServices(builder.Configuration);

var app = builder.Build();

// configure middleware pipeline relating to error handling
app.UseExceptionHandler();

// configure middleware pipeline relating to security
app.UseCors();
app.UseAuthentication();
app.UseUnauthorizedProblemDetailsMiddleware();
app.UseAuthorization();

// map endpoints
app.MapRootEndpoint();
app.MapCustomerEndpoints();
app.MapProductEndpoints();
app.MapProductTypesEndpoints();
app.MapProductCategoriesEndpoints();
app.MapProductBrandsEndpoints();

app.Run();
