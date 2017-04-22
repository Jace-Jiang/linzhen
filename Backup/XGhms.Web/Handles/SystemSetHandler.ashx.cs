using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data;
using XGhms.Helper;

namespace XGhms.Web.Handles
{
    /// <summary>
    /// SystemSetHandler 的摘要说明
    /// </summary>
    public class SystemSetHandler : IHttpHandler, IRequiresSessionState
    {
        BLL.users usersBll = new BLL.users();
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
                case "ResetPasswordBySelf":
                    ResetPasswordBySelf(context);
                    break;
                default:
                    break;
            }
            #endregion
        }

        #region 修改密码
        protected void ResetPasswordBySelf(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string oldpwd = context.Request["oldpwd"];
            string newpwd = context.Request["newpwd"];
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取用户ID
            string userPwd = dt.Rows[0]["password"].ToString();
            string Password = Utils.SHA1Encrypt(oldpwd);
            if (userPwd!=Password)
            {
                context.Response.Write("2");
                context.Response.End();
                return;
            }
            else
            {
                int i = usersBll.UpdateUserPwd(userID, Utils.SHA1Encrypt(newpwd));
                if (i==1)
                {
                    context.Session.Clear();
                    Utils.WriteCookie("XGhms_cookie_username", "XGhms", -20000);
                    Utils.WriteCookie("XGhms_cookie_passward", "XGhms", -20000);
                    context.Response.Write(i.ToString());
                    context.Response.End();
                }
                else
                {
                    context.Response.Write("3");
                    context.Response.End();
                    return;
                }
                return;
            }
        }
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