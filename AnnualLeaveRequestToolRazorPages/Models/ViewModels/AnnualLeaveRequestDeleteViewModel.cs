using AnnualLeaveRequest.Shared;

namespace AnnualLeaveRequestToolRazorPages.Models.ViewModels
{
    public class AnnualLeaveRequestDeleteViewModel
    {
        public int Year { get; set; }
        public int AnnualLeaveRequestID { get; set; }
        public AnnualLeaveRequestOverviewModel AnnualLeaveRequest { get; set; }
        public bool IsDeletable { get; set; }
    }
}
