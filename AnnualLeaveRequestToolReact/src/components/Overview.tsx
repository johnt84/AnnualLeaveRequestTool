import { useState } from "react";
import Table from "./Table";
import Details from "./Details";

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

const annualLeaveRequestsArray: AnnualLeaveRequest[] = [
  {
    recordNumber: 1,
    startDate: new Date(2025, 0, 1),
    returnDate: new Date(2025, 0, 2),
    numberOfDaysRequested: 1,
    numberOfAnnualLeaveDaysRequested: 0,
    numberOfPublicLeaveDaysRequested: 1,
    numberOfDaysLeft: 27,
    numberOfAnnualLeaveDaysLeft: 25,
    numberOfPublicLeaveDaysLeft: 2,
    paidLeaveType: "Paid",
    leaveType: "Annual Leave",
    notes: "New Years",
  },
  {
    recordNumber: 2,
    startDate: new Date(2025, 1, 9),
    returnDate: new Date(2025, 1, 10),
    numberOfDaysRequested: 1,
    numberOfAnnualLeaveDaysRequested: 1,
    numberOfPublicLeaveDaysRequested: 0,
    numberOfDaysLeft: 26,
    numberOfAnnualLeaveDaysLeft: 24,
    numberOfPublicLeaveDaysLeft: 2,
    paidLeaveType: "Paid",
    leaveType: "Annual Leave",
    notes: "Birthday",
  },
];

const Overview = () => {
  const [annualLeaveRequests, setAnnualLeaveRequests] = useState(
    annualLeaveRequestsArray
  );

  const [selectedRequest, selectRequest] = useState();

  const addAnnualLeaveRequest = () => {
    setAnnualLeaveRequests([
      ...annualLeaveRequests,
      {
        recordNumber: 3,
        startDate: new Date(2025, 6, 1),
        returnDate: new Date(2025, 6, 8),
        numberOfDaysRequested: 5,
        numberOfAnnualLeaveDaysRequested: 5,
        numberOfPublicLeaveDaysRequested: 0,
        numberOfDaysLeft: 21,
        numberOfAnnualLeaveDaysLeft: 19,
        numberOfPublicLeaveDaysLeft: 2,
        paidLeaveType: "Paid",
        leaveType: "Annual Leave",
        notes: "Summer Holiday",
      },
    ]);
  };

  return (
    <>
      {selectedRequest !== undefined ? (
        <Details request={selectedRequest} />
      ) : (
        <>
          <Table
            annualLeaveRequests={annualLeaveRequests}
            selectRequest={selectRequest}
            selectedRequest={selectedRequest}
          />
          <button className="btn btn-primary" onClick={addAnnualLeaveRequest}>
            Add
          </button>
        </>
      )}
    </>
  );
};

export default Overview;
