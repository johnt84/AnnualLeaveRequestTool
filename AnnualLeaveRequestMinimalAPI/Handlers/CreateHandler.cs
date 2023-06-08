using AnnualLeaveRequestMinimalAPI.Commands;
using MediatR;

namespace AnnualLeaveRequestMinimalAPI.Handlers
{
    public class CreateHandler : IRequestHandler<CreateCommand, AnnualLeaveRequestCRUDModel>
    {
        private readonly IAnnualLeaveRequestLogic _annualLeaveRequestLogic = null;

        public CreateHandler(IAnnualLeaveRequestLogic annualLeaveRequestLogic)
        {
            _annualLeaveRequestLogic = annualLeaveRequestLogic;
        }

        public Task<AnnualLeaveRequestCRUDModel> Handle(CreateCommand request, CancellationToken token)
        {
            return Task.FromResult(_annualLeaveRequestLogic.Create(request.annualLeaveRequestCRUDModel));
        }
    }
}
