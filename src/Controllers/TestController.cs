using Microsoft.AspNetCore.Mvc;
using ThrottlingSample.Middleware;

namespace ThrottlingSample.Controllers;

[ApiController]
[Route("api")]
[ThrottleDownload(1_000_000)]
public sealed class TestController : ControllerBase
{
    private readonly byte[] _data;

    public TestController()
    {
        _data = System.IO.File.ReadAllBytes("test-data.csv");
    }

    [HttpGet("throttled")]
    public IActionResult GetWithThrottling()
    {
        return File(_data, contentType: "text/csv", fileDownloadName: "test-data.csv");
    }

    [HttpGet("throttled-differently")]
    [ThrottleDownload(2_000_000)]
    public IActionResult GetWithDifferentThrottling()
    {
        return File(_data, contentType: "text/csv", fileDownloadName: "test-data.csv");
    }
    
    [HttpGet("not-throttled")]
    [DisableThrottleDownload]
    public IActionResult GetWithoutThrottling()
    {
        return File(_data, contentType: "text/csv", fileDownloadName: "test-data.csv");
    }
}
