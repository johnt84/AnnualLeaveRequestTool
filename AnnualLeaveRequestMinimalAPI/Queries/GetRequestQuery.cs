using MediatR;

namespace AnnualLeaveRequestMinimalAPI.Queries
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
