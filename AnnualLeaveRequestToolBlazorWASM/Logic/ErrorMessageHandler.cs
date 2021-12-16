using AnnualLeaveRequestToolBlazorWASM.Contracts;

namespace AnnualLeaveRequestToolBlazorWASM.Logic
{
    public class ErrorMessageHandler : IErrorMessageHandler
    {
        public string GetErrorMessagesForDisplay(string errorMessage)
        {
            int start = errorMessage.IndexOf("\\n");
            int end = errorMessage.IndexOf(@"""", start);
            return errorMessage.Substring(start, end - start);
        }
    }
}
