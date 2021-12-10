using AnnualLeaveRequestToolRazorPages.Interfaces;
using AnnualLeaveRequestToolRazorPages.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace AnnualLeaveRequestToolRazorPages.Pages.AnnualLeaveRequest
{
    public class DeleteModel : PageModel
    {
        public AnnualLeaveRequestDeleteViewModel AnnualLeaveRequestDeleteViewModel;

        private readonly IAnnualLeaveRequestLogic _annualLeaveRequestLogic;

        public DeleteModel(IAnnualLeaveRequestLogic annualLeaveRequestLogic)
        {
            _annualLeaveRequestLogic = annualLeaveRequestLogic;
        }

        public void OnGet(int id)
        {
            var annualLeaveRequest = _annualLeaveRequestLogic.GetRequest(id);

            int year = annualLeaveRequest?.Year ?? DateTime.UtcNow.Year;

            AnnualLeaveRequestDeleteViewModel = new AnnualLeaveRequestDeleteViewModel()
            {
                Year = year,
                AnnualLeaveRequestID = annualLeaveRequest.AnnualLeaveRequestID,
                AnnualLeaveRequest = annualLeaveRequest,
                IsDeletable = year >= DateTime.UtcNow.Year,
            };
        }

        public IActionResult OnPost(int ID)
        {
            var annualLeaveRequest = _annualLeaveRequestLogic.GetRequest(ID);

            int year = annualLeaveRequest?.Year ?? DateTime.UtcNow.Year;

            _annualLeaveRequestLogic.Delete(ID);

            return RedirectToPage("Overview", new { id = year });
        }
    }
}
