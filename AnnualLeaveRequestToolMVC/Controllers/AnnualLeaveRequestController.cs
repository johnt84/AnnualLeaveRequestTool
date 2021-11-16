using AnnualLeaveRequestToolMVC.Interfaces;
using AnnualLeaveRequestToolMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IActionResult Overview()
        {
            int year = DateTime.UtcNow.Year;

            var annualLeaveRequests = _annualLeaveRequestLogic.GetRequestsForYear(year);

            return View(annualLeaveRequests);
        }

        public IActionResult Details(int annualLeaveRequestId)
        {
            var annualLeaveRequest = _annualLeaveRequestLogic.GetRequest(annualLeaveRequestId);

            return View(annualLeaveRequest);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AnnualLeaveRequestOverviewViewModel newAnnualLeaveRequestViewModel)
        {
            if (ModelState.IsValid)
            {
                int year = DateTime.UtcNow.Year;

                var newAnnualLeaveRequest = new AnnualLeaveRequestOverviewModel()
                {
                    Year = year,
                    PaidLeaveType = newAnnualLeaveRequestViewModel.PaidLeaveType,
                    LeaveType = newAnnualLeaveRequestViewModel.LeaveType,
                    StartDate = newAnnualLeaveRequestViewModel.StartDate,
                    ReturnDate = newAnnualLeaveRequestViewModel.ReturnDate,
                    Notes = newAnnualLeaveRequestViewModel.Notes,
                };

                newAnnualLeaveRequest = _annualLeaveRequestLogic.Create(newAnnualLeaveRequest);

                return RedirectToAction("Overview");
            }
            else
            {
                return View(newAnnualLeaveRequestViewModel);
            }
        }

        public IActionResult Edit(int annualLeaveRequestId)
        {
            var annualLeaveRequest = _annualLeaveRequestLogic.GetRequest(annualLeaveRequestId);

            var editAnnualLeaveRequestViewModel = new AnnualLeaveRequestOverviewViewModel()
            {
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
        public IActionResult Edit(AnnualLeaveRequestOverviewViewModel editAnnualLeaveRequestViewModel)
        {
            if (ModelState.IsValid)
            {
                int year = DateTime.UtcNow.Year;

                var editAnnualLeaveRequest = new AnnualLeaveRequestOverviewModel()
                {
                    AnnualLeaveRequestID = editAnnualLeaveRequestViewModel.AnnualLeaveRequestID,
                    Year = year,
                    PaidLeaveType = editAnnualLeaveRequestViewModel.PaidLeaveType,
                    LeaveType = editAnnualLeaveRequestViewModel.LeaveType,
                    StartDate = editAnnualLeaveRequestViewModel.StartDate,
                    ReturnDate = editAnnualLeaveRequestViewModel.ReturnDate,
                    Notes = editAnnualLeaveRequestViewModel.Notes,
                };

                editAnnualLeaveRequest = _annualLeaveRequestLogic.Update(editAnnualLeaveRequest);

                return RedirectToAction("Overview");
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

            return RedirectToAction("Overview");
        }
    }
}
