using AnnualLeaveRequestEFDAL.DataAccess.Interfaces;

namespace AnnualLeaveRequestServerFunctions.Services;

public class WarmUpLogic : IWarmUpLogic
{
    private readonly IAnnualLeaveRequestEFDataAccess _dataAccess;

    public WarmUpLogic(IAnnualLeaveRequestEFDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public async Task WarmUpAsync(CancellationToken cancellationToken = default)
    {
        await _dataAccess.GetRequestsForYearAsync(DateTime.UtcNow.Year, cancellationToken);
    }
}
