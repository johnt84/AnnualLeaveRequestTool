using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestToolRazorPages.Interfaces;
using AnnualLeaveRequestToolRazorPages.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnnualLeaveRequestToolRazorPages.Pages.AnnualLeaveRequest
{
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public AnnualLeaveRequestCreateViewModel AnnualLeaveRequestEditViewModel { get; set; }


        private readonly IAnnualLeaveRequestLogic _annualLeaveRequestLogic;

        public EditModel(IAnnualLeaveRequestLogic annualLeaveRequestLogic)
        {
            _annualLeaveRequestLogic = annualLeaveRequestLogic;
        }

        public void OnGet(int id)
        {
            AnnualLeaveRequestEditViewModel = _annualLeaveRequestLogic.GetCreateViewModelForEdit(id);
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var editAnnualLeaveRequest = new AnnualLeaveRequestOverviewModel()
                {
                    AnnualLeaveRequestID = AnnualLeaveRequestEditViewModel.AnnualLeaveRequestID,
                    Year = AnnualLeaveRequestEditViewModel.StartDate.Year,
                    PaidLeaveType = AnnualLeaveRequestEditViewModel.PaidLeaveType,
                    LeaveType = AnnualLeaveRequestEditViewModel.LeaveType,
                    StartDate = AnnualLeaveRequestEditViewModel.StartDate,
                    ReturnDate = AnnualLeaveRequestEditViewModel.ReturnDate,
                    Notes = AnnualLeaveRequestEditViewModel.Notes,
                };

                editAnnualLeaveRequest = _annualLeaveRequestLogic.Update(editAnnualLeaveRequest);

                if (_annualLeaveRequestLogic.IsValidAnnualLeaveRequest(editAnnualLeaveRequest))
                {
                    return RedirectToPage("Overview", new { id = editAnnualLeaveRequest.Year });
                }
                else
                {
                    string errorMessage = editAnnualLeaveRequest?.ErrorMessage ?? string.Empty;

                    var editAnnualLeaveRequestViewModelInError = _annualLeaveRequestLogic.GetCreateViewModelForEdit(AnnualLeaveRequestEditViewModel.AnnualLeaveRequestID, errorMessage: errorMessage);

                    return RedirectToPage("Edit", new { editAnnualLeaveRequestViewModelInError });
                }
            }
            else
            {
                var editAnnualLeaveRequestViewModelInError = _annualLeaveRequestLogic.GetCreateViewModelForEdit(AnnualLeaveRequestEditViewModel.AnnualLeaveRequestID);

                return RedirectToPage("Edit", new { editAnnualLeaveRequestViewModelInError });
            }
        }
    }
}
