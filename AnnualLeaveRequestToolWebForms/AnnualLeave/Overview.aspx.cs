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
            var annualLeaveRequestsForYear = await annualLeaveRequestLogic.GetRequestsForYearAsync(SelectedYear);

            rptAnnualLeaveRequestForYear.DataSource = annualLeaveRequestsForYear;
            rptAnnualLeaveRequestForYear.DataBind();

            EditableYearSelected = SelectedYear >= DateTime.UtcNow.Year;
        }

        private async Task PopulateYearsDropdownAsync()
        {
            ddlYears.DataSource = await annualLeaveRequestLogic.GetYearsAsync();
            ddlYears.DataBind();

            ddlYears.SelectedValue = SelectedYear.ToString();
        }
    }
}