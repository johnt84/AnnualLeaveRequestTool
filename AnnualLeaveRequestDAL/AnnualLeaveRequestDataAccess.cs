using AnnualLeaveRequest.Shared;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AnnualLeaveRequestDAL
{
    public class AnnualLeaveRequestDataAccess : IAnnualLeaveRequestDataAccess
    {
        private readonly SqlConnectionConfiguration _configuration;

        private IDbConnection Connection => new SqlConnection(_configuration.Value);

        public AnnualLeaveRequestDataAccess(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<int> GetYears()
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();
                var years = connection.
                                    Query<int>(
                                        "Select t.Year from AnnualLeaveRequestsOverview t group by t.Year"
                                        ).
                                    OrderBy(x => x).
                                    ToList();


                return years;
            }
        }

        public List<AnnualLeaveRequestOverviewModel> GetRequestForYear(int year)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();

                var annualLeaveRequests = connection.
                                            Query<AnnualLeaveRequestOverviewModel>(
                                                "Select * from AnnualLeaveRequestsOverview t where t.Year = @year",
                                                new { year }).
                                            OrderBy(x => x.StartDate).
                                            ToList();

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
        }

        public AnnualLeaveRequestOverviewModel GetRequest(int annualLeaveRequestID)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();
                var annualLeaveRequest = connection.
                                            Query<AnnualLeaveRequestOverviewModel>(
                                                @"Select * 
                                                    from AnnualLeaveRequestsOverview t 
                                                    where t.annualLeaveRequestID = @annualLeaveRequestID",
                                                new { annualLeaveRequestID }).
                                            FirstOrDefault();


                return annualLeaveRequest;
            }
        }

        public decimal GetDaysBetweenStartDateAndReturnDate(DateTime startDate, DateTime returnDate)
        {
            var emptyDate = new DateTime(2010, 01, 01);

            if (startDate < emptyDate || returnDate < emptyDate)
            {
                return 0;
            }

            using (IDbConnection connection = Connection)
            {
                connection.Open();
                return connection.
                        Query<decimal>(
                            @"select iif(sum(t.numberOfDays) > 0, sum(t.numberOfDays), 0)
                                from numberOfAnnualLeaveDaysBetweenTwoDatesGet(@startDate, @returnDate) t",
                            new { startDate, returnDate }).
                        FirstOrDefault();
            }
        }

        public AnnualLeaveRequestOverviewModel Create(AnnualLeaveRequestOverviewModel model)
        {
            using (IDbConnection connection = Connection)
            {
                string query = @"exec procCreateAnnualLeaveRequest 
                                        @PaidLeaveType, 
                                        @LeaveType, 
                                        @StartDate, 
                                        @ReturnDate, 
                                        @DateCreated, 
                                        @Notes";

                try
                {
                    model.AnnualLeaveRequestID = connection.
                                                    Query<int>(query, model).
                                                    Single();

                    model.ErrorMessage = string.Empty;
                }
                catch (Exception ex)
                {
                    model.ErrorMessage = ex.Message;
                }
            }

            return model;
        }

        public AnnualLeaveRequestOverviewModel Update(AnnualLeaveRequestOverviewModel model)
        {
            using (IDbConnection connection = Connection)
            {
                string query = @"exec procUpdateAnnualLeaveRequest 
                                        @AnnualLeaveRequestID,                 
                                        @PaidLeaveType, 
                                        @LeaveType, 
                                        @StartDate, 
                                        @ReturnDate,  
                                        @Notes";

                try
                {
                    connection.Query<int>(query, model);

                    model.ErrorMessage = string.Empty;
                }
                catch (Exception ex)
                {
                    model.ErrorMessage = ex.Message;
                }
            }

            return model;
        }

        public void Delete(AnnualLeaveRequestOverviewModel model)
        {
            using (IDbConnection connection = Connection)
            {
                string query = "exec procDeleteAnnualLeaveRequest @AnnualLeaveRequestID";

                connection.Query<int>(query, model);
            }
        }
    }
}
