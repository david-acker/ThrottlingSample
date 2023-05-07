using Microsoft.AspNetCore.Mvc;
using ThrottlingSample.Middleware;

namespace ThrottlingSample.Controllers;

[ApiController]
[Route("test")]
public sealed class TestController : ControllerBase
{
    private readonly byte[] _data;

    public TestController()
    {
        _data = System.IO.File.ReadAllBytes("test-data.csv");
    }

    [HttpGet("not-throttled")]
    public IActionResult GetWithoutThrottling()
    {
        return File(_data, contentType: "text/csv", fileDownloadName: "test-data.csv");
    }

    [HttpGet("throttled")]
    [ThrottleDownload(1_000_000)]
    public IActionResult GetWithThrottling()
    {
        return File(_data, contentType: "text/csv", fileDownloadName: "test-data.csv");
    }
}
