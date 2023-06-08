using AnnualLeaveRequestMinimalAPI.Queries;
using MediatR;

namespace AnnualLeaveRequestMinimalAPI.Handlers
{
    public class GetYearsHandler : IRequestHandler<GetYearsQuery, List<int>>
    {
        private readonly IAnnualLeaveRequestLogic _annualLeaveRequestLogic = null;

        public GetYearsHandler(IAnnualLeaveRequestLogic annualLeaveRequestLogic)
        {
            _annualLeaveRequestLogic = annualLeaveRequestLogic;
        }

        public Task<List<int>> Handle(GetYearsQuery request, CancellationToken token)
        {
            return Task.FromResult(_annualLeaveRequestLogic.GetYears());
        }
    }
}
