using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XGhms.Web
{
    public partial class Default : Common.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "Student")
            {
                Response.Redirect("~/Student/Default.aspx");
            }
            else if (dt.Rows[0]["role_name"].ToString() == "Teacher" || dt.Rows[0]["role_name"].ToString() == "HeadTeacher")
            {
                Response.Redirect("~/Teacher/Default.aspx");
            }
            else
            {
                Response.Redirect("~/Admin/Default.aspx");
            }
        }
    }
}