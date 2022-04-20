namespace AnnualLeaveRequestEFDAL.Models
{
    public partial class AnnualLeaveRequest
    {
        public int AnnualLeaveRequestId { get; set; }
        public string PaidLeaveType { get; set; } = null!;
        public string LeaveType { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal NumberOfDays { get; set; }
        public decimal NumberOfAnnualLeaveDays { get; set; }
        public decimal NumberOfPublicLeaveDays { get; set; }
        public DateTime DateCreated { get; set; }
        public string? Notes { get; set; }
        public int Year { get; set; }
        public int? AssociatedAnnualLeaveRequestId { get; set; }
    }
}
