using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestAPI.Models;
using System.Collections.Generic;

namespace AnnualLeaveRequestAPI.Interfaces
{
    public interface IAnnualLeaveRequestLogic
    {
        List<AnnualLeaveRequestOverviewModel> GetRequestsForYear(int year);
        AnnualLeaveRequestOverviewModel GetRequest(int year, int annualLeaveRequestID);
        AnnualLeaveRequestOverviewModel GetRequest(int annualLeaveRequestID);
        AnnualLeaveRequestCRUDModel Create(AnnualLeaveRequestCRUDModel model);
        AnnualLeaveRequestCRUDModel Update(AnnualLeaveRequestCRUDModel model);
        void Delete(int annualLeaveRequestId);
    }
}
