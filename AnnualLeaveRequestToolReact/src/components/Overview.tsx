import { useEffect, useState } from "react";
import Table from "./Table";
import Details from "./Details";
import NewRequestForm from "./NewRequestForm";
import EditRequestForm from "./EditRequestForm";
import DeleteRequestForm from "./DeleteRequestForm";

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

const Overview = () => {
  const [requests, setRequests] = useState<AnnualLeaveRequest[]>([]);

  const [addRequest, setAddRequest] = useState<boolean>(false);

  const [editRequest, setEditRequest] = useState<AnnualLeaveRequest>();

  const [deleteRequest, setDeleteRequest] = useState<AnnualLeaveRequest>();

    const [viewRequest, setViewRequest] = useState<AnnualLeaveRequest>();

   const apiUrlPrefix = "";

   const apiUrlSuffix = "/api/AnnualLeaveRequest";

   const apiUrl = apiUrlPrefix + apiUrlSuffix;

  useEffect(() => {
    const fetchRequests = async () => {
      const requests = await fetchRequestsAsync();
      setRequests(requests);
    };
    fetchRequests();
  }, []);

  async function fetchRequestsAsync(): Promise<AnnualLeaveRequest[]> {
      const response = await fetch(apiUrl + "/GetRequestsForYear/2025");

    const data = await response.json();
    return data;
  }

  async function fetchRequestAync(
    request?: AnnualLeaveRequest
  ): Promise<AnnualLeaveRequest> {
    const response = await fetch(
        apiUrl + "/Get" + `/${request?.annualLeaveRequestId}`
    );

    const data = await response.json();
    return data;
  }

  const postRequestAsync = async (request: AnnualLeaveRequest) => {
      await fetch(apiUrl, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(request),
    });
  };

  const putRequestAsync = async (request: AnnualLeaveRequest) => {
      await fetch(apiUrl, {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(request),
    });
  };

  const deleteRequestAsync = async (request?: AnnualLeaveRequest) => {
      await fetch(apiUrl + `/${request?.annualLeaveRequestId}`, {
      method: "DELETE",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    });
  };

  const handleAddRequest = (addingRequest: boolean) => {
    setAddRequest(addingRequest);
  };

  const handleSaveAddRequest = (newRequest: AnnualLeaveRequest) => {
    postRequestAsync(newRequest);

    setRequests([...requests, newRequest]);
  };

  const handleViewRequest = (request?: AnnualLeaveRequest) => {
    fetchRequestAync(request);

    setViewRequest(request);
  };

  const handleEditRequest = (request?: AnnualLeaveRequest) => {
    setEditRequest(request);
  };

  const handleSaveEditRequest = (editRequest: AnnualLeaveRequest) => {
    putRequestAsync(editRequest);

    setRequests(
      requests.map((request) => {
        if (request.annualLeaveRequestId === editRequest.annualLeaveRequestId) {
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
    deleteRequestAsync(request);

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
