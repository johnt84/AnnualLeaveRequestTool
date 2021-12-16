using AnnualLeaveRequestToolWebForms.Data;
using AnnualLeaveRequestToolWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnnualLeaveRequestToolWebForms.AnnualLeave
{
    public partial class Create : Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Create";

            annualLeaveRequestLogic = new AnnualLeaveRequestLogic();

            if (!IsPostBack)
            {
                PopulateDropdowns();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var annualLeaveRequest = new AnnualLeaveRequestOverviewModel()
            {
                StartDate = DateTime.Parse(txtStartDate.Text),
                ReturnDate = DateTime.Parse(txtReturnDate.Text),
                PaidLeaveType = ddlPaidLeaveType.SelectedValue,
                LeaveType = ddlLeaveType.SelectedValue,
                Notes = txtNotes.Text,
            };

            var newAnnualLeaveRequest = annualLeaveRequestLogic.Create(annualLeaveRequest);

            Response.Redirect($@"Overview?selectedyear={newAnnualLeaveRequest.Year}");
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