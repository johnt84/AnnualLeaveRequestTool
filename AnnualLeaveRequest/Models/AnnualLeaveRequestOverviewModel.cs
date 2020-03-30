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
        [Range(1, 31, ErrorMessage = "Invalid number of days (1-30).")]
        public int NumberOfDaysRequested { get; set; }

        [Required]
        [Range(0, 31, ErrorMessage = "Invalid number of days (0-30).")]
        public int NumberOfAnnualLeaveDaysRequested { get; set; }

        [Required]
        [Range(0, 31, ErrorMessage = "Invalid number of days (0-3).")]
        public int NumberOfPublicLeaveDaysRequested { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public string Notes { get; set; }
        public int NumberOfDays { get; set; }
        public int NumberOfAnnualLeaveDays { get; set; }
        public int NumberOfPublicDays { get; set; }
        public int NumberOfDaysLeft { get; set; }
        public int NumberOfAnnualLeaveDaysLeft { get; set; }
        public int NumberOfPublicLeaveDaysLeft { get; set; }
    }
}
