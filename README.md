# ASP.NET Core Download Throttling Sample

## Running

Run `./start.sh` to start the ASP.NET Core sample application. The sample application uses the download throttling middleware and has a single endpoint (`GET /`) which returns a test CSV file included with the project.

## Testing

Run `./client-test.sh` to test the download throttling functionality. The script makes several requests to the sample application using `curl` and prints the average download speed (in bytes per second) when each request completes.

## Metrics

The sample project is currently setup to limit the download speed to 100,000 bytes (0.1 MB) per second. Using the `./client-test.sh` script, the throttled download speeds average roughly 99,000 bytes (0.099 MB) per second and will do not exceed the enforced limit of 0.1 MBs per second.

## Future Enhancements

- Allow the download rate limit to be configured via `appsettings.json`.
- Allow for granular download throttling by adding a `ThrottleDownloadAttribute`, along with the desired download rate limit, to specific endpoints or controllers.
