using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnnualLeaveRequestToolMVC.Models.ViewModels
{
    public class AnnualLeaveRequestCreateViewModel
    {
        public int AnnualLeaveRequestID { get; set; }

        [Required]
        [Display(Name = "Paid Leave Type")]
        public string PaidLeaveType { get; set; }

        [Required]
        [Display(Name = "Leave Type")]
        public string LeaveType { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public string Notes { get; set; }
        public List<string> PaidLeaveTypes { get; set; }
        public List<string> LeaveTypes { get; set; }
    }
}
