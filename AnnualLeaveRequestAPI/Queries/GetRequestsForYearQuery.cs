using AnnualLeaveRequest.Shared;
using MediatR;
using System.Collections.Generic;

namespace AnnualLeaveRequestAPI.Queries
{
    public record GetRequestsForYearQuery(int year) : IRequest<List<AnnualLeaveRequestOverviewModel>>;
    
    public class GetRequestsForYearQueryClass
    {
        public int Year { get; set; }

        public GetRequestsForYearQueryClass(int year)
        {
            Year = year;
        }    
    }
}
