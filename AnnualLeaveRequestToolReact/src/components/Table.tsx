const Table = () => {
  type AnnualLeaveRequest = {
    startDate: Date;
    returnDate: Date;
    numberOfDaysRequested: Number;
    numberOfAnnualLeaveDaysRequested: Number;
    numberOfPublicLeaveDaysRequested: Number;
    numberOfDaysLeft: Number;
    numberOfAnnualLeaveDaysLeft: Number;
    numberOfPublicLeaveDaysLeft: Number;
    notes: string;
  };

  const annualLeaveRequests: AnnualLeaveRequest[] = [
    {
      startDate: new Date("2025-01-01"),
      returnDate: new Date("2025-01-02"),
      numberOfDaysRequested: 1,
      numberOfAnnualLeaveDaysRequested: 0,
      numberOfPublicLeaveDaysRequested: 1,
      numberOfDaysLeft: 27,
      numberOfAnnualLeaveDaysLeft: 25,
      numberOfPublicLeaveDaysLeft: 2,
      notes: "New Years",
    },
    {
      startDate: new Date("2025-02-09"),
      returnDate: new Date("2025-02-10"),
      numberOfDaysRequested: 1,
      numberOfAnnualLeaveDaysRequested: 1,
      numberOfPublicLeaveDaysRequested: 0,
      numberOfDaysLeft: 26,
      numberOfAnnualLeaveDaysLeft: 24,
      numberOfPublicLeaveDaysLeft: 2,
      notes: "Birthday",
    },
  ];

  return (
    <>
      <h1>Annual Leave Requests Overview</h1>
      {annualLeaveRequests.length === 0 && (
        <p>No annual leave requests exist</p>
      )}
      <table className="table">
        <thead>
          <tr>
            <th scope="col">Start Date</th>
            <th scope="col">Return Date</th>
            <th scope="col">Number of Days Requested</th>
            <th scope="col">Number of Annual Leave Days Requested</th>
            <th scope="col">Number of Public Leave Days Requested</th>
            <th scope="col">Number of Days Left</th>
            <th scope="col">Number of Annual Leave Days Left</th>
            <th scope="col">Number of Public Holidays Left</th>
            <th scope="col">Notes</th>
          </tr>
        </thead>
        <tbody>
          {annualLeaveRequests.map((item) => (
            <tr>
              <td>
                {item.startDate.toLocaleDateString("en-us", {
                  year: "numeric",
                  month: "short",
                  day: "numeric",
                })}
              </td>
              <td>
                {item.returnDate.toLocaleDateString("en-us", {
                  year: "numeric",
                  month: "short",
                  day: "numeric",
                })}
              </td>
              <td>{item.numberOfDaysRequested.toString()}</td>
              <td>{item.numberOfAnnualLeaveDaysRequested.toString()}</td>
              <td>{item.numberOfPublicLeaveDaysRequested.toString()}</td>
              <td>{item.numberOfDaysLeft.toString()}</td>
              <td>{item.numberOfAnnualLeaveDaysLeft.toString()}</td>
              <td>{item.numberOfPublicLeaveDaysLeft.toString()}</td>
              <td>{item.notes}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  );
};

export default Table;
