using AnnualLeaveRequest.Shared;

namespace AnnualLeaveRequestToolMVC.Models.ViewModels
{
    public class AnnualLeaveRequestDeleteViewModel
    {
        public int Year { get; set; }
        public AnnualLeaveRequestOverviewModel AnnualLeaveRequest { get; set; }
        public bool IsDeletable { get; set; }
    }
}
