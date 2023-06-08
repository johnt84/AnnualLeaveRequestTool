using MediatR;

namespace AnnualLeaveRequestMinimalAPI.Commands
{
    public record DeleteCommand(int annualLeaveRequestId) : IRequest<Unit>;

    public class DeleteCommandClass
    {
        public int AnnualLeaveRequestId { get; set; }

        public DeleteCommandClass(int annualLeaveRequestId)
        {
            AnnualLeaveRequestId = annualLeaveRequestId;
        }
    }
}
