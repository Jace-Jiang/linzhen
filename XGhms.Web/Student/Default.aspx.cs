using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XGhms.Web.Student
{
    public partial class Default : Common.StuPageBase
    {
        Helper.XMLHelper xmlHelp = new Helper.XMLHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lab_MainTitle.Text = HttpUtility.UrlDecode(Helper.XMLHelper.GetNodeAttributesValue(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/Student", "title"));
                lab_MainDescription.Text = HttpUtility.UrlDecode(Helper.XMLHelper.GetNodeValue(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/Student"));
            }
        }
    }
}