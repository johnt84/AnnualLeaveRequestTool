using AnnualLeaveRequest.Shared;

namespace AnnualLeaveRequestToolRazorPages.Models.ViewModels
{
    public class AnnualLeaveRequestDetailsViewModel
    {
        public int Year { get; set; }
        public AnnualLeaveRequestOverviewModel AnnualLeaveRequest { get; set; }
        public bool IsEditable { get; set; }
    }
}
