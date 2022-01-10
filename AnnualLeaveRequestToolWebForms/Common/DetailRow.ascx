<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailRow.ascx.cs" Inherits="AnnualLeaveRequestToolWebForms.Common.DetailRow" %>

<div class="row">
    <dl>
        <dt>
            Start Date
        </dt>

        <dd>
            <%= Model.StartDate.ToString("dd-MMM-yyyy") %>
        </dd>

        <dt>
            Return Date
        </dt>

        <dd>
            <%= Model.ReturnDate.ToString("dd-MMM-yyyy") %>
        </dd>

        <dt>
            Number of Days Requested
        </dt>

        <dd>
            <%= Model.NumberOfDaysRequested %>
        </dd>

        <dt>
            Number of Annual Leave Days Requested
        </dt>

        <dd>
            <%= Model.NumberOfAnnualLeaveDaysRequested %>
        </dd>

        <dt>
            Number of Public Leave Days Requested
        </dt>

        <dd>
            <%= Model.NumberOfPublicLeaveDaysRequested %>
        </dd>

        <dt>
            Paid Leave Type
        </dt>

        <dd>
            <%= Model.PaidLeaveType %>
        </dd>

        <dt>
            Leave Type
        </dt>

        <dd>
            <%= Model.LeaveType %>
        </dd>

        <dt>
            Notes
        </dt>

        <dd>
            <%= Model.Notes %>
        </dd>
    </dl>
</div>
