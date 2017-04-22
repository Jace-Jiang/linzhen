using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using XGhms.Helper;

namespace XGhms.Web.Common
{
    public class StuPageBase : System.Web.UI.Page
    {

        public StuPageBase()
        {
            this.Load += new EventHandler(StuPageBase_Load);
        }
        private void StuPageBase_Load(object sender, EventArgs e)
        {
            if (IsStudentLogin()==0)//没有登录的用户，直接跳转到登录页面
            {
                Response.ContentType = "text/html";
                Response.Write("<html><head><title>跳转中...</title><script language='javascript'>window.location.replace('http://" + Request.Url.Authority + "/Login.aspx?ReturnUrl=" + Request.FilePath + "');</script></head><body></body></html>");
                Response.End();
            }
            else if (IsStudentLogin() == 2)
            {
                Response.ContentType = "text/html";
                Response.Write("<html><head><title>跳转中...</title><script language='javascript'>window.location.replace('http://" + Request.Url.Authority + "/Error.aspx?id=1');</script></head><body></body></html>");
                Response.End();
            }
        }
        //判断当前登录的用户
        public int IsStudentLogin()
        {
            if (Session["UserInfo"] != null) //如果Session不为Null
            {
                DataTable dt = (DataTable)Session["UserInfo"]; //获取Session的值
                if (dt.Rows[0][3].ToString() != "Student")  //如果角色不是Student
                {
                    return 2; //不是学生角色的用户跳转提示无法访问相应页面
                }
                else
                {
                    return 1; //整除返回
                }
            }
            else //如果Session为Null
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
                                if (dtList.Rows[0]["role_name"].ToString() == "Student")
                                {
                                    Session.Add("UserInfo", dtList);
                                    Session.Timeout = 45; //设置Session的过期时间
                                    return 1; //正常返回
                                }
                                else
                                {
                                    return 2; //不是学生角色的用户跳转提示无法访问相应页面
                                }
                            }
                            else
                            {
                                return 0; //用户被锁定，跳转到登录页面
                            }
                        }
                        else
                        {
                            return 0; //密码错误，跳转到登录页面
                        }
                    }
                    else
                    {
                        return 0; //用户不存在，跳转到登录页面
                    }
                }
                else
                {
                    return 0; //没有登录，跳转到登录页面
                }
            }
        }
        /// <summary>
        /// 获取登录用户的信息
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetLoginUserInfo()
        {
            if (IsStudentLogin() == 1)
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