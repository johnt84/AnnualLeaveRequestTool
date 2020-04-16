using System;
using System.ComponentModel.DataAnnotations;

namespace AnnualLeaveRequest.Models
{
    public class AnnualLeaveRequestOverviewModel
    {
        public int AnnualLeaveRequestID { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string PaidLeaveType { get; set; }

        [Required]
        public string LeaveType { get; set; }

        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime ReturnDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public string Notes { get; set; }
        public decimal NumberOfDays { get; set; }
        public decimal NumberOfAnnualLeaveDays { get; set; }
        public decimal NumberOfPublicLeaveDays { get; set; }
        public decimal NumberOfDaysRequested { get; set; }
        public decimal NumberOfAnnualLeaveDaysRequested { get; set; }
        public decimal NumberOfPublicLeaveDaysRequested { get; set; }
        public decimal NumberOfDaysLeft { get; set; }
        public decimal NumberOfAnnualLeaveDaysLeft { get; set; }
        public decimal NumberOfPublicLeaveDaysLeft { get; set; }
        public decimal NumberOfDaysLeftForYear { get; set; }
        public decimal NumberOfAnnualLeaveDaysLeftForYear { get; set; }
        public decimal NumberOfPublicLeaveDaysLeftForYear { get; set; }
        public string ErrorMessage { get; set; }
    }
}
