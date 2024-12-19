import { useState } from "react";
import Table from "./Table";
import Details from "./Details";

const Overview = () => {
  const [detailsVisible, setDetailsVisibility] = useState(false);
  const [recordClicked, setRecordClicked] = useState(0);

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

  const annualLeaveRequests: AnnualLeaveRequest[] = [
    {
      recordNumber: 0,
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
      recordNumber: 1,
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

  return (
    <>
      <Table
        annualLeaveRequests={annualLeaveRequests}
        detailsVisible={detailsVisible}
        recordClicked={recordClicked}
        setDetailsVisibility={setDetailsVisibility}
      />
      {detailsVisible && (
        <Details
          annualLeaveRequests={annualLeaveRequests}
          recordClicked={recordClicked}
        />
      )}
    </>
  );
};

export default Overview;
