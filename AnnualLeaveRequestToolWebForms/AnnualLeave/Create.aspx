<%@ Page Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="AnnualLeaveRequestToolWebForms.AnnualLeave.Create" %>

<%@ Register TagPrefix="uc" TagName="ErrorMessage" Src="~/Common/ErrorMessage.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="/content/jquery-ui.css" />
    
    <h1>Create Annual Leave Request</h1>

    <%
    if(IsError)
    { %>
        <uc:ErrorMessage id="ErrorMessage" runat="server" />   
    <% 
    }
    %>
    
    <div class="form-group">
        Start Date:
        <asp:TextBox ID="txtStartDate" ClientIDMode="Static" runat="server"></asp:TextBox>
    </div>

    <div class="form-group">    
        Return Date:
        <asp:TextBox ID="txtReturnDate" ClientIDMode="Static" runat="server"></asp:TextBox>
    </div>

    <div class="form-group">
        Leave to be Paid/Unpaid:
        <asp:DropDownList ID="ddlPaidLeaveType" runat="server"></asp:DropDownList>
    </div>

    <div class="form-group">
        Leave Type:
        <asp:DropDownList ID="ddlLeaveType" runat="server"></asp:DropDownList>
    </div>

    <div class="form-group">
        Notes:
        <asp:TextBox ID="txtNotes" runat="server"></asp:TextBox>
    </div>

    <div>
        <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click" />
    </div>

    <div class="form-actions no-color esh-link-list" style="padding-top: 20px">
        <a href="/AnnualLeave/Overview" class="esh-link-item">
            Back to Overview
        </a>
    </div>
 
    <script type="text/javascript" charset="utf8" src="/Scripts/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" charset="utf8" src="/Scripts/jquery-ui.js"></script>
    <script>  
        $(function ()  
        {  
            $('#txtStartDate').datepicker(
            {  
                dateFormat: 'dd/mm/yy',  
                changeMonth: true,  
                changeYear: true,  
                yearRange: '2020:2100'
            });

            $('#txtReturnDate').datepicker(
            {
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                yearRange: '2020:2100'
            });
        })  
    </script>  
</asp:Content>