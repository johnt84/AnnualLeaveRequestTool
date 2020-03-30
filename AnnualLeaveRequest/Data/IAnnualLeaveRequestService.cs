using AnnualLeaveRequest.Models;
using System.Collections.Generic;

namespace AnnualLeaveRequest.Data
{
    interface IAnnualLeaveRequestService
    {
        List<int> GetYears();
        List<AnnualLeaveRequestOverviewModel> GetRequestForYear(int year);
        AnnualLeaveRequestOverviewModel GetRequest(int annualLeaveRequestID);
        AnnualLeaveRequestOverviewModel Create(AnnualLeaveRequestOverviewModel model);
        AnnualLeaveRequestOverviewModel Update(AnnualLeaveRequestOverviewModel model);
        void Delete(AnnualLeaveRequestOverviewModel model);
    }
}
