using MediatR;

namespace AnnualLeaveRequestMinimalAPI.Commands
{
    public record UpdateCommand(AnnualLeaveRequestCRUDModel annualLeaveRequestCRUDModel) : IRequest<AnnualLeaveRequestCRUDModel>;

    public class UpdateCommandClass
    {
        public AnnualLeaveRequestCRUDModel AnnualLeaveRequestCRUDModel { get; set; }

        public UpdateCommandClass(AnnualLeaveRequestCRUDModel annualLeaveRequestCRUDModel)
        {
            AnnualLeaveRequestCRUDModel = annualLeaveRequestCRUDModel;
        }
    }
}
