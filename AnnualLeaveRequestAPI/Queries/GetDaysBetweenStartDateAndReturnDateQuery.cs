using MediatR;
using System;

namespace AnnualLeaveRequestAPI.Queries
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
