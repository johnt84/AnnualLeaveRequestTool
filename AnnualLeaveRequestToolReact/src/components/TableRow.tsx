import moment from "moment";

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
  request: AnnualLeaveRequest;
  setSelectedRequest: (annualLeaveRequest: AnnualLeaveRequest) => void;
  handleEditRequest: (id: string) => void;
  handleDeleteRequest: (id: string) => void;
}

const TableRow = ({
  request,
  setSelectedRequest,
  handleEditRequest,
  handleDeleteRequest,
}: Props) => {
  return (
    <tr>
      <td>{moment(request.startDate).format("DD MMM yyyy")}</td>
      <td>{moment(request.returnDate).format("DD MMM yyyy")}</td>
      <td>{request.numberOfDaysRequested.toString()}</td>
      <td>{request.numberOfAnnualLeaveDaysRequested.toString()}</td>
      <td>{request.numberOfPublicLeaveDaysRequested.toString()}</td>
      <td>{request.numberOfDaysLeft.toString()}</td>
      <td>{request.numberOfAnnualLeaveDaysLeft.toString()}</td>
      <td>{request.numberOfPublicLeaveDaysLeft.toString()}</td>
      <td>{request.notes}</td>
      <td>
        <button
          className="btn btn-primary"
          onClick={() => setSelectedRequest(request)}
        >
          View
        </button>
      </td>
      <td>
        <button
          className="btn btn-primary"
          onClick={() => handleEditRequest(request.id)}
        >
          Edit
        </button>
      </td>
      <td>
        <button
          className="btn btn-primary"
          onClick={() => handleDeleteRequest(request.id)}
        >
          Delete
        </button>
      </td>
    </tr>
  );
};

export default TableRow;
