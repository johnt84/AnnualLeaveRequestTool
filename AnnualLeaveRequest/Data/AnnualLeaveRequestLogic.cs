using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestDAL;
using System;
using System.Collections.Generic;

namespace AnnualLeaveRequest.Data
{
    public class AnnualLeaveRequestLogic : IAnnualLeaveRequestLogic
    {
        private readonly AnnualLeaveRequestDataAccess _annualLeaveRequestDataAccess;

        public AnnualLeaveRequestLogic(AnnualLeaveRequestDataAccess annualLeaveRequestDataAccess)
        {
            _annualLeaveRequestDataAccess = annualLeaveRequestDataAccess;
        }

        public List<int> GetYears()
        {
            return _annualLeaveRequestDataAccess.GetYears();
        }

        public List<AnnualLeaveRequestOverviewModel> GetRequestForYear(int year)
        {
            return _annualLeaveRequestDataAccess.GetRequestForYear(year);   
        }

        public AnnualLeaveRequestOverviewModel GetRequest(int annualLeaveRequestID)
        {
            return _annualLeaveRequestDataAccess.GetRequest(annualLeaveRequestID);
        }

        public decimal GetDaysBetweenStartDateAndReturnDate(DateTime startDate, DateTime returnDate)
        {
            return _annualLeaveRequestDataAccess.GetDaysBetweenStartDateAndReturnDate(startDate, returnDate);
        }

        public AnnualLeaveRequestOverviewModel Create(AnnualLeaveRequestOverviewModel model)
        {
            return _annualLeaveRequestDataAccess.Create(model);
        }

        public AnnualLeaveRequestOverviewModel Update(AnnualLeaveRequestOverviewModel model)
        {
            return _annualLeaveRequestDataAccess.Update(model);
        }

        public void Delete(AnnualLeaveRequestOverviewModel model)
        {
            _annualLeaveRequestDataAccess.Delete(model);
        }
    }
}
