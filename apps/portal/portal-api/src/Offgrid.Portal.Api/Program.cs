var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        name = "Offgrid - Portal Backoffice API",
        version = "1.0.0",
        description = "Backoffice API for Offgrid Portal application",
        _links = new { }
    });
});

app.Run();
