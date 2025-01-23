import { useState } from "react";
import Table from "./Table";
import Details from "./Details";

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

const Overview = () => {
  const [requests, setRequests] = useState<AnnualLeaveRequest[]>([
    {
      id: crypto.randomUUID(),
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
      id: crypto.randomUUID(),
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
  ]);

  const [selectedRequest, setSelectedRequest] = useState<AnnualLeaveRequest>();

  const handleAddRequest = () => {
    setRequests([
      ...requests,
      {
        id: crypto.randomUUID(),
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

  const handleEditRequest = (id: string) => {
    let editDays = 2;
    setRequests(
      requests.map((request) => {
        if (request.id === id) {
          return {
            ...request,
            numberOfDaysRequested: request.numberOfDaysRequested + editDays,
            numberOfAnnualLeaveDaysRequested:
              request.numberOfAnnualLeaveDaysRequested + editDays,
            numberOfDaysLeft: request.numberOfDaysLeft - editDays,
            numberOfAnnualLeaveDaysLeft:
              request.numberOfAnnualLeaveDaysLeft - editDays,
          };
        }
        return request;
      })
    );
  };

  const handleDeleteRequest = (id: string) => {
    setRequests((currentRequests) => {
      return currentRequests.filter((request) => request.id !== id);
    });
  };

  return (
    <>
      {selectedRequest !== undefined ? (
        <Details request={selectedRequest} />
      ) : (
        <>
          <Table
            requests={requests}
            setSelectedRequest={setSelectedRequest}
            handleEditRequest={handleEditRequest}
            handleDeleteRequest={handleDeleteRequest}
          />
          <button className="btn btn-primary" onClick={handleAddRequest}>
            Add
          </button>
        </>
      )}
    </>
  );
};

export default Overview;
