using AnnualLeaveRequestEFDAL.DataAccess.Interfaces;
using AnnualLeaveRequestEFDAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        public async Task<List<AnnualLeaveRequestsOverview>> GetRequestsForYearAsync(int year, CancellationToken cancellationToken)
        {
            return await _db.AnnualLeaveRequestsOverviews
                        .Where(x => x.Year == year)
                        .OrderBy(x => x.StartDate)
                        .ToListAsync(cancellationToken);
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

        public AnnualLeaveRequestsOverview Create(Models.AnnualLeaveRequest model)
        {
            var paidLeaveTypeParam = new SqlParameter()
            {
                ParameterName = "@PaidLeaveType",
                Direction = ParameterDirection.Input,
                DbType = DbType.String,
                Value = model.PaidLeaveType,
            };

            var leaveTypeParam = new SqlParameter()
            {
                ParameterName = "@LeaveType",
                Direction = ParameterDirection.Input,
                DbType = DbType.String,
                Value = model.LeaveType,
            };

            var startDateParam = new SqlParameter()
            {
                ParameterName = "@StartDate",
                Direction = ParameterDirection.Input,
                DbType = DbType.DateTime,
                Value = model.StartDate,
            };

            var returnDateParam = new SqlParameter()
            {
                ParameterName = "@ReturnDate",
                Direction = ParameterDirection.Input,
                DbType = DbType.DateTime,
                Value = model.ReturnDate,
            };

            var dateCreatedParam = new SqlParameter()
            {
                ParameterName = "@DateCreated",
                Direction = ParameterDirection.Input,
                DbType = DbType.DateTime,
                Value = model.DateCreated,
            };

            var notesParam = new SqlParameter()
            {
                ParameterName = "@Notes",
                Direction = ParameterDirection.Input,
                DbType = DbType.String,
                IsNullable = true,
                Value = !string.IsNullOrWhiteSpace(model.Notes) ? model.Notes : DBNull.Value,
            };

            var newAnnualLeaveRequestIdParam = new SqlParameter()
            {
                ParameterName = "@Id",
                DbType = DbType.Int32,
                Direction = ParameterDirection.Output,
            };

            string query = @"exec procCreateAnnualLeaveRequest 
                                    @PaidLeaveType,
                                    @LeaveType,
                                    @StartDate,
                                    @ReturnDate,
                                    @DateCreated,
                                    @Notes,
                                    @Id out";

            try
            {
                _db.Database.ExecuteSqlRaw(query,
                        paidLeaveTypeParam,
                        leaveTypeParam,
                        startDateParam,
                        returnDateParam,
                        dateCreatedParam,
                        notesParam,
                        newAnnualLeaveRequestIdParam);
            }
            catch (Exception ex)
            {
                return new AnnualLeaveRequestsOverview()
                {
                    PaidLeaveType = model.PaidLeaveType,
                    LeaveType = model.LeaveType,
                    StartDate = model.StartDate,
                    ReturnDate = model.ReturnDate,
                    DateCreated = model.DateCreated,
                    Notes = model.Notes,
                    ErrorMessage = ex.Message,
                };
            }

            return GetRequest(Convert.ToInt32(newAnnualLeaveRequestIdParam.Value));
        }

        public AnnualLeaveRequestsOverview Update(Models.AnnualLeaveRequest model)
        {
            var annualLeaveRequestIdParam = new SqlParameter()
            {
                ParameterName = "@AnnualLeaveRequestID",
                Direction = ParameterDirection.Input,
                DbType = DbType.Int32,
                Value = model.AnnualLeaveRequestId,
            };

            var paidLeaveTypeParam = new SqlParameter()
            {
                ParameterName = "@PaidLeaveType",
                Direction = ParameterDirection.Input,
                DbType = DbType.String,
                Value = model.PaidLeaveType,
            };

            var leaveTypeParam = new SqlParameter()
            {
                ParameterName = "@LeaveType",
                Direction = ParameterDirection.Input,
                DbType = DbType.String,
                Value = model.LeaveType,
            };

            var startDateParam = new SqlParameter()
            {
                ParameterName = "@StartDate",
                Direction = ParameterDirection.Input,
                DbType = DbType.DateTime,
                Value = model.StartDate,
            };

            var returnDateParam = new SqlParameter()
            {
                ParameterName = "@ReturnDate",
                Direction = ParameterDirection.Input,
                DbType = DbType.DateTime,
                Value = model.ReturnDate,
            };

            var dateCreatedParam = new SqlParameter()
            {
                ParameterName = "@DateCreated",
                Direction = ParameterDirection.Input,
                DbType = DbType.DateTime,
                Value = model.DateCreated,
            };

            var notesParam = new SqlParameter()
            {
                ParameterName = "@Notes",
                Direction = ParameterDirection.Input,
                DbType = DbType.String,
                IsNullable = true,
                Value = !string.IsNullOrWhiteSpace(model.Notes) ? model.Notes : DBNull.Value,
            };

            string query = @"exec procUpdateAnnualLeaveRequest 
                                        @AnnualLeaveRequestID,                 
                                        @PaidLeaveType, 
                                        @LeaveType, 
                                        @StartDate, 
                                        @ReturnDate,  
                                        @Notes";

            try
            {
                _db.Database.ExecuteSqlRaw(query,
                        annualLeaveRequestIdParam,
                        paidLeaveTypeParam,
                        leaveTypeParam,
                        startDateParam,
                        returnDateParam,
                        dateCreatedParam,
                        notesParam); 
            }
            catch (Exception ex)
            {
                return new AnnualLeaveRequestsOverview()
                {
                    AnnualLeaveRequestId = model.AnnualLeaveRequestId,
                    PaidLeaveType = model.PaidLeaveType,
                    LeaveType = model.LeaveType,
                    StartDate = model.StartDate,
                    ReturnDate = model.ReturnDate,
                    DateCreated = model.DateCreated,
                    Notes = model.Notes,
                    Year = model.Year,
                    ErrorMessage = ex.Message,
                };
            }

            return GetRequest(model.AnnualLeaveRequestId);
        }

        public void Delete(Models.AnnualLeaveRequest model)
        {
            var annualLeaveRequestIdParam = new SqlParameter()
            {
                ParameterName = "@AnnualLeaveRequestID",
                Direction = ParameterDirection.Input,
                DbType = DbType.Int32,
                Value = model.AnnualLeaveRequestId,
            };

            string query = @"exec procDeleteAnnualLeaveRequest @AnnualLeaveRequestID";

            try
            {
                _db.Database.ExecuteSqlRaw(query,
                        annualLeaveRequestIdParam);
            }
            catch (Exception)
            {

            }
        }
    }
}
