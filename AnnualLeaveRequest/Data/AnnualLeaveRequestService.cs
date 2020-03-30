using AnnualLeaveRequest.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AnnualLeaveRequest.Data
{
    public class AnnualLeaveRequestService : IAnnualLeaveRequestService
    {
        private readonly SqlConnectionConfiguration _configuration;

        private IDbConnection Connection => new SqlConnection(_configuration.Value);

        public AnnualLeaveRequestService(SqlConnectionConfiguration configuration)
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
                                                "Select t.Year from AnnualLeaveRequestsOverview t"
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
                                                "Select * from AnnualLeaveRequestsOverview t where t.annualLeaveRequestID = @annualLeaveRequestID",
                                                new { annualLeaveRequestID }).
                                            FirstOrDefault();


                return annualLeaveRequest;
            }
        }

        public AnnualLeaveRequestOverviewModel Create(AnnualLeaveRequestOverviewModel model)
        {
            using (IDbConnection connection = Connection)
            {
                string query = @"
                                exec procCreateAnnualLeaveRequest 
                                        @PaidLeaveType, 
                                        @LeaveType, 
                                        @StartDate, 
                                        @ReturnDate, 
                                        @NumberOfDaysRequested, 
                                        @NumberOfAnnualLeaveDaysRequested, 
                                        @NumberOfPublicLeaveDaysRequested, 
                                        @DateCreated, 
                                        @Notes";

                model.AnnualLeaveRequestID = connection.
                                                Query<int>(query, model).
                                                Single();
            }

            return model;
        }

        public AnnualLeaveRequestOverviewModel Update(AnnualLeaveRequestOverviewModel model)
        {
            using (IDbConnection connection = Connection)
            {
                string query = @"
                                exec procUpdateAnnualLeaveRequest 
                                        @AnnualLeaveRequestID,                 
                                        @PaidLeaveType, 
                                        @LeaveType, 
                                        @StartDate, 
                                        @ReturnDate, 
                                        @NumberOfDaysRequested, 
                                        @NumberOfAnnualLeaveDaysRequested, 
                                        @NumberOfPublicLeaveDaysRequested, 
                                        @Notes";

                connection.Query<int>(query, model);
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
