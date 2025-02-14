import DetailRow from "./DetailRow";

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

const Details = ({ request, handleViewRequest }: Props) => {
  return <DetailRow request={request} handleViewRequest={handleViewRequest} />;
};

export default Details;
