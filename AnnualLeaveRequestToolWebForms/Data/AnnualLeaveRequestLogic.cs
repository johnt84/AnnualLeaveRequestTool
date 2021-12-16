using AnnualLeaveRequestToolWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnnualLeaveRequestToolWebForms.Data
{
    public class AnnualLeaveRequestLogic : IAnnualLeaveRequestLogic
    {
        private static List<AnnualLeaveRequestOverviewModel> _annualLeaveRequests = new List<AnnualLeaveRequestOverviewModel>()
        {
            new AnnualLeaveRequestOverviewModel()
            {
                AnnualLeaveRequestID = 1,
                Year = 2020,
                StartDate = new DateTime(2020,12,21),
                ReturnDate = new DateTime(2020,12,31),
                DateCreated = DateTime.UtcNow,
                Notes = "Xmas and New Year",
                NumberOfDays = 28,
                NumberOfAnnualLeaveDays = 25,
                NumberOfPublicLeaveDays = 3,
                NumberOfDaysRequested = 8,
                NumberOfAnnualLeaveDaysRequested = 6,
                NumberOfPublicLeaveDaysRequested = 2,
                NumberOfDaysLeft = 19,
                NumberOfAnnualLeaveDaysLeft = 18,
                NumberOfPublicLeaveDaysLeft = 1,
            },
            new AnnualLeaveRequestOverviewModel()
            {
                AnnualLeaveRequestID = 2,
                Year = 2021,
                StartDate = new DateTime(2021,12,15),
                ReturnDate = new DateTime(2021,12,16),
                DateCreated = DateTime.UtcNow,
                Notes = "Test1",
                NumberOfDays = 28,
                NumberOfAnnualLeaveDays = 25,
                NumberOfPublicLeaveDays = 3,
                NumberOfDaysRequested = 1,
                NumberOfAnnualLeaveDaysRequested = 1,
                NumberOfPublicLeaveDaysRequested = 0,
                NumberOfDaysLeft = 27,
                NumberOfAnnualLeaveDaysLeft = 24,
                NumberOfPublicLeaveDaysLeft= 3,
            },
            new AnnualLeaveRequestOverviewModel()
            {
                AnnualLeaveRequestID = 3,
                Year = 2021,
                StartDate = new DateTime(2021,12,20),
                ReturnDate = new DateTime(2022,01,06),
                DateCreated = DateTime.UtcNow,
                Notes = "Xmas and New Year",
                NumberOfDays = 28,
                NumberOfAnnualLeaveDays = 25,
                NumberOfPublicLeaveDays = 3,
                NumberOfDaysRequested = 8,
                NumberOfAnnualLeaveDaysRequested = 6,
                NumberOfPublicLeaveDaysRequested = 2,
                NumberOfDaysLeft = 19,
                NumberOfAnnualLeaveDaysLeft = 18,
                NumberOfPublicLeaveDaysLeft = 1,
            },
            new AnnualLeaveRequestOverviewModel()
            {
                AnnualLeaveRequestID = 4,
                Year = 2022,
                StartDate = new DateTime(2022,02,09),
                ReturnDate = new DateTime(2022,02,10),
                DateCreated = DateTime.UtcNow,
                Notes = "My birthday",
                NumberOfDays = 28,
                NumberOfAnnualLeaveDays = 25,
                NumberOfPublicLeaveDays = 3,
                NumberOfDaysRequested = 2,
                NumberOfAnnualLeaveDaysRequested = 2,
                NumberOfPublicLeaveDaysRequested = 0,
                NumberOfDaysLeft = 26,
                NumberOfAnnualLeaveDaysLeft = 3,
                NumberOfPublicLeaveDaysLeft = 22,
            },
        };

        public AnnualLeaveRequestLogic()
        {
        }

        public List<int> GetYears()
        {
            return _annualLeaveRequests
                    .GroupBy(x => x.Year)
                    .Select(x => x.Key)
                    .OrderBy(x => x)
                    .ToList();
        }

        public List<AnnualLeaveRequestOverviewModel> GetRequestsForYear(int year)
        {
            return _annualLeaveRequests
                        .Where(x => x.Year == year)
                        .OrderBy(x => x.StartDate)
                        .ToList();   
        }

        public AnnualLeaveRequestOverviewModel GetRequest(int annualLeaveRequestID)
        {
            return _annualLeaveRequests
                        .Where(x => x.AnnualLeaveRequestID == annualLeaveRequestID)
                        .FirstOrDefault();
        }

        public decimal GetDaysBetweenStartDateAndReturnDate(DateTime startDate, DateTime returnDate)
        {
            return (decimal)(returnDate - startDate).TotalDays;
        }

        public AnnualLeaveRequestOverviewModel Create(AnnualLeaveRequestOverviewModel model)
        {
           _annualLeaveRequests.Add(model);

            return GetRequest(model.AnnualLeaveRequestID);
        }

        public AnnualLeaveRequestOverviewModel Update(AnnualLeaveRequestOverviewModel model)
        {
            var annualLeaveRequest = GetRequest(model.AnnualLeaveRequestID);
            var _annualLeaveRequestsArray = _annualLeaveRequests.ToArray();

            int position = Array.IndexOf(_annualLeaveRequestsArray, annualLeaveRequest);
            _annualLeaveRequestsArray[position] = model;

            return GetRequest(model.AnnualLeaveRequestID);
        }

        public void Delete(AnnualLeaveRequestOverviewModel model)
        {
            _annualLeaveRequests.Remove(model);
        }
    }
}
