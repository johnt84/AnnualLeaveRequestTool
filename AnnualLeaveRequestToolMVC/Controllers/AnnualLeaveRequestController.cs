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
        private static List<AnnualLeaveRequestOverviewModel> _annualLeaveRequests = new List<AnnualLeaveRequestOverviewModel>()
        {
            new AnnualLeaveRequestOverviewModel()
            {
                AnnualLeaveRequestID = 1,

                Year = 2021,
                PaidLeaveType = "Paid",
                LeaveType = "Annual Leave",
                StartDate = new DateTime(2021,01,01),
                ReturnDate = new DateTime(2021,01,06),
                NumberOfDaysRequested = 4,
                NumberOfAnnualLeaveDaysRequested = 3,
                NumberOfPublicLeaveDaysRequested = 1,
                NumberOfDays = 28,
                NumberOfAnnualLeaveDays = 25,
                NumberOfPublicLeaveDays = 3,
                NumberOfDaysLeft = 24,
                NumberOfAnnualLeaveDaysLeft = 22,
                NumberOfPublicLeaveDaysLeft = 2,
                Notes = "Xmas and New Year",
            },
            new AnnualLeaveRequestOverviewModel()
            {
                AnnualLeaveRequestID = 2,
                PaidLeaveType = "Paid",
                LeaveType = "Annual Leave",
                Year = 2021,
                StartDate = new DateTime(2021,07,05),
                ReturnDate = new DateTime(2021,07,12),
                NumberOfDaysRequested = 5,
                NumberOfAnnualLeaveDaysRequested = 5,
                NumberOfPublicLeaveDaysRequested = 0,
                NumberOfDays = 28,
                NumberOfAnnualLeaveDays = 25,
                NumberOfPublicLeaveDays = 3,
                NumberOfDaysLeft = 19,
                NumberOfAnnualLeaveDaysLeft = 17,
                NumberOfPublicLeaveDaysLeft = 2,
                Notes = "Summer Holiday",
            },
             new AnnualLeaveRequestOverviewModel()
            {
                AnnualLeaveRequestID = 3,
                PaidLeaveType = "Paid",
                LeaveType = "Annual Leave",
                Year = 2021,
                StartDate = new DateTime(2021,12,20),
                ReturnDate = new DateTime(2021,12,31),
                NumberOfDaysRequested = 9,
                NumberOfAnnualLeaveDaysRequested = 7,
                NumberOfPublicLeaveDaysRequested = 2,
                NumberOfDays = 28,
                NumberOfAnnualLeaveDays = 25,
                NumberOfPublicLeaveDays = 3,
                NumberOfDaysLeft = 10,
                NumberOfAnnualLeaveDaysLeft = 10,
                NumberOfPublicLeaveDaysLeft = 0,
                Notes = "Xmas and New Year",
            },
        };

        private AnnualLeaveRequestOverviewModel GetAnnualLeaveRequest(int annualLeaveRequestID) => _annualLeaveRequests
                                                                                        .Where(x => x.AnnualLeaveRequestID == annualLeaveRequestID)
                                                                                        .FirstOrDefault();

        private readonly ILogger<AnnualLeaveRequestController> _logger;

        public AnnualLeaveRequestController(ILogger<AnnualLeaveRequestController> logger)
        {
            _logger = logger;
        }

        public IActionResult Overview()
        {
            return View(_annualLeaveRequests);
        }

        public IActionResult Details(int annualLeaveRequestId)
        {
            var annualLeaveRequest = GetAnnualLeaveRequest(annualLeaveRequestId);

            return View(annualLeaveRequest);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AnnualLeaveRequestOverviewModel newAnnualLeaveRequest)
        {
            if (ModelState.IsValid)
            {
                int lastAnnualLeaveRequestID = _annualLeaveRequests.Select(x => x.AnnualLeaveRequestID).LastOrDefault();

                newAnnualLeaveRequest.AnnualLeaveRequestID = lastAnnualLeaveRequestID + 1;

                _annualLeaveRequests.Add(newAnnualLeaveRequest);

                return RedirectToAction("Overview");
            }
            else
            {
                return View(newAnnualLeaveRequest);
            }
        }

        public IActionResult Edit(int annualLeaveRequestId)
        {
            var annualLeaveRequest = GetAnnualLeaveRequest(annualLeaveRequestId);

            return View(annualLeaveRequest);
        }

        [HttpPost]
        public IActionResult Edit(AnnualLeaveRequestOverviewModel editAnnualLeaveRequest)
        {
            if (ModelState.IsValid)
            {
                int annualLeaveRequestIndex = _annualLeaveRequests
                                                .FindIndex(x => x.AnnualLeaveRequestID == editAnnualLeaveRequest.AnnualLeaveRequestID);

                _annualLeaveRequests[annualLeaveRequestIndex] = editAnnualLeaveRequest;

                return RedirectToAction("Overview");
            }
            else
            {
                return View(editAnnualLeaveRequest);
            }
        }

        public IActionResult Delete(int annualLeaveRequestId)
        {
            var annualLeaveRequest = GetAnnualLeaveRequest(annualLeaveRequestId);

            return View(annualLeaveRequest);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int annualLeaveRequestId)
        {
            _annualLeaveRequests.RemoveAll(x => x.AnnualLeaveRequestID == annualLeaveRequestId);

            return RedirectToAction("Overview");
        }
    }
}
