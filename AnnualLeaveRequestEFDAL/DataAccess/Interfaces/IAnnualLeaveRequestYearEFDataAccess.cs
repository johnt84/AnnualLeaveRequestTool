using AnnualLeaveRequestEFDAL.Models;

namespace AnnualLeaveRequestEFDAL.DataAccess.Interfaces
{
    public interface IAnnualLeaveRequestYearEFDataAccess
    {
        List<AnnualLeaveYear> GetAnnualLeaveYears();
        AnnualLeaveYear GetAnnualLeaveYear(int year);
        AnnualLeaveYear Create(AnnualLeaveYear model);
        AnnualLeaveYear Update(AnnualLeaveYear model);
        void Delete(AnnualLeaveYear model);
    }
}
