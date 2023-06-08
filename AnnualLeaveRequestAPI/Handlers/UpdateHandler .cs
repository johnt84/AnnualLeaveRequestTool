using AnnualLeaveRequestAPI.Commands;
using AnnualLeaveRequestAPI.Interfaces;
using AnnualLeaveRequestAPI.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AnnualLeaveRequestAPI.Handlers
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
