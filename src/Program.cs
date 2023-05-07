using ThrottlingSample.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseDownloadThrottling();

var data = File.ReadAllBytes("test-data.csv");
app.MapGet("/", () =>
{
    return Results.File(data, contentType: "text/csv", fileDownloadName: "test-data.csv");
})
.WithMetadata(new ThrottleDownloadAttribute(100_000));

app.MapControllers();

app.Run();
