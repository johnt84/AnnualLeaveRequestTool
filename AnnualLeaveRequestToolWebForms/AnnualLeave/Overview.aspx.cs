using AnnualLeaveRequestToolWebForms.Data;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace AnnualLeaveRequestToolWebForms.AnnualLeave
{
    public partial class Overview : Page
    {
        private IAnnualLeaveRequestLogic annualLeaveRequestLogic;

        protected bool EditableYearSelected;
        protected int SelectedYear;

        protected bool IsError;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Overview";

            annualLeaveRequestLogic = GlobalSettings.Container.GetInstance<IAnnualLeaveRequestLogic>();

            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["selectedyear"]))
                {
                    bool isValidSelectedYear = int.TryParse(Request.QueryString["selectedyear"], out SelectedYear);

                    if (!isValidSelectedYear)
                    {
                        SelectedYear = DateTime.UtcNow.Year;
                    }
                }
                else
                {
                    SelectedYear = DateTime.UtcNow.Year;
                }

                Page.RegisterAsyncTask(new PageAsyncTask(PopulatePageAsync));
            }
        }

        protected void ChangeSelectedYear(object sender, System.EventArgs e)
        {
            SelectedYear = Convert.ToInt32(ddlYears.SelectedValue);

            Page.RegisterAsyncTask(new PageAsyncTask(RefreshAnnualLeaveRequestsForYear));
        }

        private async Task PopulatePageAsync()
        {
            await PopulateYearsDropdownAsync();
            await RefreshAnnualLeaveRequestsForYear();
        }

        private async Task RefreshAnnualLeaveRequestsForYear()
        {
            try
            {
                var annualLeaveRequestsForYear = await annualLeaveRequestLogic.GetRequestsForYearAsync(SelectedYear);

                rptAnnualLeaveRequestForYear.DataSource = annualLeaveRequestsForYear;
                rptAnnualLeaveRequestForYear.DataBind();

                EditableYearSelected = SelectedYear >= DateTime.UtcNow.Year;
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex.Message);
            }
        }

        private async Task PopulateYearsDropdownAsync()
        {
            try
            {
                ddlYears.DataSource = await annualLeaveRequestLogic.GetYearsAsync();
                ddlYears.DataBind();

                ddlYears.SelectedValue = SelectedYear.ToString();
            }
            catch (Exception ex)
            {
                DisplayErrorMessage(ex.Message);
            }
        }

        private void DisplayErrorMessage(string errorMessage)
        {
            IsError = true;
            ErrorMessage.DisplayErrorMessage(errorMessage);
        }
    }
}