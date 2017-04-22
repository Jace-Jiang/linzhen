using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XGhms.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected string aaa = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserInfo"]!=null)
            {
                DataTable dt = (DataTable)Session["UserInfo"];
                lab_userName.Text = dt.Rows[0][1].ToString();
                aaa = "1";
            }
        }
    }
}