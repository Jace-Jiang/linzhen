using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XGhms.Web.Student.MyControls
{
    public partial class StuDownFooter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lb_site_footer.Text = HttpUtility.UrlDecode(Helper.XMLHelper.GetNodeValue(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/footer"));
        }
    }
}