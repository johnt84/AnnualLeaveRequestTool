namespace AnnualLeaveRequestServerFunctions.Services;

public interface IWarmUpLogic
{
    Task WarmUpAsync(CancellationToken cancellationToken = default);
}
