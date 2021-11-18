using System.Collections.Generic;

namespace AnnualLeaveRequestToolMVC.Models.ViewModels
{
    public class YearsViewModel
    {
        public int SelectedYear { get; set; }
        public List<ItemList> YearsDropdownItems { get; set; }
    }
}
