<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="AnnualLeaveRequestToolWebForms.AnnualLeave.Delete" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Delete Annual Leave Request</h1>
    
    <% if (Model == null || Model.AnnualLeaveRequestID == 0)
        { %>
            <div style="color:red; padding-bottom: 20px">
                <ul>
                    <li>Cannot delete this Annual Leave Request</li>
                </ul>
            </div>

            <div class="form-actions no-color esh-link-list" style="padding-top: 20px">
                <a href="/AnnualLeave/Overview" class="esh-link-item">
                    Back to Overview
                </a>
            </div>

    <%
        return;
    } %>

    <div class="container">
        <div class="row">
            <dl class="col-md-6 dl-horizontal">
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

        <div class="form-actions no-color esh-link-list">
            <asp:Button ID="btnDelete" Text="Delete" runat="server" class="btn esh-button esh-button-primary" OnClick="btnDelete_Click" />
            |
            <a href="/AnnualLeave/Overview?selectedYear=<%= Model.Year %>" class="esh-link-item">
                Back to Overview
            </a>
        </div>
    </div>
</asp:Content>
