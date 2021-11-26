namespace AnnualLeaveRequest.Shared
{
    public class SqlConnectionConfiguration
    {
        public string Value { get; }

        public SqlConnectionConfiguration(string value) => Value = value;
    }
}
