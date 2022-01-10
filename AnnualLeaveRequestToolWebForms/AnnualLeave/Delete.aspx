<%@ Page Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="AnnualLeaveRequestToolWebForms.AnnualLeave.Delete" %>

<%@ Register TagPrefix="uc" TagName="DetailRow" Src="~/Common/DetailRow.ascx" %>

<%@ Register TagPrefix="uc" TagName="ErrorMessage" Src="~/Common/ErrorMessage.ascx" %>

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

    <%
        if(IsError)
        { %>
            <uc:ErrorMessage id="ErrorMessage" runat="server" />   
        <% 
        }
    %>

    <div class="container">
        <uc:DetailRow id="DetailRow" runat="server" />

        <div class="form-actions no-color esh-link-list">
            <asp:Button ID="btnDelete" Text="Delete" runat="server" class="btn esh-button esh-button-primary" OnClick="btnDelete_Click" />
            |
            <a href="/AnnualLeave/Overview?selectedYear=<%= Model.Year %>" class="esh-link-item">
                Back to Overview
            </a>
        </div>
    </div>
</asp:Content>
