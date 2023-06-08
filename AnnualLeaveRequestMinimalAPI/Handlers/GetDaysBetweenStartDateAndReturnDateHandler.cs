using AnnualLeaveRequestMinimalAPI.Queries;
using MediatR;

namespace AnnualLeaveRequestMinimalAPI.Handlers
{
    public class GetDaysBetweenStartDateAndReturnDateHandler : IRequestHandler<GetDaysBetweenStartDateAndReturnDateQuery, decimal>
    {
        private readonly IAnnualLeaveRequestLogic _annualLeaveRequestLogic = null;

        public GetDaysBetweenStartDateAndReturnDateHandler(IAnnualLeaveRequestLogic annualLeaveRequestLogic)
        {
            _annualLeaveRequestLogic = annualLeaveRequestLogic;
        }

        public Task<decimal> Handle(GetDaysBetweenStartDateAndReturnDateQuery request, CancellationToken token)
        {
            return Task.FromResult(_annualLeaveRequestLogic.GetDaysBetweenStartDateAndReturnDate(request.startDate, request.returnDate));
        }
    }
}
