using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnnualLeaveRequestToolMVC.Models.ViewModels
{
    public class AnnualLeaveRequestCreateViewModel
    {
        public int AnnualLeaveRequestID { get; set; }

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
        public List<string> PaidLeaveTypes { get; set; }
        public List<string> LeaveTypes { get; set; }
    }
}
