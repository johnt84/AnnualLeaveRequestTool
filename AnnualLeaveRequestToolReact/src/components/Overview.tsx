import { useState } from "react";
import Table from "./Table";
import Details from "./Details";
import NewRequestForm from "./NewRequestForm";
import EditRequestForm from "./EditRequestForm";
import DeleteRequestForm from "./DeleteRequestForm";

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

  const [addRequest, setAddRequest] = useState<boolean>(false);

  const [editRequest, setEditRequest] = useState<AnnualLeaveRequest>();

  const [deleteRequest, setDeleteRequest] = useState<AnnualLeaveRequest>();

  const [viewRequest, setViewRequest] = useState<AnnualLeaveRequest>();

  const handleAddRequest = (addingRequest: boolean) => {
    setAddRequest(addingRequest);
  };

  const handleSaveAddRequest = (newRequest: AnnualLeaveRequest) => {
    let request = { ...newRequest, id: crypto.randomUUID() };

    setRequests([...requests, request]);
  };

  const handleViewRequest = (request?: AnnualLeaveRequest) => {
    setViewRequest(request);
  };

  const handleEditRequest = (request?: AnnualLeaveRequest) => {
    setEditRequest(request);
  };

  const handleSaveEditRequest = (editRequest: AnnualLeaveRequest) => {
    setRequests(
      requests.map((request) => {
        if (request.id === editRequest.id) {
          return {
            ...request,
            startDate: editRequest.startDate,
            returnDate: editRequest.returnDate,
            numberOfDaysRequested: editRequest.numberOfDaysRequested,
            numberOfAnnualLeaveDaysRequested:
              editRequest.numberOfAnnualLeaveDaysRequested,
            numberOfPublicLeaveDaysRequested:
              editRequest.numberOfPublicLeaveDaysRequested,
            numberOfDaysLeft: editRequest.numberOfDaysLeft,
            numberOfAnnualLeaveDaysLeft:
              editRequest.numberOfAnnualLeaveDaysLeft,
            numberOfPublicLeaveDaysLeft:
              editRequest.numberOfPublicLeaveDaysLeft,
            paidLeaveType: editRequest.paidLeaveType,
            leaveType: editRequest.leaveType,
            notes: editRequest.notes,
          };
        }
        return request;
      })
    );
  };

  const handleDeleteRequest = (request?: AnnualLeaveRequest) => {
    setDeleteRequest(request);
  };

  const handleConfirmDeleteRequest = (request: AnnualLeaveRequest) => {
    setRequests((currentRequests) => {
      return currentRequests.filter(
        (currentRequest) => currentRequest.id !== request.id
      );
    });
  };

  return (
    <>
      {addRequest === false &&
      viewRequest === undefined &&
      editRequest === undefined &&
      deleteRequest === undefined ? (
        <>
          <Table
            requests={requests}
            handleViewRequest={handleViewRequest}
            handleEditRequest={handleEditRequest}
            handleDeleteRequest={handleDeleteRequest}
          />
          <button
            className="btn btn-primary"
            onClick={() => handleAddRequest(true)}
          >
            Add
          </button>
        </>
      ) : (
        <>
          {addRequest == true && (
            <NewRequestForm
              handleAddRequest={handleAddRequest}
              handleSaveAddRequest={handleSaveAddRequest}
            />
          )}
          {viewRequest !== undefined && (
            <Details
              request={viewRequest}
              handleViewRequest={handleViewRequest}
            ></Details>
          )}
          {editRequest !== undefined && (
            <EditRequestForm
              request={editRequest}
              handleEditRequest={handleEditRequest}
              handleSaveEditRequest={handleSaveEditRequest}
            ></EditRequestForm>
          )}
          {deleteRequest !== undefined && (
            <DeleteRequestForm
              request={deleteRequest}
              handleDeleteRequest={handleDeleteRequest}
              handleConfirmDeleteRequest={handleConfirmDeleteRequest}
            ></DeleteRequestForm>
          )}
        </>
      )}
    </>
  );
};

export default Overview;
