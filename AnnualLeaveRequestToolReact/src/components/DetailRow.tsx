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
}

const DetailRow = ({ request, handleViewRequest }: Props) => {
  const handleBackToMenuClick = () => {
    handleViewRequest(undefined);
  };

  return (
    <>
      <div className="row">
        <dl className="col-md-6 dl-horizontal">
          <dt>Start Date</dt>

          <dd>{moment(request.startDate).format("DD MMM yyyy")}</dd>

          <dt>Return Date</dt>

          <dd>{moment(request.returnDate).format("DD MMM yyyy")}</dd>

          <dt>Number of Days Requested</dt>

          <dd>{request.numberOfDaysRequested?.toString()}</dd>

          <dt>Number of Annual Leave Days Requested</dt>

          <dd>{request.numberOfAnnualLeaveDaysRequested?.toString()}</dd>

          <dt>Number of Public Leave Days Requested</dt>

          <dd>{request.numberOfPublicLeaveDaysRequested?.toString()}</dd>

          <dt>Paid Leave Type</dt>

          <dd>{request.paidLeaveType}</dd>

          <dt>Leave Type</dt>

          <dd>{request.leaveType}</dd>

          <dt>Notes</dt>

          <dd>{request.notes}</dd>
        </dl>
      </div>
      <div>
        <button onClick={handleBackToMenuClick}>Back</button>
      </div>
    </>
  );
};

export default DetailRow;
