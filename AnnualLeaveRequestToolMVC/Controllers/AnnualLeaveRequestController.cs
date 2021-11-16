using AnnualLeaveRequestToolMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AnnualLeaveRequestToolMVC.Controllers
{
    public class AnnualLeaveRequestController : Controller
    {
        private static List<AnnualLeaveRequestOverviewModel> _anuualLeaveRequests = new List<AnnualLeaveRequestOverviewModel>()
        {
            new AnnualLeaveRequestOverviewModel()
            {
                Year = 2021,
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
            },
            new AnnualLeaveRequestOverviewModel()
            {
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
            },
             new AnnualLeaveRequestOverviewModel()
            {
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
            },
        };

        public IActionResult Index()
        {
            return View(_anuualLeaveRequests);
        }
    }
}
