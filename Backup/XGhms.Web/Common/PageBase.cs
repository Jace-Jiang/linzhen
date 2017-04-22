using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XGhms.Helper;
using System.Data;

namespace XGhms.Web.Common
{
    public class PageBase : System.Web.UI.Page
    {
        public PageBase()
        {
            this.Load += new EventHandler(PageBase_Load);
        }

        private void PageBase_Load(object sender, EventArgs e)
        {
            //判断用户是否登录
            if (!IsAdminLogin())
            {
                Response.ContentType = "text/html";
                Response.Write("<html><head><title>跳转中...</title><script language='javascript'>window.location.replace('http://" + Request.Url.Authority + "/Login.aspx?ReturnUrl=" + Request.FilePath + "');</script></head><body></body></html>");
                Response.End();
            }
        }

        /// <summary>
        /// 判断管理员是否已经登录(解决Session超时问题)
        /// </summary>
        public bool IsAdminLogin()
        {
            //如果Session为Null
            if (Session["UserInfo"] != null)
            {
                return true;
            }
            else
            {
                //检查Cookies
                string username = Utils.GetCookie("XGhms_cookie_username", "XGhms");
                string userpwd = Utils.GetCookie("XGhms_cookie_passward", "XGhms");
                if (username != "" && userpwd != "")
                {
                    BLL.users usersBll = new BLL.users();
                    DataTable dtList = usersBll.GetUsersInfo(username);
                    if (dtList != null && dtList.Rows.Count != 0) //判断用户是否存在
                    {
                        if (dtList.Rows[0]["password"].ToString() == DESEncrypt.Decrypt(userpwd)) //判断密码是否正确
                        {
                            if (dtList.Rows[0]["is_lock"].ToString() == "0") //判断用户是否锁定
                            {
                                    Session.Add("UserInfo", dtList);
                                    Session.Timeout = 45; //设置Session的过期时间
                                    return true; //正常返回
                            }
                            else
                            {
                                return false; //用户被锁定，跳转到登录页面
                            }
                        }
                        else
                        {
                            return false; //密码错误，跳转到登录页面
                        }
                    }
                    else
                    {
                        return false; //用户不存在，跳转到登录页面
                    }
                }
                else
                {
                    return false; //用户不存在，跳转到登录页面
                }
            }
        }
        /// <summary>
        /// 获取登录用户的信息
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetLoginUserInfo()
        {
            if (IsAdminLogin())
            {
                return (DataTable)Session["UserInfo"];
            }
            else
            {
                return null;
            }
        }
    }
}