using MediatR;

namespace AnnualLeaveRequestMinimalAPI.Commands
{
    public record CreateCommand(AnnualLeaveRequestCRUDModel annualLeaveRequestCRUDModel) : IRequest<AnnualLeaveRequestCRUDModel>;

    public class CreateCommandClass
    {
        public AnnualLeaveRequestCRUDModel AnnualLeaveRequestCRUDModel { get; set; }

        public CreateCommandClass(AnnualLeaveRequestCRUDModel annualLeaveRequestCRUDModel)
        {
            AnnualLeaveRequestCRUDModel = annualLeaveRequestCRUDModel;
        }
    }
}
