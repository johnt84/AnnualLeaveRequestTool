namespace AnnualLeaveRequestEFDAL.Models
{
    public class AnnualLeaveRequestsBetweenTwoDates
    {
        public AnnualLeaveRequestsBetweenTwoDates(int year, decimal numberOfDays, decimal numberOfAnnualLeaveDays
			, decimal numberOfPublicLeaveDays, decimal numberOfDaysLeft
			, decimal numberOfAnnualLeaveDaysLeft, decimal numberOfPublicLeaveDaysLeft, decimal numberOfDaysLeftAfterRequest
			, decimal numberOfAnnualLeaveDaysLeftAfterRequest, decimal numberOfPublicLeaveDaysLeftAfterRequest, bool alreadyExists
			, int annualLeaveRequestID)
        {
			Year = year;
			NumberOfDays = numberOfDays;
			NumberOfAnnualLeaveDays = numberOfAnnualLeaveDays;
			NumberOfPublicLeaveDays = numberOfPublicLeaveDays;
			NumberOfDaysLeft = numberOfDaysLeft;
			NumberOfAnnualLeaveDaysLeft = numberOfAnnualLeaveDaysLeft;
			NumberOfPublicLeaveDaysLeft = numberOfPublicLeaveDaysLeft;
			NumberOfDaysLeftAfterRequest = numberOfDaysLeftAfterRequest;
			NumberOfAnnualLeaveDaysLeftAfterRequest = numberOfAnnualLeaveDaysLeftAfterRequest;
			NumberOfPublicLeaveDaysLeftAfterRequest = numberOfPublicLeaveDaysLeftAfterRequest;
			AlreadyExists = alreadyExists;
			AnnualLeaveRequestID = annualLeaveRequestID;
		}

        public int Year { get; private set; }
		public decimal NumberOfDays { get; private set; }
		public decimal NumberOfAnnualLeaveDays { get; private set; }
		public decimal NumberOfPublicLeaveDays { get; private set; }
		public decimal NumberOfDaysLeft { get; private set; }
		public decimal NumberOfAnnualLeaveDaysLeft { get; private set; }
		public decimal NumberOfPublicLeaveDaysLeft { get; private set; }
		public decimal NumberOfDaysLeftAfterRequest { get; private set; }
		public decimal NumberOfAnnualLeaveDaysLeftAfterRequest { get; private set; }
		public decimal NumberOfPublicLeaveDaysLeftAfterRequest { get; private set; }
		public bool AlreadyExists { get; private set; }
		public int AnnualLeaveRequestID { get; private set; }
	}
}
