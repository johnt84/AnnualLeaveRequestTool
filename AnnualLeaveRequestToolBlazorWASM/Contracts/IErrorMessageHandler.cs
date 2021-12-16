namespace AnnualLeaveRequestToolBlazorWASM.Contracts
{
    public interface IErrorMessageHandler
    {
        string GetErrorMessagesForDisplay(string errorMessage);
    }
}
