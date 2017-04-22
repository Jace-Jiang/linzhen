// Author: 陈旭东
// Create date: 2015-4-11
// Description:	这个一般处理程序专门处理查询里面的方法
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.Text;

namespace XGhms.Web.Handles
{
    /// <summary>
    /// SearchHandler 的摘要说明
    /// </summary>
    public class SearchHandler : IHttpHandler, IRequiresSessionState
    {
        BLL.users_info userinfoBll = new BLL.users_info();
        BLL.college collegeBll = new BLL.college();
        BLL.users userBll = new BLL.users();
        BLL.role roleBll = new BLL.role();
        BLL.classes classBll = new BLL.classes();
        public void ProcessRequest(HttpContext context)
        {
            DataTable dt = new Common.PageBase().GetLoginUserInfo();
            if (dt == null)
            {
                context.Response.Write("未登录或已超时，请重新登录！");
                return;
            }
            //取得处事类型
            string action = HttpContext.Current.Request["action"];
            #region 根据不同的action执行不同的方法
            switch (action)
            {
                case "SearchUserList":
                    SearchUserList(context);
                    break;
                case "SearchUserInfo":
                    SearchUserInfo(context);
                    break;
                case "GetLoginUserInfo":
                    GetLoginUserInfo(context);
                    break;
                //case "GetTotalNum":
                //    GetHomeworkTotalNumBystuID(context);
                //    break;
                //case "GetSelectData":
                //    GetHomeworkBystuIDForpage(context);
                //    break;
                //case "GetStuScoreDefault":
                //    GetStuScoreDefault(context);
                //    break;
                //case "GetTwentyScoreByCourseID":
                //    GetTwentyScoreByCourseID(context);
                //    break;
                default:
                    break;
            }
            #endregion
        }

        #region 根据查询条件返回用户列表
        protected void SearchUserList(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string str = HttpContext.Current.Request["str"]; //获取作业的ID,同时具有防止SQL注入的作用
            //查询用户，根据教工号或者学号，还有姓名来查询（精确查询）
            DataTable dt= userinfoBll.ExistsUser(str);
            if (dt!=null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"user\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{\"id\":" + dt.Rows[i]["id"] + ",");
                    sb.Append("\"user_number\":\"" + dt.Rows[i]["user_number"] + "\",");
                    sb.Append("\"role_id\":" + dt.Rows[i]["role_id"] + ",");
                    sb.Append("\"real_name\":\"" + dt.Rows[i]["real_name"] + "\",");
                    sb.Append("\"college_name\":\"" + dt.Rows[i]["college_name"] + "\"},");
                }
                sb.Append("]}");
                context.Response.Write(sb.Remove((sb.Length - 3), 1));
            }
            else
            {
                context.Response.Write("false");
                context.Response.End();
            }
        }
        #endregion

        #region 根据用户ID来查询用户的详细信息
        protected void SearchUserInfo(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int userID = Convert.ToInt32(HttpContext.Current.Request["userID"]); //获取用户ID
            Model.users_info userinfoModel = userinfoBll.GetModelByUserID(userID);
            if (true)
            {
                
            }
            Model.college collegeModel = collegeBll.GetModel(userinfoModel.college_id);
            Model.role roleModel = roleBll.GetModel(userinfoModel.role_id);
            Model.classes classModel = classBll.GetModel(userinfoModel.class_id);
            string class_name;
            if (classModel == null)
            {
                class_name = "";
            }
            else
            {
                class_name = classModel.class_name;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{\"userinfo\":[");
            str.Append("{\"id\":" + userID + ",");
            str.Append("\"role_id\":" + userinfoModel.role_id + ",");
            str.Append("\"role_name\":\"" + roleModel.role_name + "\",");
            str.Append("\"real_name\":\"" + userinfoModel.real_name + "\",");
            str.Append("\"user_number\":\"" + userBll.GetUserNumByUserID(userID) + "\",");
            str.Append("\"class_name\":\"" + class_name + "\",");
            str.Append("\"sex\":" + userinfoModel.sex + ",");
            str.Append("\"birthday\":\"" + userinfoModel.birthday.ToString("yyyy年MM月dd号") + "\",");
            str.Append("\"telephone\":\"" + userinfoModel.telephone + "\",");
            str.Append("\"email\":\"" + userinfoModel.email + "\",");
            str.Append("\"college_id\":" + userinfoModel.college_id + ",");
            str.Append("\"college_name\":\"" + collegeModel.college_name + "\",");
            str.Append("\"major\":\"" + HttpUtility.UrlEncodeUnicode(userinfoModel.major).Replace("+", "%20") + "\",");
            str.Append("\"address\":\"" + HttpUtility.UrlEncodeUnicode(userinfoModel.address).Replace("+", "%20") + "\",");
            str.Append("\"explain\":\"" + HttpUtility.UrlEncodeUnicode(userinfoModel.explain).Replace("+", "%20") + "\"}");
            str.Append("]}");
            context.Response.Write(str.ToString());
        }
        #endregion

        #region 获取当前登录用户的信息
        protected void GetLoginUserInfo(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取用户ID
            Model.users_info userinfoModel = userinfoBll.GetModelByUserID(userID);
            Model.college collegeModel = collegeBll.GetModel(userinfoModel.college_id);
            Model.role roleModel = roleBll.GetModel(userinfoModel.role_id);
            Model.classes classModel = classBll.GetModel(userinfoModel.class_id);
            string class_name;
            if (classModel == null)
            {
                class_name = "";
            }
            else
            {
                class_name = classModel.class_name;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{\"userinfo\":[");
            str.Append("{\"id\":" + userID + ",");
            str.Append("\"role_id\":" + userinfoModel.role_id + ",");
            str.Append("\"role_name\":\"" + roleModel.role_name + "\",");
            str.Append("\"real_name\":\"" + userinfoModel.real_name + "\",");
            str.Append("\"user_number\":\"" + userBll.GetUserNumByUserID(userID) + "\",");
            str.Append("\"class_name\":\"" + class_name + "\",");
            str.Append("\"sex\":" + userinfoModel.sex + ",");
            str.Append("\"birthday\":\"" + userinfoModel.birthday.ToString("yyyy-MM-dd") + "\",");
            str.Append("\"telephone\":\"" + userinfoModel.telephone + "\",");
            str.Append("\"email\":\"" + userinfoModel.email + "\",");
            str.Append("\"college_id\":" + userinfoModel.college_id + ",");
            str.Append("\"college_name\":\"" + collegeModel.college_name + "\",");
            str.Append("\"major\":\"" + HttpUtility.UrlEncodeUnicode(userinfoModel.major).Replace("+", "%20") + "\",");
            str.Append("\"address\":\"" + HttpUtility.UrlEncodeUnicode(userinfoModel.address).Replace("+", "%20") + "\",");
            str.Append("\"explain\":\"" + HttpUtility.UrlEncodeUnicode(userinfoModel.explain).Replace("+", "%20") + "\"}");
            str.Append("]}");
            context.Response.Write(str.ToString());
        }
        #endregion

        #region CheckIsNULL
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}