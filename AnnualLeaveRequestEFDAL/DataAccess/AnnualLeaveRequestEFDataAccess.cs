using AnnualLeaveRequestEFDAL.DataAccess.Interfaces;
using AnnualLeaveRequestEFDAL.Models;

namespace AnnualLeaveRequestEFDAL.DataAccess
{
    public class AnnualLeaveRequestEFDataAccess : IAnnualLeaveRequestEFDataAccess
    {
        private readonly AnnualLeaveDbContext _db;
        private readonly IAnnualLeaveRequestYearEFDataAccess _annualLeaveRequestYearEFDataAccess;

        public AnnualLeaveRequestEFDataAccess(AnnualLeaveDbContext db, IAnnualLeaveRequestYearEFDataAccess annualLeaveRequestYearEFDataAccess)
        {
            _db = db;
            _annualLeaveRequestYearEFDataAccess = annualLeaveRequestYearEFDataAccess;
        }

        public List<int> GetYears()
        {
            return _db.AnnualLeaveYears
                    .Select(x => x.Year)
                    .OrderBy(x => x)
                    .ToList();
        }

        public List<AnnualLeaveRequestsOverview> GetRequestsForYear(int year)
        {
            return _db.AnnualLeaveRequestsOverviews
                        .Where(x => x.Year == year)
                        .OrderBy(x => x.StartDate)
                        .ToList();
        }

        public AnnualLeaveRequestsOverview? GetRequest(int annualLeaveRequestID)
        {
            return _db.AnnualLeaveRequestsOverviews
                        .Where(x => x.AnnualLeaveRequestId == annualLeaveRequestID)
                        .FirstOrDefault();
        }

        public List<AnnualLeaveRequestsBetweenTwoDates> GetAnnualLeaveRequestsBetweenTwoDatesGet(DateTime startDate, DateTime returnDate)
        {
            return _db
                    .GetAnnualLeaveRequestsBetweenTwoDates(startDate, returnDate)
                    .ToList();
        }

        public decimal GetDaysBetweenStartDateAndReturnDate(DateTime startDate, DateTime returnDate)
        {
            var annualLeaveRequestsBetweenTwoDates = GetAnnualLeaveRequestsBetweenTwoDatesGet(startDate, returnDate);

            if (annualLeaveRequestsBetweenTwoDates.Count > 0)
            {
                return annualLeaveRequestsBetweenTwoDates.First().NumberOfDays;
            }

            return 0;
        }

        public List<AnnualLeaveRequestsOverview> Create(Models.AnnualLeaveRequest model)
        {
            var annualLeaveRequestsBetweenTwoDates = GetAnnualLeaveRequestsBetweenTwoDatesGet(model.StartDate, model.ReturnDate);

            if (annualLeaveRequestsBetweenTwoDates.Count == 0)
            {
                return null;
            }

             var annualLeaveRequestsOverviews = new List<AnnualLeaveRequestsOverview>();

            foreach (var annualLeaveRequestsBetweenTwoDate in annualLeaveRequestsBetweenTwoDates)
            {
                var annualLeaveYear = _annualLeaveRequestYearEFDataAccess.GetAnnualLeaveYear(annualLeaveRequestsBetweenTwoDate.Year);

                annualLeaveYear.NumberOfDaysLeft = annualLeaveRequestsBetweenTwoDate.NumberOfDaysLeftAfterRequest;
                annualLeaveYear.NumberOfAnnualLeaveDaysLeft = annualLeaveRequestsBetweenTwoDate.NumberOfAnnualLeaveDaysLeftAfterRequest;
                annualLeaveYear.NumberOfPublicLeaveDaysLeft = annualLeaveRequestsBetweenTwoDate.NumberOfPublicLeaveDaysLeftAfterRequest;

                var updatedAnnualLeaveRequestYear = _annualLeaveRequestYearEFDataAccess.Update(annualLeaveYear);

                model.Year = annualLeaveRequestsBetweenTwoDate.Year;
                model.NumberOfDays = annualLeaveRequestsBetweenTwoDate.NumberOfDays;
                model.NumberOfAnnualLeaveDays = annualLeaveRequestsBetweenTwoDate.NumberOfAnnualLeaveDays;
                model.NumberOfPublicLeaveDays = annualLeaveRequestsBetweenTwoDate.NumberOfPublicLeaveDays;

                _db.AnnualLeaveRequests.Add(model);

                _db.SaveChanges();

                int newAnnualLeaveRequestId = model?.AnnualLeaveRequestId ?? 0;

                var newAnnualLeaveRequest = GetRequest(newAnnualLeaveRequestId);

                annualLeaveRequestsOverviews.Add(newAnnualLeaveRequest);
            }

            return annualLeaveRequestsOverviews;
        }

