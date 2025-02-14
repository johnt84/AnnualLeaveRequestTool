import { useState } from "react";

interface Props {
  request: AnnualLeaveRequest;
  handleEditRequest: (request?: AnnualLeaveRequest) => void;
  handleSaveEditRequest: (request: AnnualLeaveRequest) => void;
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

const EditRequestForm = ({
  request,
  handleEditRequest,
  handleSaveEditRequest,
}: Props) => {
  const [editRequest, setEditRequest] = useState<AnnualLeaveRequest>(request);

  const handleSaveClick = () => {
    if (editRequest === undefined) return;

    handleSaveEditRequest(editRequest);

    handleEditRequest(undefined);
  };

  const handleCancelClick = () => {
    handleEditRequest(undefined);
  };

  return (
    <>
      <div>
        <label htmlFor="startDate">Start Date: </label>
        <input
          type="date"
          placeholder="Start Date"
          value={editRequest?.startDate.toString()}
          onChange={(e) =>
            setEditRequest({
              ...editRequest,
              startDate: new Date(e.target.value),
            })
          }
        />
        <label>{editRequest?.startDate.toString()}</label>
      </div>
      <div>
        <label htmlFor="returnDate">Return Date: </label>
        <input
          type="date"
          placeholder="Return Date"
          value={editRequest?.returnDate.toString()}
          onChange={(e) =>
            setEditRequest({
              ...editRequest,
              returnDate: new Date(e.target.value),
            })
          }
        />
        <label>{editRequest?.returnDate.toString()}</label>
      </div>
      <div>
        <label htmlFor="paidLeaveType">Paid Leave Type: </label>
        <input
          type="text"
          placeholder="Paid Leave Type"
          value={editRequest?.paidLeaveType}
          onChange={(e) =>
            setEditRequest({
              ...editRequest,
              paidLeaveType: e.target.value,
            })
          }
        />
      </div>
      <div>
        <label htmlFor="leaveType">Leave Type: </label>
        <input
          type="text"
          placeholder="Leave Type"
          value={editRequest?.leaveType}
          onChange={(e) =>
            setEditRequest({
              ...editRequest,
              leaveType: e.target.value,
            })
          }
        />
      </div>
      <div>
        <label htmlFor="notes">Notes: </label>
        <input
          type="text"
          placeholder="Notes"
          value={editRequest?.notes}
          onChange={(e) =>
            setEditRequest({
              ...editRequest,
              notes: e.target.value,
            })
          }
        />
      </div>
      <div>
        <button onClick={handleSaveClick}>Save</button>
        <button onClick={handleCancelClick}>Cancel</button>
      </div>
    </>
  );
};

export default EditRequestForm;
