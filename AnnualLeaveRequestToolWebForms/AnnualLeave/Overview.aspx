<%@ Page Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Overview.aspx.cs" Inherits="AnnualLeaveRequestToolWebForms.AnnualLeave.Overview" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="/content/jquery.dataTables.min.css">

    <div>
        <div style="padding-bottom: 20px">
            <h1>Annual Leave Calendar - <%= SelectedYear %></h1>
        </div>

        <% if (EditableYearSelected)
            { %>
            <div class="form-actions no-color esh-link-list" style="padding-bottom: 20px">
                <a href="/AnnualLeave/Create" class="esh-link-item">
                    Create
                </a>
            </div>
        <% }
        %>

        <div style="padding-bottom: 20px">
            <asp:DropDownList ID="ddlYears" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChangeSelectedYear"></asp:DropDownList>
        </div>

        <asp:Repeater ID="rptAnnualLeaveRequestForYear" runat="server" ItemType="AnnualLeaveRequestToolWebForms.Models.AnnualLeaveRequestOverviewModel">
            <HeaderTemplate>
                <table id="datatableformat" class="table table-hover table-striped table-bordered no-wrap">
                    <thead>
                        <tr class="nowrap">
                            <th style="width: 120px;">Start Date</th>
                            <th style="width: 120px;">Return Date</th>
                            <th style="width: 165px;">Number of Days Requested</th>
                            <th style="width: 165px;">Number of Annual Leave Days Requested</th>
                            <th style="width: 165px;">Number of Public Leave Days Requested</th>
                            <th style="width: 165px;">Number of Days Left</th>
                            <th style="width: 165px;">Number of Annual Leave Days Left</th>
                            <th style="width: 165px;">Number of Public Holidays Left</th>
                            <th style="width: 150px;">Notes</th>
                            <th>&nbsp;</th>
                            <th>&nbsp;</th>
                            <th>&nbsp;</th>
                        </tr>
                    </thead>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="nowrap">
                    <td><%# Item.StartDate.ToString("dd MMM yyyy") %></td>
                    <td><%# Item.ReturnDate.ToString("dd MMM yyyy") %></td>
                    <td><%# Item.NumberOfDaysRequested %></td>
                    <td><%# Item.NumberOfAnnualLeaveDaysRequested %></td>
                    <td><%# Item.NumberOfPublicLeaveDaysRequested %></td>
                    <td><%# Item.NumberOfDaysLeft %></td>
                    <td><%# Item.NumberOfAnnualLeaveDaysLeft %></td>
                    <td><%# Item.NumberOfPublicLeaveDaysLeft %></td>
                    <td><%# Item.Notes %></td>

                    <td>
                        <% if (EditableYearSelected)
                            { %>
                            <a href="/AnnualLeave/Edit?annualLeaveRequestID=<%# Item.AnnualLeaveRequestID %>" class="esh-table-link">
                                Edit
                            </a>
                        <% }
                        %>
                    </td>
                    <td>
                        <a href="/AnnualLeave/Details?annualLeaveRequestID=<%# Item.AnnualLeaveRequestID %>" class="esh-table-link">
                            Details
                        </a> 
                    </td>
                    <td>
                        <% if (EditableYearSelected)
                            { %>
                            <a href="/AnnualLeave/Delete?annualLeaveRequestID=<%# Item.AnnualLeaveRequestID %>" class="esh-table-link">
                                Delete
                            </a>
                        <% }
                        %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>

    <script type="text/javascript" charset="utf8" src="/Scripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        $("#datatableformat").dataTable({
            "bPaginate": false,
            "ordering": true,
            "order": [[0, "asc"]]
        });
    </script>
</asp:Content>