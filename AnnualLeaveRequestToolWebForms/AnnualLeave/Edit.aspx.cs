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
    public partial class Edit : Page
    {
        private IAnnualLeaveRequestLogic annualLeaveRequestLogic;

        private IErrorHandler errorHandler;

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

        protected bool IsError;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Edit";

            annualLeaveRequestLogic = GlobalSettings.Container.GetInstance<IAnnualLeaveRequestLogic>();
            errorHandler = GlobalSettings.Container.GetInstance<IErrorHandler>();

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

            Page.RegisterAsyncTask(new PageAsyncTask(() =>
            {
                return PopulatePageAsync(annualLeaveRequestID);
            }));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Page.RegisterAsyncTask(new PageAsyncTask(SubmitButtonClickAsync));
        }

        private async Task PopulatePageAsync(int annualLeaveRequestID)
        {
            try
            {
                Model = await annualLeaveRequestLogic.GetRequestAsync(annualLeaveRequestID);

                if(Model == null)
                {
                    return;
                }

                if (!IsPostBack)
                {
                    PopulateControls();
                }
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex.Message);
            }
        }

        private async Task SubmitButtonClickAsync()
        {
            var annualLeaveRequestInput = new AnnualLeaveRequestInput()
            {
                StartDate = txtStartDate.Text,
                ReturnDate = txtReturnDate.Text,
                PaidLeaveType = ddlPaidLeaveType.SelectedValue,
                LeaveType = ddlLeaveType.SelectedValue,
            };

            var errors = errorHandler.CheckAnnualLeaveRequestEntryIsValid(annualLeaveRequestInput);

            if (errors != null && errors.Count > 0)
            {
                DisplayErrorMessages(errors);
                return;
            }

            DateTime startDate = DateTime.Parse(txtStartDate.Text);

            Model.Year = startDate.Year;
            Model.StartDate = DateTime.Parse(txtStartDate.Text);
            Model.ReturnDate = DateTime.Parse(txtReturnDate.Text);
            Model.PaidLeaveType = ddlPaidLeaveType.SelectedValue;
            Model.LeaveType = ddlLeaveType.SelectedValue;
            Model.Notes = txtNotes.Text;

            try
            {
                var editAnnualLeaveRequest = await annualLeaveRequestLogic.UpdateAsync(Model);

                Response.Redirect($@"Overview?selectedyear={editAnnualLeaveRequest.Year}");
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex.Message);
            }
        }

        private void PopulateControls()
        {
            PopulateDropdowns();

            txtStartDate.Text = Model.StartDate.ToString("dd/MM/yyyy");
            txtReturnDate.Text = Model.ReturnDate.ToString("dd/MM/yyyy");
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

        private void DisplayErrorMessage(string errorMessage)
        {
            IsError = true;
            ErrorMessage.DisplayErrorMessage(errorMessage);
        }

        private void DisplayErrorMessages(List<string> errorMessages)
        {
            IsError = true;
            ErrorMessage.DisplayErrorMessages(errorMessages);
        }
    }
}