﻿using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestAPI.Interfaces;
using AnnualLeaveRequestAPI.Models;
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

        public AnnualLeaveRequestCRUDModel Create(AnnualLeaveRequestCRUDModel model)
        {
            var annnualLeaveRequestOverviewModel = new AnnualLeaveRequestOverviewModel()
            {
                Year = model.Year,
                PaidLeaveType = model.PaidLeaveType,
                LeaveType = model.LeaveType,
                StartDate = model.StartDate,
                ReturnDate = model.ReturnDate,
                Notes = model.Notes,
            };

            var newAnnnualLeaveRequestOverviewModel = _annualLeaveRequestDataAccess.Create(annnualLeaveRequestOverviewModel);

            return new AnnualLeaveRequestCRUDModel()
            {
                AnnualLeaveRequestID = newAnnnualLeaveRequestOverviewModel.AnnualLeaveRequestID,
                Year = newAnnnualLeaveRequestOverviewModel.Year,
                PaidLeaveType = newAnnnualLeaveRequestOverviewModel.PaidLeaveType,
                LeaveType = newAnnnualLeaveRequestOverviewModel.LeaveType,
                StartDate = newAnnnualLeaveRequestOverviewModel.StartDate,
                ReturnDate = newAnnnualLeaveRequestOverviewModel.ReturnDate,
                Notes = newAnnnualLeaveRequestOverviewModel.Notes,
                ErrorMessage = newAnnnualLeaveRequestOverviewModel.ErrorMessage,
            };
        }

        public AnnualLeaveRequestCRUDModel Update(AnnualLeaveRequestCRUDModel model)
        {
            var annnualLeaveRequestOverviewModel = new AnnualLeaveRequestOverviewModel()
            {
                AnnualLeaveRequestID = model.AnnualLeaveRequestID,
                Year = model.Year,
                PaidLeaveType = model.PaidLeaveType,
                LeaveType = model.LeaveType,
                StartDate = model.StartDate,
                ReturnDate = model.ReturnDate,
                Notes = model.Notes,
            };

            var updateAnnnualLeaveRequestOverviewModel = _annualLeaveRequestDataAccess.Update(annnualLeaveRequestOverviewModel);

            return new AnnualLeaveRequestCRUDModel()
            {
                AnnualLeaveRequestID = updateAnnnualLeaveRequestOverviewModel.AnnualLeaveRequestID,
                Year = updateAnnnualLeaveRequestOverviewModel.Year,
                PaidLeaveType = updateAnnnualLeaveRequestOverviewModel.PaidLeaveType,
                LeaveType = updateAnnnualLeaveRequestOverviewModel.LeaveType,
                StartDate = updateAnnnualLeaveRequestOverviewModel.StartDate,
                ReturnDate = updateAnnnualLeaveRequestOverviewModel.ReturnDate,
                Notes = updateAnnnualLeaveRequestOverviewModel.Notes,
                ErrorMessage = updateAnnnualLeaveRequestOverviewModel.ErrorMessage,
            };
        }

        public void Delete(int annualLeaveRequestId)
        {
            var model = GetRequest(annualLeaveRequestId);
            
            _annualLeaveRequestDataAccess.Delete(model);
        }
    }
}
