using AnnualLeaveRequestToolWebForms.Data;
using AnnualLeaveRequestToolWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnnualLeaveRequestToolWebForms.AnnualLeave
{
    public partial class Edit : Page
    {
        private IAnnualLeaveRequestLogic annualLeaveRequestLogic;

        private List<string> PaidLeaveTypes = new List<string>()
                {
                    "Paid",
                    "Unpaid"
                }
                .OrderBy(x => x)
                .ToList();

        private List<string> LeaveTypes = new List<string>()
                {
                    "Annual Leave",
                    "Compassionate Leave",
                    "Appointment"
                }
                .OrderBy(x => x)
                .ToList();

        protected AnnualLeaveRequestOverviewModel Model;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Edit";

            annualLeaveRequestLogic = new AnnualLeaveRequestLogic();

            annualLeaveRequestLogic = new AnnualLeaveRequestLogic();

            if (string.IsNullOrEmpty(Request.QueryString["annualLeaveRequestID"]))
            {
                return;
            }

            int annualLeaveRequestID = 0;

            bool isValidAnnualLeaveRequestID = int.TryParse(Request.QueryString["annualLeaveRequestID"], out annualLeaveRequestID);

            if (!isValidAnnualLeaveRequestID)
            {
                return;
            }

            Model = annualLeaveRequestLogic.GetRequest(annualLeaveRequestID);

            if (!IsPostBack)
            {
                PopulateControls();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Model.StartDate = DateTime.Parse(txtStartDate.Text);
            Model.ReturnDate = DateTime.Parse(txtReturnDate.Text);
            Model.PaidLeaveType = ddlPaidLeaveType.SelectedValue;
            Model.LeaveType = ddlLeaveType.SelectedValue;
            Model.Notes = txtNotes.Text;

            var editAnnualLeaveRequest = annualLeaveRequestLogic.Update(Model);

            Response.Redirect($@"Overview?selectedyear={editAnnualLeaveRequest.Year}");
        }

        private void PopulateControls()
        {
            PopulateDropdowns();

            txtStartDate.Text = Model.StartDate.ToString("yyyy-MM-dd");
            txtReturnDate.Text = Model.ReturnDate.ToString("yyyy-MM-dd");
            ddlPaidLeaveType.SelectedValue = Model.PaidLeaveType;
            ddlLeaveType.SelectedValue = Model.LeaveType;
            txtNotes.Text = Model.Notes;
        }

        private void PopulateDropdowns()
        {
            PopulatePaidLeaveTypeDropdown();
            PopulateLeaveTypeDropdown();
        }

        private void PopulatePaidLeaveTypeDropdown()
        {
            ddlPaidLeaveType.DataSource = PaidLeaveTypes;
            ddlPaidLeaveType.DataBind();

            ddlPaidLeaveType.Items.Insert(0, new ListItem()
            {
                Text = "Please select",
            });
        }

        private void PopulateLeaveTypeDropdown()
        {
            ddlLeaveType.DataSource = LeaveTypes;
            ddlLeaveType.DataBind();

            ddlLeaveType.Items.Insert(0, new ListItem()
            {
                Text = "Please select",
            });
        }
    }
}