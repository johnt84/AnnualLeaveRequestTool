import DetailRow from "./DetailRow";

interface Props {
  annualLeaveRequests: AnnualLeaveRequest[];
  recordClicked: number;
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

const Details = ({ annualLeaveRequests, recordClicked }: Props) => {
  return (
    <DetailRow
      annualLeaveRequests={annualLeaveRequests}
      recordClicked={recordClicked}
    />
  );
};

export default Details;
