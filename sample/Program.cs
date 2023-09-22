using DownloadThrottling;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();
app.UseDownloadThrottling();

var data = File.ReadAllBytes("test-data.csv");

app.MapGet("/minimal/throttled", () =>
{
    return Results.File(data, contentType: "text/csv", fileDownloadName: "test-data.csv");
})
.WithMetadata(new EnableDownloadThrottlingAttribute(100_000));

app.MapGet("/minimal/not-throttled", () =>
{
    return Results.File(data, contentType: "text/csv", fileDownloadName: "test-data.csv");
});

app.MapControllers();
app.Run();
