namespace AnnualLeaveRequestEFDAL.Models
{
    public partial class AnnualLeaveYear
    {
        public int Year { get; set; }
        public decimal NumberOfDays { get; set; }
        public decimal NumberOfAnnualLeaveDays { get; set; }
        public decimal NumberOfPublicLeaveDays { get; set; }
        public decimal NumberOfDaysLeft { get; set; }
        public decimal NumberOfAnnualLeaveDaysLeft { get; set; }
        public decimal NumberOfPublicLeaveDaysLeft { get; set; }
    }
}
