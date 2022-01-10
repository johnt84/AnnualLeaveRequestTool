using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace AnnualLeaveRequestToolWebForms.Common
{
    public partial class ErrorMessage : UserControl
    {
        protected List<string> ErrorMessages = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        internal void DisplayErrorMessage(string errorMessage)
        {
            int errorMessagesPosition = errorMessage.IndexOf("\\\\n");

            string errorMessageTrimmed = errorMessagesPosition > 0 ? 
                                            errorMessage.Substring(errorMessagesPosition) 
                                            : errorMessage;

            string errorMessageCleaned = errorMessageTrimmed.Replace("\\\\n", "*").Replace(@"""", string.Empty);

            ErrorMessages = errorMessageCleaned.Split('*').ToList();
            ErrorMessages.RemoveAll(x => string.IsNullOrEmpty(x));
        }

        internal void DisplayErrorMessages(List<string> errors)
        {
            if(errors == null || errors.Count == 0)
            {
                return;
            }
            
            ErrorMessages = errors;
            ErrorMessages.RemoveAll(x => string.IsNullOrEmpty(x));
        }
    }
}