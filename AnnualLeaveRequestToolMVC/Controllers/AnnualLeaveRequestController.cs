using AnnualLeaveRequest.Shared;
using AnnualLeaveRequestToolMVC.Interfaces;
using AnnualLeaveRequestToolMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace AnnualLeaveRequestToolMVC.Controllers
{
    public class AnnualLeaveRequestController : Controller
    {
        private readonly ILogger<AnnualLeaveRequestController> _logger;
        private readonly IAnnualLeaveRequestLogic _annualLeaveRequestLogic;

        private bool IsValidAnnualLeaveRequest(AnnualLeaveRequestOverviewModel annualLeaveRequest) => annualLeaveRequest != null 
                                                                        && string.IsNullOrEmpty(annualLeaveRequest.ErrorMessage);

        public AnnualLeaveRequestController(ILogger<AnnualLeaveRequestController> logger, IAnnualLeaveRequestLogic annualLeaveRequestLogic)
        {
            _logger = logger;
            _annualLeaveRequestLogic = annualLeaveRequestLogic;
        }

        public IActionResult Overview(int selectedYear)
        {
            int yearForOverviewView = selectedYear > 0 ? selectedYear : DateTime.UtcNow.Year;

            var annualLeaveRequestsForYear = _annualLeaveRequestLogic.GetRequestsForYear(yearForOverviewView);

            return View(annualLeaveRequestsForYear);
        }

        public IActionResult Details(int annualLeaveRequestId)
        {
            var annualLeaveRequest = _annualLeaveRequestLogic.GetRequest(annualLeaveRequestId);

            int year = annualLeaveRequest?.Year ?? DateTime.UtcNow.Year;

            var annualLeaveRequestDetailsViewModel = new AnnualLeaveRequestDetailsViewModel()
            {
                Year = year,
                AnnualLeaveRequest = annualLeaveRequest,
                IsEditable = year >= DateTime.UtcNow.Year,
            };

            return View(annualLeaveRequestDetailsViewModel);
        }

        public IActionResult Create()
        {
            var createAnnualLeaveRequest = _annualLeaveRequestLogic.GetCreateViewModelForCreate();

            return View(createAnnualLeaveRequest);
        }

        [HttpPost]
        public IActionResult Create(AnnualLeaveRequestCreateViewModel newAnnualLeaveRequestViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAnnualLeaveRequest = new AnnualLeaveRequestOverviewModel()
                {
                    Year = newAnnualLeaveRequestViewModel.StartDate.Year,
                    PaidLeaveType = newAnnualLeaveRequestViewModel.PaidLeaveType,
                    LeaveType = newAnnualLeaveRequestViewModel.LeaveType,
                    StartDate = newAnnualLeaveRequestViewModel.StartDate,
                    ReturnDate = newAnnualLeaveRequestViewModel.ReturnDate,
                    Notes = newAnnualLeaveRequestViewModel.Notes,
                };

                newAnnualLeaveRequest = _annualLeaveRequestLogic.Create(newAnnualLeaveRequest);
                
                if(IsValidAnnualLeaveRequest(newAnnualLeaveRequest))
                {
                    return RedirectToAction("Overview", new { selectedYear = newAnnualLeaveRequest.Year });
                }
                else
                {
                    var createAnnualLeaveRequest = _annualLeaveRequestLogic.GetCreateViewModelForCreate(errorMessage: newAnnualLeaveRequest?.ErrorMessage ?? string.Empty);

                    return View(createAnnualLeaveRequest);
                }  
            }
            else
            {
                var createAnnualLeaveRequest = _annualLeaveRequestLogic.GetCreateViewModelForCreate();

                return View(createAnnualLeaveRequest);
            }
        }

        public IActionResult Edit(int annualLeaveRequestId)
        {
            var editAnnualLeaveRequestViewModel = _annualLeaveRequestLogic.GetCreateViewModelForEdit(annualLeaveRequestId);

            return View(editAnnualLeaveRequestViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AnnualLeaveRequestCreateViewModel editAnnualLeaveRequestViewModel)
        {
            if (ModelState.IsValid)
            {
                var editAnnualLeaveRequest = new AnnualLeaveRequestOverviewModel()
                {
                    AnnualLeaveRequestID = editAnnualLeaveRequestViewModel.AnnualLeaveRequestID,
                    Year = editAnnualLeaveRequestViewModel.StartDate.Year,
                    PaidLeaveType = editAnnualLeaveRequestViewModel.PaidLeaveType,
                    LeaveType = editAnnualLeaveRequestViewModel.LeaveType,
                    StartDate = editAnnualLeaveRequestViewModel.StartDate,
                    ReturnDate = editAnnualLeaveRequestViewModel.ReturnDate,
                    Notes = editAnnualLeaveRequestViewModel.Notes,
                };

                editAnnualLeaveRequest = _annualLeaveRequestLogic.Update(editAnnualLeaveRequest);

                if (IsValidAnnualLeaveRequest(editAnnualLeaveRequest))
                {
                    return RedirectToAction("Overview", new { selectedYear = editAnnualLeaveRequest.Year });
                }
                else
                {
                    string errorMessage = editAnnualLeaveRequest?.ErrorMessage ?? string.Empty;

                    var editAnnualLeaveRequestViewModelInError = _annualLeaveRequestLogic.GetCreateViewModelForEdit(editAnnualLeaveRequestViewModel.AnnualLeaveRequestID, errorMessage: errorMessage);

                    return View(editAnnualLeaveRequestViewModelInError);
                }
            }
            else
            {
                var editAnnualLeaveRequestViewModelInError = _annualLeaveRequestLogic.GetCreateViewModelForEdit(editAnnualLeaveRequestViewModel.AnnualLeaveRequestID);

                return View(editAnnualLeaveRequestViewModelInError);
            }
        }

        public IActionResult Delete(int annualLeaveRequestId)
        {
            var annualLeaveRequest = _annualLeaveRequestLogic.GetRequest(annualLeaveRequestId);

            int year = annualLeaveRequest?.Year ?? DateTime.UtcNow.Year;

            var annualLeaveRequestDeleteViewModel = new AnnualLeaveRequestDeleteViewModel()
            {
                Year = year,
                AnnualLeaveRequestID = annualLeaveRequest.AnnualLeaveRequestID,
                AnnualLeaveRequest = annualLeaveRequest,
                IsDeletable = year >= DateTime.UtcNow.Year,
            };

            return View(annualLeaveRequestDeleteViewModel);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int annualLeaveRequestId)
        {
            _annualLeaveRequestLogic.Delete(annualLeaveRequestId);

            return RedirectToAction("Overview", new { selectedYear = DateTime.UtcNow.Year });
        }

        [HttpPost]
        public IActionResult ChangeSelectedYear(int selectedYear)
        {
            return RedirectToAction("Overview", new { selectedYear = selectedYear });
        }
    }
}
