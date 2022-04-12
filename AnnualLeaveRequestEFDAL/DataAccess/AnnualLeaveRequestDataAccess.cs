using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestEFDAL.DataAccess.Interfaces;
using AnnualLeaveRequestEFDAL.Models;
using AutoMapper;
using System.Data;

namespace AnnualLeaveRequestEFDAL.DataAccess
{
    public class AnnualLeaveRequestDataAccess : IAnnualLeaveRequestDataAccess
    {
        private readonly IAnnualLeaveRequestEFDataAccess _annualLeaveRequestEFDataAccess;
        private readonly IMapper _mapper;

        public AnnualLeaveRequestDataAccess(IAnnualLeaveRequestEFDataAccess annualLeaveRequestEFDataAccess, IMapper mapper)
        {
            _annualLeaveRequestEFDataAccess = annualLeaveRequestEFDataAccess;
            _mapper = mapper;
        }

        public List<int> GetYears()
        {
            return _annualLeaveRequestEFDataAccess.GetYears();
        }

        public List<AnnualLeaveRequestOverviewModel> GetRequestsForYear(int year)
        {
            var annualLeaveRequestsEF = _annualLeaveRequestEFDataAccess.GetRequestsForYear(year);

            var annualLeaveRequests = _mapper.Map<List<AnnualLeaveRequestOverviewModel>>(annualLeaveRequestsEF);

            if(annualLeaveRequests == null || annualLeaveRequests.Count == 0)
            {
                return null;
            }

            decimal noOfDaysLeft = annualLeaveRequests.First().NumberOfDays;
            decimal noOfAnnualLeaveDaysLeft = annualLeaveRequests.First().NumberOfAnnualLeaveDays;
            decimal noOfPublicLeaveDaysLeft = annualLeaveRequests.First().NumberOfPublicLeaveDays;

            foreach (var annualLeaveRequest in annualLeaveRequests.OrderBy(x => x.StartDate).ToList())
            {
                noOfDaysLeft = noOfDaysLeft - annualLeaveRequest.NumberOfDaysRequested;
                noOfAnnualLeaveDaysLeft = noOfAnnualLeaveDaysLeft - annualLeaveRequest.NumberOfAnnualLeaveDaysRequested;
                noOfPublicLeaveDaysLeft = noOfPublicLeaveDaysLeft - annualLeaveRequest.NumberOfPublicLeaveDaysRequested;

                annualLeaveRequest.NumberOfDaysLeft = noOfDaysLeft;
                annualLeaveRequest.NumberOfAnnualLeaveDaysLeft = noOfAnnualLeaveDaysLeft;
                annualLeaveRequest.NumberOfPublicLeaveDaysLeft = noOfPublicLeaveDaysLeft;
            }

            return annualLeaveRequests;
        }

        public AnnualLeaveRequestOverviewModel GetRequest(int annualLeaveRequestID)
        {
            var annualLeaveRequestEF = _annualLeaveRequestEFDataAccess.GetRequest(annualLeaveRequestID);

            return _mapper.Map<AnnualLeaveRequestOverviewModel>(annualLeaveRequestEF);
        }

        public decimal GetDaysBetweenStartDateAndReturnDate(DateTime startDate, DateTime returnDate)
        {
            var emptyDate = new DateTime(2010, 01, 01);

            if (startDate < emptyDate || returnDate < emptyDate)
            {
                return 0;
            }

            return _annualLeaveRequestEFDataAccess.GetDaysBetweenStartDateAndReturnDate(startDate, returnDate); 
        }

        public AnnualLeaveRequestOverviewModel Create(AnnualLeaveRequestOverviewModel model)
        {
            var annualLeaveRequest = new Models.AnnualLeaveRequest()
            {
                AnnualLeaveRequestId = model.AnnualLeaveRequestID,
                PaidLeaveType = model.PaidLeaveType,
                LeaveType = model.LeaveType,
                StartDate = model.StartDate,
                ReturnDate = model.ReturnDate,
                NumberOfDays = model.NumberOfDays,
                NumberOfAnnualLeaveDays = model.NumberOfAnnualLeaveDays,
                NumberOfPublicLeaveDays = model.NumberOfPublicLeaveDays,  
                DateCreated = model.DateCreated,
                Notes = model.Notes,
                Year = model.Year,
            };

            var newAnnualLeaveRequest = _annualLeaveRequestEFDataAccess.Create(annualLeaveRequest);

            return _mapper.Map<AnnualLeaveRequestOverviewModel>(newAnnualLeaveRequest);
        }

        public AnnualLeaveRequestOverviewModel Update(AnnualLeaveRequestOverviewModel model)
        {
            var annualLeaveRequest = new Models.AnnualLeaveRequest()
            {
                AnnualLeaveRequestId = model.AnnualLeaveRequestID,
                PaidLeaveType = model.PaidLeaveType,
                LeaveType = model.LeaveType,
                StartDate = model.StartDate,
                ReturnDate = model.ReturnDate,
                NumberOfDays = model.NumberOfDays,
                NumberOfAnnualLeaveDays = model.NumberOfAnnualLeaveDays,
                NumberOfPublicLeaveDays = model.NumberOfPublicLeaveDays,
                DateCreated = model.DateCreated,
                Notes = model.Notes,
                Year = model.Year,
            };

            var updateAnnualLeaveRequest = _annualLeaveRequestEFDataAccess.Update(annualLeaveRequest);

            return _mapper.Map<AnnualLeaveRequestOverviewModel>(updateAnnualLeaveRequest);
        }

        public void Delete(AnnualLeaveRequestOverviewModel model)
        {
            var annualLeaveRequest = new Models.AnnualLeaveRequest()
            {
                AnnualLeaveRequestId = model.AnnualLeaveRequestID,
                PaidLeaveType = model.PaidLeaveType,
                LeaveType = model.LeaveType,
                StartDate = model.StartDate,
                ReturnDate = model.ReturnDate,
                NumberOfDays = model.NumberOfDays,
                NumberOfAnnualLeaveDays = model.NumberOfAnnualLeaveDays,
                NumberOfPublicLeaveDays = model.NumberOfPublicLeaveDays,
                DateCreated = model.DateCreated,
                Notes = model.Notes,
                Year = model.Year,
            };

            _annualLeaveRequestEFDataAccess.Delete(annualLeaveRequest);
        }
    }
}