using MediatR;

namespace AnnualLeaveRequestMinimalAPI.Queries
{
    public record GetDaysBetweenStartDateAndReturnDateQuery(DateTime startDate, DateTime returnDate) : IRequest<decimal>;

    public class GetDaysBetweenStartDateAndReturnDateQueryClass
    {
        public DateTime StartDate { get; set; }  
        public DateTime ReturnDate { get; set; }  

        public GetDaysBetweenStartDateAndReturnDateQueryClass(DateTime startDate, DateTime returnDate)
        {
            StartDate = startDate;
            ReturnDate = returnDate;
        }
    }
}
