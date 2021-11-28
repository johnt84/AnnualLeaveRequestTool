using AnnualLeaveRequest.Shared;
using System.Collections.Generic;

namespace AnnualLeaveRequestAPI.Interfaces
{
    public interface IAnnualLeaveRequestLogic
    {
        List<AnnualLeaveRequestOverviewModel> GetRequestsForYear(int year);
        AnnualLeaveRequestOverviewModel GetRequest(int year, int annualLeaveRequestID);
        AnnualLeaveRequestOverviewModel GetRequest(int annualLeaveRequestID);
        AnnualLeaveRequestOverviewModel Create(AnnualLeaveRequestOverviewModel model);
        AnnualLeaveRequestOverviewModel Update(AnnualLeaveRequestOverviewModel model);
        void Delete(int annualLeaveRequestId);
    }
}
