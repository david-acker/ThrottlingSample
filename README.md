# ASP.NET Core Download Throttling Sample

## Setup

Add execute permissions for `start.sh` and `client-test.sh` scripts:

```bash
chmod +x start.sh client-test.sh
```

## Running

Run `./start.sh` to start the ASP.NET Core sample application. The sample includes the following endpoints, all of which return the sample CSV file included with the project:

### Minimal Endpoints

- Throttled:
  - `GET /minimal/throttled`: limited to 0.1 MBs per second
- Not Throttled:
  - `GET /minimal/not-throttled`

### Controller Endpoints

- Throttled:
  - `GET /controller/throttled`: limited to 0.1 MBs per second
  - `GET /controller/throttled-override`: limited to 0.2 MBs per second
- Not Throttled:
  - `GET /controller/not-throttled`

## Testing

Run `./client-test.sh` to test the download throttling functionality. The script makes several requests to the sample application's `GET /minimal/throttled` using `curl` and prints the average download speed (in bytes per second) when each request completes.

The actual download speeds average 99,000 bytes (0.099 MBs) per second and do not exceed the enforced limit of 0.1 MBs per second.

## Future Enhancements

- [ ] Allow the download rate limit to be configured via `appsettings.json`.
- [x] Allow for granular download throttling by adding a `ThrottleDownloadAttribute`, along with the desired download rate limit, to specific endpoints or controllers.
