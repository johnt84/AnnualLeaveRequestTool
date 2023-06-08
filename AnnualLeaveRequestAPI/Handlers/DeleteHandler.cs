using AnnualLeaveRequestAPI.Commands;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using AnnualLeaveRequestAPI.Interfaces;

namespace AnnualLeaveRequestAPI.Handlers
{
    public class DeleteHandler : IRequestHandler<DeleteCommand, Unit>
    {
        private readonly IAnnualLeaveRequestLogic _annualLeaveRequestLogic = null;

        public DeleteHandler(IAnnualLeaveRequestLogic annualLeaveRequestLogic)
        {
            _annualLeaveRequestLogic = annualLeaveRequestLogic;
        }

        public Task<Unit> Handle(DeleteCommand request, CancellationToken token)
        {
            _annualLeaveRequestLogic.Delete(request.annualLeaveRequestId);

            return Task.FromResult(Unit.Value);
        }
    }
}
