using AnnualLeaveRequestAPI.Commands;
using AnnualLeaveRequestAPI.Interfaces;
using AnnualLeaveRequestAPI.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AnnualLeaveRequestAPI.Handlers
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
