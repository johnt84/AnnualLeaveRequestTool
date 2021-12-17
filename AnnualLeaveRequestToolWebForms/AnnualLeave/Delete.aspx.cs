using AnnualLeaveRequestToolWebForms.Data;
using AnnualLeaveRequestToolWebForms.Models;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace AnnualLeaveRequestToolWebForms.AnnualLeave
{
    public partial class Delete : Page
    {
        protected AnnualLeaveRequestOverviewModel Model;

        private IAnnualLeaveRequestLogic annualLeaveRequestLogic;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Delete";

            annualLeaveRequestLogic = GlobalSettings.Container.GetInstance<IAnnualLeaveRequestLogic>();

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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Page.RegisterAsyncTask(new PageAsyncTask(DeleteButtonClickAsync));
        }

        private async Task PopulatePageAsync(int annualLeaveRequestID)
        {
            Model = await annualLeaveRequestLogic.GetRequestAsync(annualLeaveRequestID);
        }

        private async Task DeleteButtonClickAsync()
        {
            await annualLeaveRequestLogic.DeleteAsync(Model);
            Response.Redirect($@"Overview?selectedyear={Model.Year}");
        }
    }
}