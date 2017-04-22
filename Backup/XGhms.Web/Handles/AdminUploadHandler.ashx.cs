using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.IO;
using System.Data;
using XGhms.Helper;
using System.Web.SessionState;

namespace XGhms.Web.Handles
{
    /// <summary>
    /// 此处理过程主要用于用户上传文件，用于批量添加学生用户和批量添加班级学生
    /// </summary>
    public class AdminUploadHandler : IHttpHandler, IRequiresSessionState
    {
        BLL.users userBll = new BLL.users();
        BLL.college collegeBll = new BLL.college();
        BLL.classes classBll = new BLL.classes();
        BLL.users_info userinfoBll = new BLL.users_info();
        BLL.role roleBll = new BLL.role();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "HeadTeacher" || dt.Rows[0]["role_name"].ToString() == "Teacher" || dt.Rows[0]["role_name"].ToString() == "Student")
            {
                    context.Response.Write("{\"msg\":\"你没有权限导入，请联系管理员\"}");
                    context.Response.End();
                    return;
            }
            /*权限管理 End*/
            //获取上传的文件的对象  
            HttpPostedFile imgFile = context.Request.Files["btnfile"];
            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("file", "xls");
            //最大文件大小
            int maxSize = 1000000;
            if (imgFile == null)
            {
                context.Response.Write("{\"msg\":\"请选择文件\"}");
                context.Response.End();
            }

            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                context.Response.Write("{\"msg\":\"文件太大\"}");
                context.Response.End();
            }

            //获取上传文件的名称  
            string s = imgFile.FileName;
            //截取获得上传文件的名称(ie上传会把绝对路径也连带上，这里只得到文件的名称)  
            string str = s.Substring(s.LastIndexOf("\\") + 1);
            //给文件添加随机戳  
            string path = "/Upload/" + Guid.NewGuid() + str;
            string filePath = context.Server.MapPath(path);
            //保存文件  
            imgFile.SaveAs(context.Server.MapPath(path));
            string sreturn = AddUserClass(filePath, context);
            //删除文件
            if (System.IO.File.Exists(context.Server.MapPath(path)))//先判断文件是否存在，再执行操作
                System.IO.File.Delete(context.Server.MapPath(path));
            context.Response.Write(sreturn);  
        }

        protected string AddUserClass(string path, HttpContext context)
        {
            DataSet ds = ExcelHelp.importExcelToDataSet(path);
            int roleID = roleBll.GetRoleIDByRoleName("Student");
            int successNum = 0; //临时变量
            int errorNum = 0; //临时变量
            for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
            {
                //第一步，检查该用户是否存在
                if (userBll.Exists(ds.Tables[0].Rows[i][0].ToString().Trim()))  //如果存在
                {
                    //第二步，检查所在学院是否存在
                    if (collegeBll.Exists(ds.Tables[0].Rows[i][3].ToString().Trim())) //用户存在，学院不存在
                    {
                        //第三步，检查班级是否存在
                        if (classBll.Exists(ds.Tables[0].Rows[i][5].ToString().Trim())) //用户、学院、班级存在
                        {

                            int UserID = userBll.GetUserIDByUserNum(ds.Tables[0].Rows[i][0].ToString());
                            int collegeID = collegeBll.GetCollegeIDByCollegeName(ds.Tables[0].Rows[i][3].ToString());
                            int classID = classBll.GetClassIDByClassName(ds.Tables[0].Rows[i][5].ToString());
                            /*权限管理 Begin*/
                            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
                            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
                            {
                                string collegeUser = collegeBll.GetModel(collegeID).college_admin;
                                if (!collegeUser.Contains(dt.Rows[0]["id"].ToString()))
                                {
                                    return "{\"msg\":\"Excel表中的学院你没有权限修改\n请重新检查或联系超级管理员\"}";
                                }
                            }
                            /*权限管理 End*/
                            int sex;
                            if (ds.Tables[0].Rows[i][2].ToString().Trim()=="男")
                            {
                                sex = 1;
                            }
                            else
                            {
                                sex = 0;
                            }
                            int ri = userinfoBll.UpdateClassByUserID(UserID, ds.Tables[0].Rows[i][1].ToString().Trim(), sex, collegeID, classID, ds.Tables[0].Rows[i][4].ToString().Trim());
                            if (ri == 1)
                            {
                                successNum = successNum + 1;
                            }
                            else
                            {
                                errorNum = errorNum + 1;
                            }
                        }
                        else //用户、学院存在，班级不存在
                        {
                            return "{\"msg\":\"Excel表中的班级不存在，请重新检查\"}"; //班级不存在
                        }
                    }
                    else //用户存在，学院不存在
                    {
                        return "{\"msg\":\"Excel表中的学院不存在，请重新检查\"}"; //学院不存在
                    }
                }
                else  //如果该学号的用户不存在
                {
                    if (collegeBll.Exists(ds.Tables[0].Rows[i][3].ToString().Trim()))
                    {
                        //检查班级是否存在
                        if (classBll.Exists(ds.Tables[0].Rows[i][5].ToString().Trim())) //用户不存在、学院、班级存在
                        {
                            //新建用户，初始密码为123456
                            int collegeID = collegeBll.GetCollegeIDByCollegeName(ds.Tables[0].Rows[i][3].ToString().Trim());
                            int classID = classBll.GetClassIDByClassName(ds.Tables[0].Rows[i][5].ToString().Trim());
                            int newuserid = userBll.InsertUserReturnID(roleID, ds.Tables[0].Rows[i][0].ToString().Trim(), ds.Tables[0].Rows[i][0].ToString().Trim(), Utils.SHA1Encrypt("123456"), 0);
                            int sex;
                            if (ds.Tables[0].Rows[i][2].ToString().Trim() == "男")
                            {
                                sex = 1;
                            }
                            else
                            {
                                sex = 0;
                            }
                            int ri = userinfoBll.UpdateClassByUserID(newuserid, ds.Tables[0].Rows[i][1].ToString().Trim(), sex, collegeID, classID, ds.Tables[0].Rows[i][4].ToString().Trim());
                            if (ri == 1)
                            {
                                successNum = successNum + 1;
                            }
                            else
                            {
                                errorNum = errorNum + 1;
                            }
                        }
                        else //用户不存在、学院存在，班级不存在
                        {
                            return "{\"msg\":\"Excel表中的班级不存在，请重新检查\"}"; //班级不存在
                        }
                    }
                    else
                    {
                        return "{\"msg\":\"Excel表中的学院不存在，请重新检查\"}"; //学院不存在
                    }
                }
            }
            return "{\"msg\":\"成功" + successNum + "个，失败" + errorNum + "个\"}"; //最后成功的时候输出
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