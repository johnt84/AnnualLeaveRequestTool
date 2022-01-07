using AnnualLeaveRequestToolWebForms.Data;
using AnnualLeaveRequestToolWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        protected bool IsError;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Create";

            annualLeaveRequestLogic = GlobalSettings.Container.GetInstance<IAnnualLeaveRequestLogic>();

            if (!IsPostBack)
            {
                PopulateDropdowns();
                IsError = false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Page.RegisterAsyncTask(new PageAsyncTask(SubmitButtonClickAsync));
        }

        private async Task SubmitButtonClickAsync()
        {
            DateTime startDate = DateTime.Parse(txtStartDate.Text);

            var annualLeaveRequest = new AnnualLeaveRequestOverviewModel()
            {
                Year = startDate.Year,
                StartDate = startDate,
                ReturnDate = DateTime.Parse(txtReturnDate.Text),
                PaidLeaveType = ddlPaidLeaveType.SelectedValue,
                LeaveType = ddlLeaveType.SelectedValue,
                Notes = txtNotes.Text,
            };

            try
            {
                var newAnnualLeaveRequest = await annualLeaveRequestLogic.CreateAsync(annualLeaveRequest);

                Response.Redirect($@"Overview?selectedyear={newAnnualLeaveRequest.Year}");
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex.Message);
            }
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

        private void DisplayErrorMessage(string errorMessage)
        {
            IsError = true;
            ErrorMessage.DisplayErrorMessage(errorMessage);
        }
    }
}