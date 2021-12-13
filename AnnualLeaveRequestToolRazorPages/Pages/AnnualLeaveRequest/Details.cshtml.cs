using AnnualLeaveRequestToolRazorPages.Interfaces;
using AnnualLeaveRequestToolRazorPages.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace AnnualLeaveRequestToolRazorPages.Pages.AnnualLeaveRequest
{
    public class DetailsModel : PageModel
    {
        public AnnualLeaveRequestDetailsViewModel AnnualLeaveRequestDetailsViewModel;

        private readonly IAnnualLeaveRequestLogic _annualLeaveRequestLogic;

        public DetailsModel(IAnnualLeaveRequestLogic annualLeaveRequestLogic)
        {
            _annualLeaveRequestLogic = annualLeaveRequestLogic;
        }

        public void OnGet(int id)
        {
            var annualLeaveRequest = _annualLeaveRequestLogic.GetRequest(id);

            int year = annualLeaveRequest?.Year ?? DateTime.UtcNow.Year;

            AnnualLeaveRequestDetailsViewModel = new AnnualLeaveRequestDetailsViewModel()
            {
                Year = year,
                AnnualLeaveRequest = annualLeaveRequest,
                IsEditable = year >= DateTime.UtcNow.Year,
            };
        }
    }
}
