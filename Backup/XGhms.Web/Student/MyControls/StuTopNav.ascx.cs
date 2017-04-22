using System;
using System.Data;

namespace XGhms.Web.Student.MyControls
{
    public partial class StuTopNav : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["UserInfo"];
            lab_userName.Text = dt.Rows[0][1].ToString();
        }
    }
}