using Offgrid.Portal.ProductSearch.Indexor.Infrastructure.DependencyInjection;
using Offgrid.Portal.ProductSearch.Infrastructure.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddProductSearchInfrastructure(builder.Configuration, builder.Environment)
    .AddProductSearchApplication()
    .AddProductSearchIndexor(builder.Configuration);

var host = builder.Build();
host.Run();
