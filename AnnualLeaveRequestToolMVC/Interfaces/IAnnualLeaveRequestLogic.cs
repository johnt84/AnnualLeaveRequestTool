﻿using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestToolMVC.Models.ViewModels;
using System.Collections.Generic;

namespace AnnualLeaveRequestToolMVC.Interfaces
{
    public interface IAnnualLeaveRequestLogic
    {
        List<int> GetYears();
        AnnualLeaveRequestOverviewViewModel GetRequestsForYear(int year);
        AnnualLeaveRequestOverviewModel GetRequest(int annualLeaveRequestID);
        AnnualLeaveRequestOverviewModel Create(AnnualLeaveRequestOverviewModel model);
        AnnualLeaveRequestOverviewModel Update(AnnualLeaveRequestOverviewModel model);
        void Delete(int annualLeaveRequestId);
        AnnualLeaveRequestCreateViewModel GetCreateViewModelForCreate(string errorMessage = "");
        AnnualLeaveRequestCreateViewModel GetCreateViewModelForEdit(int annualLeaveRequestID, string errorMessage = "");
    }
}
