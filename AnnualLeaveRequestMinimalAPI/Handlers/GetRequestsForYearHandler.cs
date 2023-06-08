using AnnualLeaveRequestMinimalAPI.Queries;
using MediatR;

namespace AnnualLeaveRequestAPI.Handlers
{
    public class GetRequestsForYearHandler : IRequestHandler<GetRequestsForYearQuery, List<AnnualLeaveRequestOverviewModel>>
    {
        private readonly IAnnualLeaveRequestLogic _annualLeaveRequestLogic = null;

        public GetRequestsForYearHandler(IAnnualLeaveRequestLogic annualLeaveRequestLogic)
        {
            _annualLeaveRequestLogic = annualLeaveRequestLogic;
        }

        public Task<List<AnnualLeaveRequestOverviewModel>> Handle(GetRequestsForYearQuery request, CancellationToken token)
        {
            return Task.FromResult(_annualLeaveRequestLogic.GetRequestsForYear(request.year));
        }
    }
}
