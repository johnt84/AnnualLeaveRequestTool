import moment from "moment";

interface Props {
  annualLeaveRequests: AnnualLeaveRequest[];
  selectedRequest: AnnualLeaveRequest;
  selectRequest: (annualLeaveRequest: AnnualLeaveRequest) => void;
}

type AnnualLeaveRequest = {
  recordNumber: number;
  startDate: Date;
  returnDate: Date;
  numberOfDaysRequested: number;
  numberOfAnnualLeaveDaysRequested: number;
  numberOfPublicLeaveDaysRequested: number;
  numberOfDaysLeft: number;
  numberOfAnnualLeaveDaysLeft: number;
  numberOfPublicLeaveDaysLeft: number;
  paidLeaveType: string;
  leaveType: string;
  notes: string;
};

const Table = ({ annualLeaveRequests, selectRequest }: Props) => {
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
            <tr key={item.recordNumber}>
              <td>{moment(item.startDate).format("DD MMM yyyy")}</td>
              <td>{moment(item.returnDate).format("DD MMM yyyy")}</td>
              <td>{item.numberOfDaysRequested.toString()}</td>
              <td>{item.numberOfAnnualLeaveDaysRequested.toString()}</td>
              <td>{item.numberOfPublicLeaveDaysRequested.toString()}</td>
              <td>{item.numberOfDaysLeft.toString()}</td>
              <td>{item.numberOfAnnualLeaveDaysLeft.toString()}</td>
              <td>{item.numberOfPublicLeaveDaysLeft.toString()}</td>
              <td>{item.notes}</td>
              <td>
                <button
                  type="button"
                  className="btn btn-primary"
                  onClick={() => selectRequest(item)}
                >
                  View
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  );
};

export default Table;
