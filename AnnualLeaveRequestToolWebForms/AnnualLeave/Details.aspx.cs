using AnnualLeaveRequestToolWebForms.Data;
using AnnualLeaveRequestToolWebForms.Models;
using System;
using System.Web.UI;

namespace AnnualLeaveRequestToolWebForms.AnnualLeave
{
    public partial class Details : Page
    {
        protected AnnualLeaveRequestOverviewModel Model;
        protected bool EditableRequest;

        private IAnnualLeaveRequestLogic annualLeaveRequestLogic;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Details";

            annualLeaveRequestLogic = GlobalSettings.Container.GetInstance<IAnnualLeaveRequestLogic>();

            if (!Page.IsPostBack)
            {
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
                EditableRequest = Model.StartDate.Year >= DateTime.UtcNow.Year;
            }
        }
    }
}