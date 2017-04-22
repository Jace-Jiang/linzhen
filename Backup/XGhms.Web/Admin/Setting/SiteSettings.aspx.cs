using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XGhms.Web.Admin.Setting
{
    public partial class SiteSettings : Common.AdmPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*权限控制Begin*/
            DataTable dt = (DataTable)Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() != "Administrator")
            {
                Response.ContentType = "text/html";
                Response.Write("<html><head><title>跳转中...</title><script language='javascript'>window.location.replace('http://" + Request.Url.Authority + "/Error.aspx?id=1');</script></head><body></body></html>");
                return;
            }
            /*权限控制End*/
            if (!IsPostBack)
            {
                stuTitle.Text= HttpUtility.UrlDecode(Helper.XMLHelper.GetNodeAttributesValue(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/Student", "title"));
                stuDescription.Text = HttpUtility.UrlDecode(Helper.XMLHelper.GetNodeValue(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/Student"));
                terTitle.Text = HttpUtility.UrlDecode(Helper.XMLHelper.GetNodeAttributesValue(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/Teacher", "title"));
                terDescription.Text = HttpUtility.UrlDecode(Helper.XMLHelper.GetNodeValue(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/Teacher"));
                admTitle.Text = HttpUtility.UrlDecode(Helper.XMLHelper.GetNodeAttributesValue(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/Admin", "title"));
                admDescription.Text = HttpUtility.UrlDecode(Helper.XMLHelper.GetNodeValue(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/Admin"));
                FooterInfo.Text = HttpUtility.UrlDecode(Helper.XMLHelper.GetNodeValue(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/footer"));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Helper.XMLHelper.SetNodeAttributesValue(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/Student", "title", HttpUtility.UrlEncode(stuTitle.Text));
            Helper.XMLHelper.UpdateNodeInnerText(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/Student", HttpUtility.UrlEncode(stuDescription.Text));
            Helper.XMLHelper.SetNodeAttributesValue(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/Teacher", "title", HttpUtility.UrlEncode(terTitle.Text));
            Helper.XMLHelper.UpdateNodeInnerText(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/Teacher", HttpUtility.UrlEncode(terDescription.Text));
            Helper.XMLHelper.SetNodeAttributesValue(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/Admin", "title", HttpUtility.UrlEncode(admTitle.Text));
            Helper.XMLHelper.UpdateNodeInnerText(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/Admin", HttpUtility.UrlEncode(admDescription.Text));
            Helper.XMLHelper.UpdateNodeInnerText(Request.PhysicalApplicationPath + "App_Data\\DefaultInfo.xml", "root/footer", HttpUtility.UrlEncode(FooterInfo.Text));
        }
    }
}