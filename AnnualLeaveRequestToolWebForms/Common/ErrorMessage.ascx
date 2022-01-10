<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ErrorMessage.ascx.cs" Inherits="AnnualLeaveRequestToolWebForms.Common.ErrorMessage" %>

<div style="color:red; font-size: 18px;" padding-bottom: 20px">
    <% 
    if (ErrorMessages?.Count > 0)
    {
    %>
        <ul>
            <%
            foreach (var errorMessage in ErrorMessages)
            {%>
                <li><%: errorMessage %></li>
            <%}
            %>
        </ul>
    <%
    }
    %>
</div>
