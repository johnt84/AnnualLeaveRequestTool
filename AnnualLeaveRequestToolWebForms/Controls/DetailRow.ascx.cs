using AnnualLeaveRequestToolWebForms.Models;
using System;
using System.Web.UI;

namespace AnnualLeaveRequestToolWebForms.Controls
{
    public partial class DetailRow : UserControl
    {
        protected AnnualLeaveRequestOverviewModel Model;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void DisplayDetailRow(AnnualLeaveRequestOverviewModel model)
        {
            Model = model;
        }
    }
}