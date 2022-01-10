using AnnualLeaveRequestToolWebForms.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnnualLeaveRequestToolWebForms.Data
{
    interface IAnnualLeaveRequestLogic
    {
        Task<List<int>> GetYearsAsync();
        Task<List<AnnualLeaveRequestOverviewModel>> GetRequestsForYearAsync(int year);
        Task<AnnualLeaveRequestOverviewModel> GetRequestAsync(int annualLeaveRequestID);
        //Task<decimal> GetDaysBetweenStartDateAndReturnDate(DateTime startDate, DateTime returnDate);
        Task<AnnualLeaveRequestOverviewModel> CreateAsync(AnnualLeaveRequestOverviewModel model);
        Task<AnnualLeaveRequestOverviewModel> UpdateAsync(AnnualLeaveRequestOverviewModel model);
        Task DeleteAsync(AnnualLeaveRequestOverviewModel model);
    }
}
