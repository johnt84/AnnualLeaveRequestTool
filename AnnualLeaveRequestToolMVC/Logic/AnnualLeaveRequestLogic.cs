﻿using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestDapperDAL;
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
            var annualLeaveRequestsForYear = _annualLeaveRequestDataAccess.GetRequestsForYear(selectedYear);

            if(annualLeaveRequestsForYear == null || annualLeaveRequestsForYear.Count == 0)
            {
                return null;
            }

            var lastAnnualeaveRequestForYear = annualLeaveRequestsForYear.OrderBy(x => x.StartDate).Last();

            annualLeaveRequestsForYear.RemoveAll(x => x.AnnualLeaveRequestID == 0);

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

        public AnnualLeaveRequestCreateViewModel GetCreateViewModelForCreate(string errorMessage = "")
        {
            var errorMessageViewModel = GetErrorMesssageViewModel(errorMessage);

            return new AnnualLeaveRequestCreateViewModel()
            {
                PaidLeaveTypes = _paidLeaveTypes,
                LeaveTypes = _leaveTypes,
                ErrorMessageViewModel = errorMessageViewModel,
            };
        }
        
        public AnnualLeaveRequestCreateViewModel GetCreateViewModelForEdit(int annualLeaveRequestID, string errorMessage = "")
        {
            var annualLeaveRequest = GetRequest(annualLeaveRequestID);

            var editAnnualLeaveRequest = annualLeaveRequest != null 
                                            ? annualLeaveRequest 
                                            : new AnnualLeaveRequestOverviewModel();

            int year = annualLeaveRequest?.Year ?? DateTime.UtcNow.Year;

            var errorMessageViewModel = GetErrorMesssageViewModel(errorMessage);

            return new AnnualLeaveRequestCreateViewModel()
            {
                AnnualLeaveRequestID = editAnnualLeaveRequest.AnnualLeaveRequestID,
                PaidLeaveType = editAnnualLeaveRequest.PaidLeaveType,
                LeaveType = editAnnualLeaveRequest.LeaveType,
                StartDate = editAnnualLeaveRequest.StartDate,
                ReturnDate = editAnnualLeaveRequest.ReturnDate,
                Notes = editAnnualLeaveRequest.Notes,
                PaidLeaveTypes = _paidLeaveTypes,
                LeaveTypes = _leaveTypes,
                ErrorMessageViewModel = errorMessageViewModel,
                Year = year,
                IsEditable = year >= DateTime.UtcNow.Year,
            };
        }

        private ErrorMessageViewModel GetErrorMesssageViewModel(string errorMessage)
        {
            var errorMessageViewModel = new ErrorMessageViewModel();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                var errorMessages = errorMessage.Split("\\n").ToList();
                errorMessages.RemoveAll(x => string.IsNullOrEmpty(x));

                errorMessageViewModel.ErrorMessages = errorMessages;
            }

            return errorMessageViewModel;
        }
    }
}
