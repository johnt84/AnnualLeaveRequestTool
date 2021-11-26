using AnnualLeaveRequest.Shared;

namespace AnnualLeaveRequestToolMVC.Models.ViewModels
{
    public class AnnualLeaveRequestDetailsViewModel
    {
        public AnnualLeaveRequestOverviewModel AnnualLeaveRequest { get; set; }
        public bool IsEditable { get; set; }
    }
}
