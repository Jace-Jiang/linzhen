using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XGhms.Web.Admin.CollegeControls
{
    public partial class CollegeManage : Common.AdmPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*权限控制 Begin*/
            DataTable dt = (DataTable)Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                Response.ContentType = "text/html";
                Response.Write("<html><head><title>跳转中...</title><script language='javascript'>alert('抱歉，您没有权限访问该页面！');window.location.replace('CollegeList.aspx');</script></head><body></body></html>");
                return;
            }
            /*权限控制 End*/
        }
    }
}