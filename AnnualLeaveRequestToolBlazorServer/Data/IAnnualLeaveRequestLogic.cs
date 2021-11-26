using AnnualLeaveRequest.Shared;
using System;
using System.Collections.Generic;

namespace AnnualLeaveRequestToolBlazorServer.Data
{
    interface IAnnualLeaveRequestLogic
    {
        List<int> GetYears();
        List<AnnualLeaveRequestOverviewModel> GetRequestForYear(int year);
        AnnualLeaveRequestOverviewModel GetRequest(int annualLeaveRequestID);
        decimal GetDaysBetweenStartDateAndReturnDate(DateTime startDate, DateTime returnDate);
        AnnualLeaveRequestOverviewModel Create(AnnualLeaveRequestOverviewModel model);
        AnnualLeaveRequestOverviewModel Update(AnnualLeaveRequestOverviewModel model);
        void Delete(AnnualLeaveRequestOverviewModel model);
    }
}
