using System.ComponentModel.DataAnnotations.Schema;

namespace AnnualLeaveRequestEFDAL.Models
{
    public partial class AnnualLeaveRequestsOverview
    {
        public int? AnnualLeaveRequestId { get; set; }
        public int Year { get; set; }
        public string? PaidLeaveType { get; set; }
        public string? LeaveType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? NumberOfDaysRequested { get; set; }
        public decimal? NumberOfAnnualLeaveDaysRequested { get; set; }
        public decimal? NumberOfPublicLeaveDaysRequested { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? Notes { get; set; }
        public decimal NumberOfDays { get; set; }
        public decimal NumberOfAnnualLeaveDays { get; set; }
        public decimal NumberOfPublicLeaveDays { get; set; }
        public decimal NumberOfDaysLeftForYear { get; set; }
        public decimal NumberOfAnnualLeaveDaysLeftForYear { get; set; }
        public decimal NumberOfPublicLeaveDaysLeftForYear { get; set; }

        [NotMapped]
        public string ErrorMessage { get; set; }
    }
}
