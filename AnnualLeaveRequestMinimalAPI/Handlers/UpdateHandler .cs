using AnnualLeaveRequestMinimalAPI.Commands;
using MediatR;

namespace AnnualLeaveRequestMinimalAPI.Handlers
{
    public class UpdateHandler : IRequestHandler<UpdateCommand, AnnualLeaveRequestCRUDModel>
    {
        private readonly IAnnualLeaveRequestLogic _annualLeaveRequestLogic = null;

        public UpdateHandler(IAnnualLeaveRequestLogic annualLeaveRequestLogic)
        {
            _annualLeaveRequestLogic = annualLeaveRequestLogic;
        }

        public Task<AnnualLeaveRequestCRUDModel> Handle(UpdateCommand request, CancellationToken token)
        {
            return Task.FromResult(_annualLeaveRequestLogic.Update(request.annualLeaveRequestCRUDModel));
        }
    }
}
