using AnnualLeaveRequestToolRazorPages.Interfaces;
using AnnualLeaveRequestToolRazorPages.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace AnnualLeaveRequestToolRazorPages.Pages.AnnualLeaveRequest
{
    public class OverviewModel : PageModel
    {
        public AnnualLeaveRequestOverviewViewModel AnnualLeaveRequestOverviewViewModel;

        [BindProperty(SupportsGet = true)]
        public int ID { get; set; } = DateTime.UtcNow.Year;

        private readonly IAnnualLeaveRequestLogic _annualLeaveRequestLogic;

        public OverviewModel(IAnnualLeaveRequestLogic annualLeaveRequestLogic)
        {
            _annualLeaveRequestLogic = annualLeaveRequestLogic;
        }

        public void OnGet()
        {
            AnnualLeaveRequestOverviewViewModel = _annualLeaveRequestLogic.GetRequestsForYear(ID);
        }
        public void OnPost()
        {
            AnnualLeaveRequestOverviewViewModel = _annualLeaveRequestLogic.GetRequestsForYear(ID);
        }
    }
}
