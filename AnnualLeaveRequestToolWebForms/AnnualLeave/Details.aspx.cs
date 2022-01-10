using AnnualLeaveRequestToolWebForms.Data;
using AnnualLeaveRequestToolWebForms.Models;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace AnnualLeaveRequestToolWebForms.AnnualLeave
{
    public partial class Details : Page
    {
        protected AnnualLeaveRequestOverviewModel Model;
        protected bool EditableRequest;

        private IAnnualLeaveRequestLogic annualLeaveRequestLogic;

        protected bool IsError;

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

                Page.RegisterAsyncTask(new PageAsyncTask(() =>
                {
                    return PopulatePageAsync(annualLeaveRequestID);
                }));
            }
        }

        private async Task PopulatePageAsync(int annualLeaveRequestID)
        {
            try
            {
                Model = await annualLeaveRequestLogic.GetRequestAsync(annualLeaveRequestID);

                if (Model == null)
                {
                    return;
                }

                EditableRequest = Model.StartDate.Year >= DateTime.UtcNow.Year;
                DetailRow.Model = Model;
            }
            catch(Exception ex)
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