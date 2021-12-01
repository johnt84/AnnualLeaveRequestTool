using System;
using System.ComponentModel.DataAnnotations;

namespace AnnualLeaveRequestAPI.Models
{
    public class AnnualLeaveRequestCRUDModel
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
        public string ErrorMessage { get; set; }
    }
}
