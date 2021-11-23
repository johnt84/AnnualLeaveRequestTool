using System.Collections.Generic;

namespace AnnualLeaveRequestToolMVC.Models.ViewModels
{
    public class AnnualLeaveRequestOverviewViewModel
    {
        public int SelectedYear { get; set; }
        public List<AnnualLeaveRequestOverviewModel> AnnualLeaveRequestsForYear { get; set; }
        public List<int> Years { get; set; }
        public AnnualLeaveRequestOverviewModel AnnualLeaveRequestOverviewForYear { get; set; }
        public bool EditableYearSelected { get; set; }
    }
}
