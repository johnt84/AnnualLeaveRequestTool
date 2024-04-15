using AnnualLeaveRequestServerFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AnnualLeaveRequestServerFunctions;

public class WarmUp
{
    private readonly ILogger<WarmUp> _logger;
    private readonly IWarmUpLogic _warmUpLogic;

    public WarmUp(ILogger<WarmUp> logger, IWarmUpLogic warmUpLogic)
    {
        _logger = logger;
        _warmUpLogic = warmUpLogic;
    }

    [Function("WarmUp")]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("Warm up function started");

        await _warmUpLogic.WarmUpAsync();

        return new OkResult();
    }
}
