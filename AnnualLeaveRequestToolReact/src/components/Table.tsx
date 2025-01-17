import moment from "moment";

interface Props {
  annualLeaveRequests: AnnualLeaveRequest[];
  setAnnualLeaveRequests: (annualLeaveRequests: AnnualLeaveRequest[]) => void;
  selectedRequest: AnnualLeaveRequest;
  setSelectedRequest: (annualLeaveRequest: AnnualLeaveRequest) => void;
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

const Table = ({
  annualLeaveRequests,
  setAnnualLeaveRequests,
  setSelectedRequest,
}: Props) => {
  const editAnnualLeaveRequest = (request: AnnualLeaveRequest) => {
    const editRequests = annualLeaveRequests.map((annualLeaveRequest) => {
      if (request.recordNumber === annualLeaveRequest.recordNumber) {
        return {
          ...annualLeaveRequest,
          numberOfDaysRequested: 2,
        };
      } else {
        return annualLeaveRequest;
      }
    });
    setAnnualLeaveRequests(editRequests);
  };

  const deleteAnnualLeaveRequest = (request: AnnualLeaveRequest) => {
    const index = annualLeaveRequests.indexOf(request);

    const deleteRequests = [
      ...annualLeaveRequests.slice(0, index), // Elements before the one to delete
      ...annualLeaveRequests.slice(index + 1), // Elements after the one to delete
    ];

    setAnnualLeaveRequests(deleteRequests);
  };

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
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
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
                  className="btn btn-primary"
                  onClick={() => setSelectedRequest(item)}
                >
                  View
                </button>
              </td>
              <td>
                <button
                  className="btn btn-primary"
                  onClick={() => editAnnualLeaveRequest(item)}
                >
                  Edit
                </button>
              </td>
              <td>
                <button
                  className="btn btn-primary"
                  onClick={() => deleteAnnualLeaveRequest(item)}
                >
                  Delete
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
