using AnnualLeaveRequestToolWebForms.Models;
using System;
using System.Collections.Generic;

namespace AnnualLeaveRequestToolWebForms.Data
{
    public class ErrorHandler : IErrorHandler
    {
        public List<string> CheckAnnualLeaveRequestEntryIsValid(AnnualLeaveRequestInput annualLeaveRequestInput)
        {
            string defaultDropdownSelectItem = "please select";
            
            var errors = new List<string>();

            if (string.IsNullOrEmpty(annualLeaveRequestInput.StartDate))
            {
                errors.Add("Start date was not set");
            }
            else
            {
                string errorMessage = CheckForValidDate(annualLeaveRequestInput.StartDate, "Start date");

                if(!string.IsNullOrEmpty(errorMessage))
                {
                    errors.Add(errorMessage);
                }
            }

            if (string.IsNullOrEmpty(annualLeaveRequestInput.ReturnDate))
            {
                errors.Add("Return date was not set");
            }
            else
            {
                string errorMessage = CheckForValidDate(annualLeaveRequestInput.ReturnDate, "Return date");

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    errors.Add(errorMessage);
                }
            }

            if (annualLeaveRequestInput.PaidLeaveType.ToLower().Equals(defaultDropdownSelectItem))
            {
                errors.Add("Paid leave type was not set");
            }

            if (annualLeaveRequestInput.LeaveType.ToLower().Equals(defaultDropdownSelectItem))
            {
                errors.Add("Leave type was not set");
            }

            return errors;
        }

        private string CheckForValidDate(string dateAsText, string dateName)
        {
            DateTime minDateTime = new DateTime(2020, 01, 01);
            DateTime maxDateTime = new DateTime(2100, 01, 01);

            bool isValidDate = DateTime.TryParse(dateAsText, out DateTime date);

            return !isValidDate || date < minDateTime || date > maxDateTime
                        ? $"{dateName} of {date.ToString("dd/MM/yyyy")} is not a valid date" 
                        : string.Empty;
        }
    }
}