using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestMinimalAPI.Models;
using System;
using System.Collections.Generic;

namespace AnnualLeaveRequestMinimalAPI.Interfaces
{
    public interface IAnnualLeaveRequestLogic
    {
        List<int> GetYears();
        List<AnnualLeaveRequestOverviewModel> GetRequestsForYear(int year);
        AnnualLeaveRequestOverviewModel GetRequest(int annualLeaveRequestID);
        decimal GetDaysBetweenStartDateAndReturnDate(DateTime startDate, DateTime returnDate);
        AnnualLeaveRequestCRUDModel Create(AnnualLeaveRequestCRUDModel model);
        AnnualLeaveRequestCRUDModel Update(AnnualLeaveRequestCRUDModel model);
        void Delete(int annualLeaveRequestId);
    }
}
