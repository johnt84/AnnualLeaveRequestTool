using AnnualLeaveRequestEFDAL.Models;

namespace AnnualLeaveRequestEFDAL.DataAccess.Interfaces
{
    public interface IAnnualLeaveRequestEFDataAccess
    {
        List<int> GetYears();
        List<AnnualLeaveRequestsOverview> GetRequestsForYear(int year);
        AnnualLeaveRequestsOverview? GetRequest(int annualLeaveRequestID);
        decimal GetDaysBetweenStartDateAndReturnDate(DateTime startDate, DateTime returnDate);
        List<AnnualLeaveRequestsOverview> Create(Models.AnnualLeaveRequest model);
        List<AnnualLeaveRequestsOverview> Update(Models.AnnualLeaveRequest model);
        void Delete(Models.AnnualLeaveRequest model);
    }
}
