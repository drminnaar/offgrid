var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/", () =>
{
    return TypedResults.Ok("Shop API is running ...");
});

app.Run();
