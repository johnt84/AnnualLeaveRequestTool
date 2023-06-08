using AnnualLeaveRequestAPI.Models;
using MediatR;

namespace AnnualLeaveRequestAPI.Commands
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
