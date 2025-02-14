import moment from "moment";

interface AnnualLeaveRequest {
  annualLeaveRequestId: number;
  year: string;
  paidLeaveType: string;
  leaveType: string;
  startDate: Date;
  returnDate: Date;
  dateCreated: Date;
  notes: string;
  numberOfDays: number;
  numberOfAnnualLeaveDays: number;
  numberOfPublicLeaveDays: number;
  numberOfDaysRequested: number;
  numberOfAnnualLeaveDaysRequested: number;
  numberOfPublicLeaveDaysRequested: number;
  numberOfDaysLeft: number;
  numberOfAnnualLeaveDaysLeft: number;
  numberOfPublicLeaveDaysLeft: number;
  numberOfDaysLeftForYear: number;
  numberOfAnnualLeaveDaysLeftForYear: number;
  numberOfPublicLeaveDaysLeftForYear: number;
  errorMessage: string;
}

interface Props {
  request: AnnualLeaveRequest;
  handleViewRequest: (annualLeaveRequest?: AnnualLeaveRequest) => void;
  handleEditRequest: (annualLeaveRequest?: AnnualLeaveRequest) => void;
  handleDeleteRequest: (annualLeaveRequest?: AnnualLeaveRequest) => void;
}

const TableRow = ({
  request,
  handleViewRequest,
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
          onClick={() => handleViewRequest(request)}
        >
          View
        </button>
      </td>
      <td>
        <button
          className="btn btn-primary"
          onClick={() => handleEditRequest(request)}
        >
          Edit
        </button>
      </td>
      <td>
        <button
          className="btn btn-primary"
          onClick={() => handleDeleteRequest(request)}
        >
          Delete
        </button>
      </td>
    </tr>
  );
};

export default TableRow;
