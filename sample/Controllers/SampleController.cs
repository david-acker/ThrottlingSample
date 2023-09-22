using Microsoft.AspNetCore.Mvc;
using DownloadThrottling;

namespace DownloadThrottling.Sample.Controllers;

[ApiController]
[Route("controller")]
[EnableDownloadThrottling(1_000_000)]
public sealed class SampleController : ControllerBase
{
    private readonly byte[] _data;

    public SampleController()
    {
        _data = System.IO.File.ReadAllBytes("test-data.csv");
    }

    [HttpGet("throttled")]
    public IActionResult GetWithThrottling()
    {
        return File(_data, contentType: "text/csv", fileDownloadName: "test-data.csv");
    }

    [HttpGet("throttled-override")]
    [EnableDownloadThrottling(2_000_000)]
    public IActionResult GetWithDifferentThrottling()
    {
        return File(_data, contentType: "text/csv", fileDownloadName: "test-data.csv");
    }
    
    [HttpGet("not-throttled")]
    [DisableDownloadThrottling]
    public IActionResult GetWithoutThrottling()
    {
        return File(_data, contentType: "text/csv", fileDownloadName: "test-data.csv");
    }
}
