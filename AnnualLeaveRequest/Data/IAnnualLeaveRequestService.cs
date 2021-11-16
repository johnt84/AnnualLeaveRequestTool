using AnnualLeaveRequest.Models;
using System;
using System.Collections.Generic;

namespace AnnualLeaveRequest.Data
{
    public interface IAnnualLeaveRequestService
    {
        public List<int> GetYears();
        public List<AnnualLeaveRequestOverviewModel> GetRequestForYear(int year);
        public AnnualLeaveRequestOverviewModel GetRequest(int annualLeaveRequestID);
        public decimal GetDaysBetweenStartDateAndReturnDate(DateTime startDate, DateTime returnDate);
        public AnnualLeaveRequestOverviewModel Create(AnnualLeaveRequestOverviewModel model);
        public AnnualLeaveRequestOverviewModel Update(AnnualLeaveRequestOverviewModel model);
        public void Delete(AnnualLeaveRequestOverviewModel model);
    }
}
