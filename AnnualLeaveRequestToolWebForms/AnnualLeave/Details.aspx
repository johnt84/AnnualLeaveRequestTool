<%@ Page Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="AnnualLeaveRequestToolWebForms.AnnualLeave.Details" %>

<%@ Register TagPrefix="uc" TagName="DetailRow" Src="~/Controls/DetailRow.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Details</h1>
    
    <% if (Model == null || Model.AnnualLeaveRequestID == 0)
        { %>
            <div style="color:red; padding-bottom: 20px">
                <ul>
                    <li>Cannot view this Annual Leave Request</li>
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
        <uc:DetailRow id="DetailRow" runat="server" />

        <div class="form-actions no-color esh-link-list">
            <% if (EditableRequest)
            {%>
                <a href="/AnnualLeave/Edit?AnnualLeaveRequestId=<%= Model.AnnualLeaveRequestID %>" class="esh-link-item">
                    Edit
                </a>
                <label>|</label>
            <% } %>

            <a href="/AnnualLeave/Overview?SelectedYear=<%= Model.Year %>" class="esh-link-item">
                Back to Overview
            </a>
        </div>
    </div>
</asp:Content>