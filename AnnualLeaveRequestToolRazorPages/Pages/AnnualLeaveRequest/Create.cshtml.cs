using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestToolRazorPages.Interfaces;
using AnnualLeaveRequestToolRazorPages.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace AnnualLeaveRequestToolRazorPages.Pages.AnnualLeaveRequest
{
    public class CreateModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public AnnualLeaveRequestCreateViewModel AnnualLeaveRequestCreateViewModel { get; set; }

        [BindProperty]
        protected List<string> ErrorMessages { get; set; }

        private readonly IAnnualLeaveRequestLogic _annualLeaveRequestLogic;

        public CreateModel(IAnnualLeaveRequestLogic annualLeaveRequestLogic)
        {
            _annualLeaveRequestLogic = annualLeaveRequestLogic;
        }

        public void OnGet()
        {
            AnnualLeaveRequestCreateViewModel = _annualLeaveRequestLogic.GetCreateViewModelForCreate();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var newAnnualLeaveRequest = new AnnualLeaveRequestOverviewModel()
                {
                    Year = AnnualLeaveRequestCreateViewModel.StartDate.Year,
                    PaidLeaveType = AnnualLeaveRequestCreateViewModel.PaidLeaveType,
                    LeaveType = AnnualLeaveRequestCreateViewModel.LeaveType,
                    StartDate = AnnualLeaveRequestCreateViewModel.StartDate,
                    ReturnDate = AnnualLeaveRequestCreateViewModel.ReturnDate,
                    Notes = AnnualLeaveRequestCreateViewModel.Notes,
                };

                newAnnualLeaveRequest = _annualLeaveRequestLogic.Create(newAnnualLeaveRequest);

                if (_annualLeaveRequestLogic.IsValidAnnualLeaveRequest(newAnnualLeaveRequest))
                {
                    return RedirectToPage("Overview", new { id = newAnnualLeaveRequest.Year });
                }
                else
                {
                    var createAnnualLeaveRequest = _annualLeaveRequestLogic.GetCreateViewModelForCreate(errorMessage: newAnnualLeaveRequest?.ErrorMessage ?? string.Empty);

                    return RedirectToPage("Create", new { createAnnualLeaveRequest });
                }
            }
            else
            {
                var createAnnualLeaveRequest = _annualLeaveRequestLogic.GetCreateViewModelForCreate();

                return RedirectToPage("Create", new { createAnnualLeaveRequest });
            }
        }
    }
}
