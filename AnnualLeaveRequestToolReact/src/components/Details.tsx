import DetailRow from "./DetailRow";

interface Props {
  request: AnnualLeaveRequest;
}

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

const Details = ({ request }: Props) => {
  return <DetailRow request={request} />;
};

export default Details;
