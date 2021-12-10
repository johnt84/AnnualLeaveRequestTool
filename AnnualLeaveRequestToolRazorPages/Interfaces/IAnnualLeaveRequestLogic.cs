using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestToolRazorPages.Models.ViewModels;
using System.Collections.Generic;

namespace AnnualLeaveRequestToolRazorPages.Interfaces
{
    public interface IAnnualLeaveRequestLogic
    {
        bool IsValidAnnualLeaveRequest(AnnualLeaveRequestOverviewModel annualLeaveRequest);
        List<int> GetYears();
        AnnualLeaveRequestOverviewViewModel GetRequestsForYear(int year);
        AnnualLeaveRequestOverviewModel GetRequest(int annualLeaveRequestID);
        AnnualLeaveRequestOverviewModel Create(AnnualLeaveRequestOverviewModel model);
        AnnualLeaveRequestOverviewModel Update(AnnualLeaveRequestOverviewModel model);
        void Delete(int annualLeaveRequestId);
        AnnualLeaveRequestCreateViewModel GetCreateViewModelForCreate(string errorMessage = "");
        AnnualLeaveRequestCreateViewModel GetCreateViewModelForEdit(int annualLeaveRequestID, string errorMessage = "");
    }
}
