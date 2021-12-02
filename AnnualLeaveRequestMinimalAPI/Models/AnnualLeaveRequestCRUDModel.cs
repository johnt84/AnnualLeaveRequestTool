using System.ComponentModel.DataAnnotations;

namespace AnnualLeaveRequestMinimalAPI.Models
{
    public class AnnualLeaveRequestCRUDModel
    {
        public static int Required { get; private set; }
        public int AnnualLeaveRequestID { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string PaidLeaveType { get; set; } = string.Empty;

        [Required]
        public string LeaveType { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime ReturnDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public string Notes { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
