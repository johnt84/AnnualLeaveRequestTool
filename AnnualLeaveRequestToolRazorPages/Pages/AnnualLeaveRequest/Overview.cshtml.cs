using AnnualLeaveRequestToolRazorPages.Interfaces;
using AnnualLeaveRequestToolRazorPages.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnnualLeaveRequestToolRazorPages.Pages.AnnualLeaveRequest
{
    public class OverviewModel : PageModel
    {
        public AnnualLeaveRequestOverviewViewModel AnnualLeaveRequestOverviewModel;

        private readonly IAnnualLeaveRequestLogic _annualLeaveRequestLogic;

        public OverviewModel(IAnnualLeaveRequestLogic annualLeaveRequestLogic)
        {
            _annualLeaveRequestLogic = annualLeaveRequestLogic;
        }

        public void OnGet()
        {
            AnnualLeaveRequestOverviewModel = _annualLeaveRequestLogic.GetRequestsForYear(2021);
        }
    }
}
