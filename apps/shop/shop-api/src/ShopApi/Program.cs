using Offgrid.ShopApi.DependencyInjection;
using Offgrid.ShopApi.Endpoints.Customers;
using Offgrid.ShopApi.Endpoints.Root;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandlers();
builder.Services.AddCommonServices();
builder.Services.AddDbContext(builder.Configuration, builder.Environment);
builder.Services.AddCustomerServices();

var app = builder.Build();
app.UseExceptionHandler();
app.MapRootEndpoint();
app.MapCustomerEndpoints();
app.Run();
