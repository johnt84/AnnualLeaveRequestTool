using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestAPI.Interfaces;
using AnnualLeaveRequestDAL;
using System.Collections.Generic;

namespace AnnualLeaveRequestAPI.Logic
{
    public class AnnualLeaveRequestLogic : IAnnualLeaveRequestLogic
    {
        private readonly AnnualLeaveRequestDataAccess _annualLeaveRequestDataAccess;

        public AnnualLeaveRequestLogic(AnnualLeaveRequestDataAccess annualLeaveRequestDataAccess)
        {
            _annualLeaveRequestDataAccess = annualLeaveRequestDataAccess;
        }

        public List<AnnualLeaveRequestOverviewModel> GetRequestsForYear(int year)
        {
            return _annualLeaveRequestDataAccess.GetRequestForYear(year);
        }

        public AnnualLeaveRequestOverviewModel GetRequest(int year, int annualLeaveRequestID)
        {
            return _annualLeaveRequestDataAccess.GetRequest(year, annualLeaveRequestID);
        }

        public AnnualLeaveRequestOverviewModel GetRequest(int annualLeaveRequestID)
        {
            return _annualLeaveRequestDataAccess.GetRequest(annualLeaveRequestID);
        }

        public AnnualLeaveRequestOverviewModel Create(AnnualLeaveRequestOverviewModel model)
        {
            return _annualLeaveRequestDataAccess.Create(model);
        }

        public AnnualLeaveRequestOverviewModel Update(AnnualLeaveRequestOverviewModel model)
        {
            return _annualLeaveRequestDataAccess.Update(model);
        }

        public void Delete(int annualLeaveRequestId)
        {
            var model = GetRequest(annualLeaveRequestId);
            
            _annualLeaveRequestDataAccess.Delete(model);
        }
    }
}
