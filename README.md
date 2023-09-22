# Download Throttling Sample

Middleware for throttling download speeds in ASP.NET Core applications

## Documentation

### EnableDownloadThrottingAttribute

Enables download throttling for the target controllers or endpoints.

**Parameters:**

- `bytesPerSecond` (_int_): The maximum allowed download speed in bytes per second.

**Example:**

```csharp
[EnableDownloadThrottling(2_000_000)]
public async Task<IActionResult> GetWithThrottling()
```

<hr>

### DisableDownloadThrottingAttribute

Disables download throttling for the target controllers or endpoints.

**Parameters:** None

**Example:**

```csharp
[DisableDownloadThrottling()]
public async Task<IActionResult> GetWithoutThrottling()
```

<hr>

### UseDownloadThrottling

Adds middleware for throttling download speeds.

**Parameters:**

- `builder` (_IApplicationBuilder_)

**Example:**

```csharp
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.UseDownloadThrottling();
```

## Sample Application 

### Setup

Add execute permissions for `start.sh` and `client-test.sh` scripts:

```bash
chmod +x start.sh client-test.sh
```

### Usage

Run `./start.sh` to start the ASP.NET Core sample application. 

#### Testing

Optionally, run `./client-test.sh` to test the download throttling functionality, after starting the sample application.

This test script uses `curl` to make several requests to the sample application's `GET /minimal/throttled` and prints the average download speed of each request (in bytes per second) to the console. The actual download speeds average 99,000 bytes (0.099 MBs) per second, and do not exceed the enforced limit of 0.1 MBs per second.

### Endpoints

The sample includes the following endpoints, all of which return the sample CSV file included with the project.

#### Minimal Endpoints

Throttled:
  - `GET /minimal/throttled`: limited to 0.1 MBs per second
    
Not Throttled:
  - `GET /minimal/not-throttled`

#### Controller Endpoints

Throttled:
  - `GET /controller/throttled`: limited to 0.1 MBs per second
  - `GET /controller/throttled-override`: limited to 0.2 MBs per second

Not Throttled:
  - `GET /controller/not-throttled`

## Future Enhancements

- [ ] Add tests for the `DownloadThrottling` project.
- [ ] Allow the download rate limits to be configured via `appsettings.json`.
