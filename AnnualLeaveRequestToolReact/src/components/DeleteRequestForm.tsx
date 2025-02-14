interface Props {
  request: AnnualLeaveRequest;
  handleDeleteRequest: (request?: AnnualLeaveRequest) => void;
  handleConfirmDeleteRequest: (request: AnnualLeaveRequest) => void;
}

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

const DeleteRequestForm = ({
  request,
  handleDeleteRequest,
  handleConfirmDeleteRequest,
}: Props) => {
  const handleConfirmDeleteClick = () => {
    if (request === undefined) return;

    handleConfirmDeleteRequest(request);

    handleDeleteRequest(undefined);
  };

  const handleCancelDeleteClick = () => {
    handleDeleteRequest(undefined);
  };

  return (
    <>
      <div>
        <label htmlFor="startDate">Start Date: </label>
        <label>{request.startDate.toString()}</label>
      </div>
      <div>
        <label htmlFor="returnDate">Return Date: </label>
        <label>{request.returnDate.toString()}</label>
      </div>
      <div>
        <label htmlFor="paidLeaveType">Paid Leave Type: </label>
        <label>{request.paidLeaveType}</label>
      </div>
      <div>
        <label htmlFor="leaveType">Leave Type: </label>
        <label>{request.leaveType}</label>
      </div>
      <div>
        <label htmlFor="notes">Notes: </label>
        <label>{request.notes}</label>
      </div>
      <div>
        <button onClick={handleConfirmDeleteClick}>Delete</button>
        <button onClick={handleCancelDeleteClick}>Cancel</button>
      </div>
    </>
  );
};

export default DeleteRequestForm;
