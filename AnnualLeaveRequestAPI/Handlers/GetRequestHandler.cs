using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestAPI.Interfaces;
using AnnualLeaveRequestAPI.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AnnualLeaveRequestAPI.Handlers
{
    public class GetRequestHandler : IRequestHandler<GetRequestQuery, AnnualLeaveRequestOverviewModel>
    {
        private readonly IAnnualLeaveRequestLogic _annualLeaveRequestLogic = null;

        public GetRequestHandler(IAnnualLeaveRequestLogic annualLeaveRequestLogic)
        {
            _annualLeaveRequestLogic = annualLeaveRequestLogic;
        }

        public Task<AnnualLeaveRequestOverviewModel> Handle(GetRequestQuery request, CancellationToken token)
        {
            return Task.FromResult(_annualLeaveRequestLogic.GetRequest(request.year));
        }
    }
}
