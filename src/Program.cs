using ThrottlingSample.Middleware;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseDownloadThrottling();

var data = File.ReadAllBytes("test-data.csv");
app.MapGet("/", () => Results.File(data, contentType: "text/csv", fileDownloadName: "test-data.csv"));

app.Run();
