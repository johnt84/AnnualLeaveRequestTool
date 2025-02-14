import { useState } from "react";

interface Props {
  handleAddRequest: (addingRequest: boolean) => void;
  handleSaveAddRequest: (newRequest: AnnualLeaveRequest) => void;
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

const NewRequestForm = ({ handleAddRequest, handleSaveAddRequest }: Props) => {
  const emptyRequest: AnnualLeaveRequest = {
    annualLeaveRequestId: 0,
    year: "2025",
    paidLeaveType: "",
    leaveType: "",
    startDate: new Date(),
    returnDate: new Date(),
    dateCreated: new Date(),
    notes: "",
    numberOfDays: 0,
    numberOfAnnualLeaveDays: 0,
    numberOfPublicLeaveDays: 0,
    numberOfDaysRequested: 0,
    numberOfAnnualLeaveDaysRequested: 0,
    numberOfPublicLeaveDaysRequested: 0,
    numberOfDaysLeft: 0,
    numberOfAnnualLeaveDaysLeft: 0,
    numberOfPublicLeaveDaysLeft: 0,
    numberOfDaysLeftForYear: 0,
    numberOfAnnualLeaveDaysLeftForYear: 0,
    numberOfPublicLeaveDaysLeftForYear: 0,
    errorMessage: "",
  };

  const [newRequest, setNewRequest] =
    useState<AnnualLeaveRequest>(emptyRequest);

  const handleSaveClick = () => {
    if (newRequest === undefined) return;

    handleSaveAddRequest(newRequest);
    setNewRequest(newRequest);

    handleAddRequest(false);
  };

  const handleCancelClick = () => {
    handleAddRequest(false);
  };

  return (
    <>
      <div>
        <label htmlFor="startDate">Start Date: </label>
        <input
          type="date"
          placeholder="Start Date"
          value={newRequest?.startDate.toString()}
          onChange={(e) =>
            setNewRequest({
              ...newRequest,
              startDate: new Date(e.target.value),
            })
          }
        />
        <label>{newRequest?.startDate.toString()}</label>
      </div>
      <div>
        <label htmlFor="returnDate">Return Date: </label>
        <input
          type="date"
          placeholder="Return Date"
          value={newRequest?.returnDate.toString()}
          onChange={(e) =>
            setNewRequest({
              ...newRequest,
              returnDate: new Date(e.target.value),
            })
          }
        />
        <label>{newRequest?.returnDate.toString()}</label>
      </div>
      <div>
        <label htmlFor="paidLeaveType">Paid Leave Type: </label>
        <input
          type="text"
          placeholder="Paid Leave Type"
          value={newRequest?.paidLeaveType}
          onChange={(e) =>
            setNewRequest({
              ...newRequest,
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
          value={newRequest?.leaveType}
          onChange={(e) =>
            setNewRequest({
              ...newRequest,
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
          value={newRequest?.notes}
          onChange={(e) =>
            setNewRequest({
              ...newRequest,
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

export default NewRequestForm;
