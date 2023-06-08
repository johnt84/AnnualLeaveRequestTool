using MediatR;
using AnnualLeaveRequest.Shared;

namespace AnnualLeaveRequestAPI.Queries
{
    public record GetRequestQuery(int year) : IRequest<AnnualLeaveRequestOverviewModel>;

    public class GetRequestQueryClass
    {
        public int AnnualLeaveRequestID { get; set; }

        public GetRequestQueryClass(int annualLeaveRequestID)
        {
            AnnualLeaveRequestID = annualLeaveRequestID;
        }
    }
}
