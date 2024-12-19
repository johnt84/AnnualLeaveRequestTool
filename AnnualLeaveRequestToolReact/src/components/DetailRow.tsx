import moment from "moment";

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

const DetailRow = ({ annualLeaveRequests, recordClicked }: Props) => {
  let model = annualLeaveRequests[recordClicked];
  return (
    <>
      <div className="row">
        <dl className="col-md-6 dl-horizontal">
          <dt>Start Date</dt>

          <dd>{moment(model.startDate).format("DD MMM yyyy")}</dd>

          <dt>Return Date</dt>

          <dd>{moment(model.returnDate).format("DD MMM yyyy")}</dd>

          <dt>Number of Days Requested</dt>

          <dd>{model.numberOfDaysRequested.toString()}</dd>

          <dt>Number of Annual Leave Days Requested</dt>

          <dd>{model.numberOfAnnualLeaveDaysRequested.toString()}</dd>

          <dt>Number of Public Leave Days Requested</dt>

          <dd>{model.numberOfPublicLeaveDaysRequested.toString()}</dd>

          <dt>Paid Leave Type</dt>

          <dd>{model.paidLeaveType}</dd>

          <dt>Leave Type</dt>

          <dd>{model.leaveType}</dd>

          <dt>Notes</dt>

          <dd>{model.notes}</dd>
        </dl>
      </div>
    </>
  );
};

export default DetailRow;
