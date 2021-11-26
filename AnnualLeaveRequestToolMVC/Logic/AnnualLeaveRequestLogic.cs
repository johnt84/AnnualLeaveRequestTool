using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestDAL;
using AnnualLeaveRequestToolMVC.Interfaces;
using AnnualLeaveRequestToolMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnnualLeaveRequestToolMVC.Logic
{
    public class AnnualLeaveRequestLogic : IAnnualLeaveRequestLogic
    {
        private readonly AnnualLeaveRequestDataAccess _annualLeaveRequestDataAccess;

        private static List<string> _paidLeaveTypes = new List<string>()
        {
            "Paid",
            "Unpaid",
        };

        private static List<string> _leaveTypes = new List<string>()
        {
            "Annual Leave",
            "Appointment",
            "Compassionate Leave",
        };

        public AnnualLeaveRequestLogic(AnnualLeaveRequestDataAccess annualLeaveRequestDataAccess)
        {
            _annualLeaveRequestDataAccess = annualLeaveRequestDataAccess;
        }

        public List<int> GetYears()
        {
            return _annualLeaveRequestDataAccess.GetYears();
        }

        public AnnualLeaveRequestOverviewViewModel GetRequestsForYear(int selectedYear)
        {
            var annualLeaveRequestsForYear = _annualLeaveRequestDataAccess.GetRequestForYear(selectedYear);

            var lastAnnualeaveRequestForYear = annualLeaveRequestsForYear.OrderBy(x => x.StartDate).Last();

            return new AnnualLeaveRequestOverviewViewModel()
            {
                SelectedYear = selectedYear,
                AnnualLeaveRequestsForYear = annualLeaveRequestsForYear,
                AnnualLeaveRequestOverviewForYear = lastAnnualeaveRequestForYear,
                Years = GetYears(),
                EditableYearSelected = selectedYear >= DateTime.UtcNow.Year,
            };
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

        public AnnualLeaveRequestCreateViewModel GetCreateViewModelForCreate()
        {
            return new AnnualLeaveRequestCreateViewModel()
            {
                PaidLeaveTypes = _paidLeaveTypes,
                LeaveTypes = _leaveTypes,
            };
        }
        
        public AnnualLeaveRequestCreateViewModel GetCreateViewModelForEdit(int annualLeaveRequestID)
        {
            var annualLeaveRequest = GetRequest(annualLeaveRequestID);

            return new AnnualLeaveRequestCreateViewModel()
            {
                AnnualLeaveRequestID = annualLeaveRequest.AnnualLeaveRequestID,
                PaidLeaveType = annualLeaveRequest.PaidLeaveType,
                LeaveType = annualLeaveRequest.LeaveType,
                StartDate = annualLeaveRequest.StartDate,
                ReturnDate = annualLeaveRequest.ReturnDate,
                Notes = annualLeaveRequest.Notes,
                PaidLeaveTypes = _paidLeaveTypes,
                LeaveTypes = _leaveTypes,
            };
        }
    }
}
