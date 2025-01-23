import TableRow from "./TableRow";

interface AnnualLeaveRequest {
  id: string;
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
}

interface Props {
  requests: AnnualLeaveRequest[];
  setSelectedRequest: (annualLeaveRequest: AnnualLeaveRequest) => void;
  handleEditRequest: (id: string) => void;
  handleDeleteRequest: (id: string) => void;
}

const Table = ({
  requests,
  setSelectedRequest,
  handleEditRequest,
  handleDeleteRequest,
}: Props) => {
  return (
    <>
      <h1>Annual Leave Requests Overview</h1>
      {requests.length === 0 && <p>No annual leave requests exist</p>}
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
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
          </tr>
        </thead>
        <tbody>
          {requests.map((item) => (
            <TableRow
              request={item}
              setSelectedRequest={setSelectedRequest}
              handleEditRequest={handleEditRequest}
              handleDeleteRequest={handleDeleteRequest}
            />
          ))}
        </tbody>
      </table>
    </>
  );
};

export default Table;
