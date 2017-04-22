// Author: 陈旭东
// Create date: 2015-3-20
// Description:	这个一般处理程序专门处理登录的方法
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using XGhms.Helper;
using System.Web.SessionState;

namespace XGhms.Web.Handles
{
    /// <summary>
    /// LoginHandler 的摘要说明
    /// </summary>
    public class LoginHandler : IHttpHandler, IRequiresSessionState
    {
        BLL.users usersBll = new BLL.users();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0);
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "");
            context.Response.CacheControl = "no-cache";
            string action = context.Request["action"];
            string name = context.Request["userName"];
            string pwd = context.Request["userPwd"];
            string code = context.Request["Code"];
            string check = context.Request["isChecked"];
            if (action == "login")
            {
                if (code.ToLower() == context.Session["verify_code"].ToString().ToLower())
                {
                    DataTable dtList = usersBll.GetUsersInfo(name);
                    if (dtList != null && dtList.Rows.Count != 0)    //检查用户是否存在
                    {
                        string userPwd = dtList.Rows[0][2].ToString();
                        string Password = Utils.SHA1Encrypt(pwd);
                        if (userPwd == Password)    //密码正确
                        {
                            if (dtList.Rows[0][5].ToString() == "0")
                            {
                                context.Session.Add("UserInfo", dtList);
                                context.Session.Timeout = 60; //设置Session的过期时间为一个小时
                                context.Response.Write("1");   //登陆成功，实现跳转
                                if (check.ToLower() == "true")
                                {
                                    Utils.WriteCookie("XGhms_cookie_username", "XGhms", name, 10080);
                                    Utils.WriteCookie("XGhms_cookie_passward", "XGhms", DESEncrypt.Encrypt(Password), 10080);
                                }
                                else {
                                    //防止Session提前过期
                                    Utils.WriteCookie("XGhms_cookie_username", "XGhms", name);
                                    Utils.WriteCookie("XGhms_cookie_passward", "XGhms", DESEncrypt.Encrypt(Password));
                                }
                                context.Response.End();
                            }
                            else
                            {
                                context.Response.Write("2");    //用户被锁定
                                context.Response.End();
                            }
                        }
                        else
                        {
                            context.Response.Write("3");    //密码错误
                            context.Response.End();
                        }
                    }
                    else
                    {
                        context.Response.Write("7");   //用户名不存在
                        context.Response.End();
                    }
                }
                else
                {
                    context.Response.Write("4");  //验证码错误
                    context.Response.End();
                }
            }
            if (action == "logout")  //点击退出系统时
            {
                context.Session.Clear();
                Utils.WriteCookie("XGhms_cookie_username", "XGhms", -20000);
                Utils.WriteCookie("XGhms_cookie_passward", "XGhms", -20000);
                context.Response.Write("5");  //跳转页面
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}