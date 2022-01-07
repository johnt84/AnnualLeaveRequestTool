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

        public void DisplayErrorMessage(string errorMessage)
        {
            int errorMessagesPosition = errorMessage.IndexOf("\\\\n");

            string errorMessageTrimmed = errorMessage.Substring(errorMessagesPosition);
            string errorMessageCleaned = errorMessageTrimmed.Replace("\\\\n", "*").Replace(@"""", string.Empty);

            ErrorMessages = errorMessageCleaned.Split('*').ToList();
            ErrorMessages.RemoveAll(x => string.IsNullOrEmpty(x));
        }
    }
}