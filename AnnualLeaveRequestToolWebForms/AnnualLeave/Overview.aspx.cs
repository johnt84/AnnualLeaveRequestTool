using AnnualLeaveRequestToolWebForms.Data;
using System;
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

            annualLeaveRequestLogic = new AnnualLeaveRequestLogic();

            if(!Page.IsPostBack)
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

                PopulateYearsDropdown();

                RefreshAnnualLeaveRequestsForYear();
            }
        }

        protected void ChangeSelectedYear(object sender, System.EventArgs e)
        {
            SelectedYear = Convert.ToInt32(ddlYears.SelectedValue);

            RefreshAnnualLeaveRequestsForYear();
        }

        private void RefreshAnnualLeaveRequestsForYear()
        {
            var annualLeaveRequestsForYear = annualLeaveRequestLogic.GetRequestsForYear(SelectedYear);

            rptAnnualLeaveRequestForYear.DataSource = annualLeaveRequestsForYear;
            rptAnnualLeaveRequestForYear.DataBind();

            EditableYearSelected = SelectedYear >= DateTime.UtcNow.Year;
        }

        private void PopulateYearsDropdown()
        {
            ddlYears.DataSource = annualLeaveRequestLogic.GetYears();
            ddlYears.DataBind();

            ddlYears.SelectedValue = SelectedYear.ToString();
        }
    }
}