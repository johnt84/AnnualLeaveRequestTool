using AnnualLeaveRequestToolMVC.Models;
using System.Collections.Generic;

namespace AnnualLeaveRequestToolMVC.Interfaces
{
    public interface IAnnualLeaveRequestLogic
    {
        List<AnnualLeaveRequestOverviewModel> GetRequestsForYear(int year);
        AnnualLeaveRequestOverviewModel GetRequest(int annualLeaveRequestID);
        AnnualLeaveRequestOverviewModel Create(AnnualLeaveRequestOverviewModel model);
        AnnualLeaveRequestOverviewModel Update(AnnualLeaveRequestOverviewModel model);
        void Delete(int annualLeaveRequestId);
    }
}
