using AnnualLeaveRequestToolMVC.Interfaces;
using AnnualLeaveRequestToolMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnnualLeaveRequestToolMVC.Logic
{
    public class AnnualLeaveRequestLogic : IAnnualLeaveRequestLogic
    {
        private static List<AnnualLeaveRequestOverviewModel> _annualLeaveRequests = new List<AnnualLeaveRequestOverviewModel>()
        {
            new AnnualLeaveRequestOverviewModel()
            {
                AnnualLeaveRequestID = 1,

                Year = 2021,
                PaidLeaveType = "Paid",
                LeaveType = "Annual Leave",
                StartDate = new DateTime(2021,01,01),
                ReturnDate = new DateTime(2021,01,06),
                NumberOfDaysRequested = 4,
                NumberOfAnnualLeaveDaysRequested = 3,
                NumberOfPublicLeaveDaysRequested = 1,
                NumberOfDays = 28,
                NumberOfAnnualLeaveDays = 25,
                NumberOfPublicLeaveDays = 3,
                NumberOfDaysLeft = 24,
                NumberOfAnnualLeaveDaysLeft = 22,
                NumberOfPublicLeaveDaysLeft = 2,
                Notes = "Xmas and New Year",
            },
            new AnnualLeaveRequestOverviewModel()
            {
                AnnualLeaveRequestID = 2,
                PaidLeaveType = "Paid",
                LeaveType = "Annual Leave",
                Year = 2021,
                StartDate = new DateTime(2021,07,05),
                ReturnDate = new DateTime(2021,07,12),
                NumberOfDaysRequested = 5,
                NumberOfAnnualLeaveDaysRequested = 5,
                NumberOfPublicLeaveDaysRequested = 0,
                NumberOfDays = 28,
                NumberOfAnnualLeaveDays = 25,
                NumberOfPublicLeaveDays = 3,
                NumberOfDaysLeft = 19,
                NumberOfAnnualLeaveDaysLeft = 17,
                NumberOfPublicLeaveDaysLeft = 2,
                Notes = "Summer Holiday",
            },
             new AnnualLeaveRequestOverviewModel()
            {
                AnnualLeaveRequestID = 3,
                PaidLeaveType = "Paid",
                LeaveType = "Annual Leave",
                Year = 2021,
                StartDate = new DateTime(2021,12,20),
                ReturnDate = new DateTime(2021,12,31),
                NumberOfDaysRequested = 9,
                NumberOfAnnualLeaveDaysRequested = 7,
                NumberOfPublicLeaveDaysRequested = 2,
                NumberOfDays = 28,
                NumberOfAnnualLeaveDays = 25,
                NumberOfPublicLeaveDays = 3,
                NumberOfDaysLeft = 10,
                NumberOfAnnualLeaveDaysLeft = 10,
                NumberOfPublicLeaveDaysLeft = 0,
                Notes = "Xmas and New Year",
            },
        };

        private AnnualLeaveRequestOverviewModel GetAnnualLeaveRequest(int annualLeaveRequestID) => _annualLeaveRequests
                                                                                        .Where(x => x.AnnualLeaveRequestID == annualLeaveRequestID)
                                                                                        .FirstOrDefault();

        public List<AnnualLeaveRequestOverviewModel> GetRequestsForYear(int year)
        {
            var annualLeaveRequestsForYear = _annualLeaveRequests.Where(x => x.Year == year).ToList();

            decimal noOfDaysLeft = annualLeaveRequestsForYear.First().NumberOfDays;
            decimal noOfAnnualLeaveDaysLeft = annualLeaveRequestsForYear.First().NumberOfAnnualLeaveDays;
            decimal noOfPublicLeaveDaysLeft = annualLeaveRequestsForYear.First().NumberOfPublicLeaveDays;

            foreach (var annualLeaveRequest in annualLeaveRequestsForYear.OrderBy(x => x.StartDate).ToList())
            {
                noOfDaysLeft = noOfDaysLeft - annualLeaveRequest.NumberOfDaysRequested;
                noOfAnnualLeaveDaysLeft = noOfAnnualLeaveDaysLeft - annualLeaveRequest.NumberOfAnnualLeaveDaysRequested;
                noOfPublicLeaveDaysLeft = noOfPublicLeaveDaysLeft - annualLeaveRequest.NumberOfPublicLeaveDaysRequested;

                annualLeaveRequest.NumberOfDaysLeft = noOfDaysLeft;
                annualLeaveRequest.NumberOfAnnualLeaveDaysLeft = noOfAnnualLeaveDaysLeft;
                annualLeaveRequest.NumberOfPublicLeaveDaysLeft = noOfPublicLeaveDaysLeft;
            }

            return annualLeaveRequestsForYear;
        }

        public AnnualLeaveRequestOverviewModel GetRequest(int annualLeaveRequestID)
        {
            return GetAnnualLeaveRequest(annualLeaveRequestID);
        }

        public AnnualLeaveRequestOverviewModel Create(AnnualLeaveRequestOverviewModel model)
        {
            int lastAnnualLeaveRequestID = _annualLeaveRequests.Select(x => x.AnnualLeaveRequestID).LastOrDefault();

            model.AnnualLeaveRequestID = lastAnnualLeaveRequestID + 1;

            model = CalculateDaysRequested(model);

            _annualLeaveRequests.Add(model);

            return model;
        }

        public AnnualLeaveRequestOverviewModel Update(AnnualLeaveRequestOverviewModel model)
        {
            int annualLeaveRequestIndex = _annualLeaveRequests
                                                .FindIndex(x => x.AnnualLeaveRequestID == model.AnnualLeaveRequestID);

            model = CalculateDaysRequested(model);

            _annualLeaveRequests[annualLeaveRequestIndex] = model;

            return model;
        }

        public void Delete(int annualLeaveRequestId)
        {
            _annualLeaveRequests.RemoveAll(x => x.AnnualLeaveRequestID == annualLeaveRequestId);
        }

        private AnnualLeaveRequestOverviewModel CalculateDaysRequested(AnnualLeaveRequestOverviewModel model)
        {
            model.NumberOfAnnualLeaveDaysRequested = (model.ReturnDate - model.StartDate).Days;
            model.NumberOfPublicLeaveDaysRequested = 0;
            model.NumberOfDaysRequested = model.NumberOfAnnualLeaveDaysRequested + model.NumberOfPublicLeaveDaysRequested;

            return model;
        }
    }
}
