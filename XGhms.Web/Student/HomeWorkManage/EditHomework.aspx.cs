using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XGhms.Web.Student.HomeWorkManage
{
    public partial class EditHomework : Common.StuPageBase
    {
        BLL.homework_student hwstuBll = new BLL.homework_student();
        BLL.course_homework courhwBll = new BLL.course_homework();
        protected void Page_Load(object sender, EventArgs e)
        {
            string msgID = Request.QueryString["id"]; //获取作业的ID
            if (msgID != null)
            {
                DataTable dt = (DataTable)Session["UserInfo"]; //获取Session的值
                int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取学生ID
                int hwID = Convert.ToInt32(msgID); //获取作业的ID,同时具有防止SQL注入的作用
                if (!hwstuBll.Exists("homework_id=" + hwID + " and student_id=" + userID))
                {
                    return;
                }
                else
                {
                    string[] scoure = (String[])Session["StuHWinfo"];
                    string[] strs = { courhwBll.GetModel(hwID).course_id.ToString(), hwID.ToString() };
                    if (scoure==null)
                    {
                        Session.Add("StuHWinfo", strs);
                    }
                    else
                    {
                        Session.Remove("StuHWinfo");
                        Session.Add("StuHWinfo", strs);
                    }
                }
            }
        }
    }
}