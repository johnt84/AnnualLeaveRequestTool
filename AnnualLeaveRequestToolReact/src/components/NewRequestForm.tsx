import { useState } from "react";

interface Props {
  handleAddRequest: (addingRequest: boolean) => void;
  handleSaveAddRequest: (newRequest: AnnualLeaveRequest) => void;
}

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

const NewRequestForm = ({ handleAddRequest, handleSaveAddRequest }: Props) => {
  const emptyRequest: AnnualLeaveRequest = {
    id: "",
    startDate: new Date(),
    returnDate: new Date(),
    numberOfDaysRequested: 0,
    numberOfAnnualLeaveDaysRequested: 0,
    numberOfPublicLeaveDaysRequested: 0,
    numberOfDaysLeft: 0,
    numberOfAnnualLeaveDaysLeft: 0,
    numberOfPublicLeaveDaysLeft: 0,
    paidLeaveType: "",
    leaveType: "",
    notes: "",
  };

  const [newRequest, setNewRequest] =
    useState<AnnualLeaveRequest>(emptyRequest);

  const handleSaveClick = () => {
    if (newRequest === undefined) return;

    calculateDates();

    handleSaveAddRequest(newRequest);
    setNewRequest(newRequest);

    handleAddRequest(false);
  };

  const handleCancelClick = () => {
    handleAddRequest(false);
  };

  const calculateDates = () => {
    let numberOfDays = getNumberOfDaysBetweenTwoDate(
      newRequest.startDate,
      newRequest.returnDate
    );

    setNewRequest({
      ...newRequest,
      numberOfDaysRequested: numberOfDays,
      numberOfAnnualLeaveDaysRequested: numberOfDays,
    });
  };

  function getNumberOfDaysBetweenTwoDate(fromDate: Date, toDate: Date) {
    let differenceInTime = toDate.getTime() - fromDate.getTime();

    return Math.round(differenceInTime / (1000 * 3600 * 24));
  }

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