        public List<AnnualLeaveRequestsOverview> Update(Models.AnnualLeaveRequest model)
        {
            var annualLeaveRequestsBetweenTwoDates = GetAnnualLeaveRequestsBetweenTwoDatesGet(model.StartDate, model.ReturnDate);

            if (annualLeaveRequestsBetweenTwoDates.Count == 0)
            {
                return null;
            }

            var annualLeaveRequestsOverviews = new List<AnnualLeaveRequestsOverview>();

            foreach (var annualLeaveRequestsBetweenTwoDate in annualLeaveRequestsBetweenTwoDates)
            {
                var annualLeaveYear = _annualLeaveRequestYearEFDataAccess.GetAnnualLeaveYear(annualLeaveRequestsBetweenTwoDate.Year);

                var existingAnnualLeaveRequestBeforeChange = GetRequest(model.AnnualLeaveRequestId);

                decimal numberOfDaysLeftAfterChange = annualLeaveRequestsBetweenTwoDate.NumberOfDaysLeft - (model.NumberOfDays - existingAnnualLeaveRequestBeforeChange.NumberOfDays);
                decimal numberOfAnnualLeaveDaysLeftAfterChange = annualLeaveRequestsBetweenTwoDate.NumberOfAnnualLeaveDaysLeft - (model.NumberOfAnnualLeaveDays - existingAnnualLeaveRequestBeforeChange.NumberOfAnnualLeaveDays);
                decimal numberOfPublicDaysLeftAfterChange = annualLeaveRequestsBetweenTwoDate.NumberOfPublicLeaveDaysLeft - (model.NumberOfPublicLeaveDays - existingAnnualLeaveRequestBeforeChange.NumberOfPublicLeaveDays);

                annualLeaveYear.NumberOfDaysLeft = numberOfDaysLeftAfterChange;
                annualLeaveYear.NumberOfAnnualLeaveDaysLeft = numberOfAnnualLeaveDaysLeftAfterChange;
                annualLeaveYear.NumberOfPublicLeaveDaysLeft = numberOfPublicDaysLeftAfterChange;

                var updatedAnnualLeaveRequestYear = _annualLeaveRequestYearEFDataAccess.Update(annualLeaveYear);

                model.Year = annualLeaveRequestsBetweenTwoDate.Year;
                model.NumberOfDays = annualLeaveRequestsBetweenTwoDate.NumberOfDays;
                model.NumberOfAnnualLeaveDays = annualLeaveRequestsBetweenTwoDate.NumberOfAnnualLeaveDays;
                model.NumberOfPublicLeaveDays = annualLeaveRequestsBetweenTwoDate.NumberOfPublicLeaveDays;

                _db.AnnualLeaveRequests.Update(model);

                _db.SaveChanges();

                int updateAnnualLeaveRequestId = model?.AnnualLeaveRequestId ?? 0;

                var updateAnnualLeaveRequest = GetRequest(updateAnnualLeaveRequestId);

                annualLeaveRequestsOverviews.Add(updateAnnualLeaveRequest);
            }

            return annualLeaveRequestsOverviews;
        }

        public void Delete(Models.AnnualLeaveRequest model)
        {
            var annualLeaveRequestsBetweenTwoDates = GetAnnualLeaveRequestsBetweenTwoDatesGet(model.StartDate, model.ReturnDate);

            if (annualLeaveRequestsBetweenTwoDates.Count == 0)
            {
                return;
            }

            var annualLeaveRequestsOverviews = new List<AnnualLeaveRequestsOverview>();

            foreach (var annualLeaveRequestsBetweenTwoDate in annualLeaveRequestsBetweenTwoDates)
            {
                model.Year = annualLeaveRequestsBetweenTwoDate.Year;

                _db.AnnualLeaveRequests.Remove(model);

                _db.SaveChanges();

                var annualLeaveYear = _annualLeaveRequestYearEFDataAccess.GetAnnualLeaveYear(model.Year);

                annualLeaveYear.NumberOfDaysLeft = annualLeaveYear.NumberOfDaysLeft + annualLeaveRequestsBetweenTwoDate.NumberOfDays;
                annualLeaveYear.NumberOfAnnualLeaveDaysLeft = annualLeaveYear.NumberOfAnnualLeaveDaysLeft + annualLeaveRequestsBetweenTwoDate.NumberOfAnnualLeaveDays;
                annualLeaveYear.NumberOfPublicLeaveDaysLeft = annualLeaveYear.NumberOfPublicLeaveDaysLeft + annualLeaveRequestsBetweenTwoDate.NumberOfPublicLeaveDays;

                var updatedAnnualLeaveRequestYear = _annualLeaveRequestYearEFDataAccess.Update(annualLeaveYear);

                int updateAnnualLeaveRequestId = model?.AnnualLeaveRequestId ?? 0;

                var updateAnnualLeaveRequest = GetRequest(updateAnnualLeaveRequestId);

                annualLeaveRequestsOverviews.Add(updateAnnualLeaveRequest);
            }
        }
    }
}
