using AnnualLeaveRequestToolMVC.Interfaces;
using AnnualLeaveRequestToolMVC.Models;
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

            return View(annualLeaveRequest);
        }

        public IActionResult Create(int selectedYear)
        {
            var createAnnualLeaveRequest = new AnnualLeaveRequestCreateViewModel()
            {
                Year = selectedYear,
            };

            return View(createAnnualLeaveRequest);
        }

        [HttpPost]
        public IActionResult Create(AnnualLeaveRequestCreateViewModel newAnnualLeaveRequestViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAnnualLeaveRequest = new AnnualLeaveRequestOverviewModel()
                {
                    Year = newAnnualLeaveRequestViewModel.Year,
                    PaidLeaveType = newAnnualLeaveRequestViewModel.PaidLeaveType,
                    LeaveType = newAnnualLeaveRequestViewModel.LeaveType,
                    StartDate = newAnnualLeaveRequestViewModel.StartDate,
                    ReturnDate = newAnnualLeaveRequestViewModel.ReturnDate,
                    Notes = newAnnualLeaveRequestViewModel.Notes,
                };

                newAnnualLeaveRequest = _annualLeaveRequestLogic.Create(newAnnualLeaveRequest);

                return RedirectToAction("Overview", new { selectedYear = newAnnualLeaveRequest.Year });
            }
            else
            {
                return View(newAnnualLeaveRequestViewModel);
            }
        }

        public IActionResult Edit(int annualLeaveRequestId)
        {
            var annualLeaveRequest = _annualLeaveRequestLogic.GetRequest(annualLeaveRequestId);

            var editAnnualLeaveRequestViewModel = new AnnualLeaveRequestCreateViewModel()
            {
                AnnualLeaveRequestID = annualLeaveRequest.AnnualLeaveRequestID,
                Year = annualLeaveRequest.Year,
                PaidLeaveType = annualLeaveRequest.PaidLeaveType,
                LeaveType = annualLeaveRequest.LeaveType,
                StartDate = annualLeaveRequest.StartDate,
                ReturnDate = annualLeaveRequest.ReturnDate,
                Notes = annualLeaveRequest.Notes,
            };

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
                    Year = editAnnualLeaveRequestViewModel.Year,
                    PaidLeaveType = editAnnualLeaveRequestViewModel.PaidLeaveType,
                    LeaveType = editAnnualLeaveRequestViewModel.LeaveType,
                    StartDate = editAnnualLeaveRequestViewModel.StartDate,
                    ReturnDate = editAnnualLeaveRequestViewModel.ReturnDate,
                    Notes = editAnnualLeaveRequestViewModel.Notes,
                };

                editAnnualLeaveRequest = _annualLeaveRequestLogic.Update(editAnnualLeaveRequest);

                return RedirectToAction("Overview", new { selectedYear = editAnnualLeaveRequest.Year });
            }
            else
            {
                return View(editAnnualLeaveRequestViewModel);
            }
        }

        public IActionResult Delete(int annualLeaveRequestId)
        {
            var annualLeaveRequest = _annualLeaveRequestLogic.GetRequest(annualLeaveRequestId);

            return View(annualLeaveRequest);
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
