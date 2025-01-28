import DetailRow from "./DetailRow";

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

interface Props {
  request: AnnualLeaveRequest;
  handleViewRequest: (annualLeaveRequest?: AnnualLeaveRequest) => void;
}

const Details = ({ request, handleViewRequest }: Props) => {
  return <DetailRow request={request} handleViewRequest={handleViewRequest} />;
};

export default Details;
