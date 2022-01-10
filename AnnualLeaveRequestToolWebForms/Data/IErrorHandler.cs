using AnnualLeaveRequestToolWebForms.Models;
using System.Collections.Generic;

namespace AnnualLeaveRequestToolWebForms.Data
{
    internal interface IErrorHandler
    {
        List<string> CheckAnnualLeaveRequestEntryIsValid(AnnualLeaveRequestInput annualLeaveRequestInput);
    }
}
