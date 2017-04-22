using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XGhms.Web.Admin.Message
{
    public partial class MessageInfo : Common.AdmPageBase
    {
        BLL.user_message umsgBll = new BLL.user_message();
        protected void Page_Load(object sender, EventArgs e)
        {
            string msgID = Request.QueryString["id"];
            if (msgID != null)
            {
                DataTable dt = (DataTable)Session["UserInfo"];
                if (umsgBll.IsThisMsgToReciver(Convert.ToInt32(msgID), Convert.ToInt32(dt.Rows[0]["id"])))
                {
                    umsgBll.updateMsgIsRead(Convert.ToInt32(msgID));
                }
            }
        }
    }
}