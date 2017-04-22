using System;
using System.Web;
using System.Data;
using System.Collections;
using System.IO;
using XGhms.Helper;
using System.Web.SessionState;

namespace XGhms.Web.Handles
{
    /// <summary>
    /// 此处理过程主要用于用户上传文件，用于批量添加课程的学生用户
    /// </summary>
    public class AdminUploadForCourseHandler : IHttpHandler, IRequiresSessionState
    {
        BLL.role roleBll = new BLL.role();
        BLL.users userBll = new BLL.users();
        BLL.course courseBll = new BLL.course();
        BLL.college collegeBll = new BLL.college();
        BLL.course_student coustuBll = new BLL.course_student();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() != "CollegeAdmin")
            {
                if (dt.Rows[0]["role_name"].ToString() != "Admin")
                {
                    if (dt.Rows[0]["role_name"].ToString() != "Administrator")
                    {
                        context.Response.Write("{\"msg\":\"你没有权限导入，请联系管理员\"}");
                        context.Response.End();
                        return;
                    }
                }
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
            string sreturn = AddUserCourse(filePath, context);
            //删除文件
            if (System.IO.File.Exists(context.Server.MapPath(path)))//先判断文件是否存在，再执行操作
                System.IO.File.Delete(context.Server.MapPath(path));
            context.Response.Write(sreturn);  
        }

        protected string AddUserCourse(string path, HttpContext context)
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
                    //第二步，检查课程是否存在（根据课程ID查询）
                    if (courseBll.Exists(Convert.ToInt32(ds.Tables[0].Rows[i][2].ToString().Trim())))
                    {
                        int UserID = userBll.GetUserIDByUserNum(ds.Tables[0].Rows[i][0].ToString());
                        int courseID = Convert.ToInt32(ds.Tables[0].Rows[i][2].ToString().Trim());
                        /*权限管理 Begin*/
                        DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
                        if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
                        {
                            string collegeUser = collegeBll.GetModel(courseBll.GetModel(courseID).college_id).college_admin;
                            if (!collegeUser.Contains(dt.Rows[0]["id"].ToString()))
                            {
                                return "{\"msg\":\"Excel表中的课程你没有权限添加\n请重新检查或联系超级管理员\"}";
                            }
                        }
                        /*权限管理 End*/
                        if (coustuBll.Exists(courseID,UserID))
                        {
                            errorNum = errorNum + 1;
                        }
                        else
                        {
                            int ri = coustuBll.Insert(courseID, UserID);
                            if (ri == 1)
                            {
                                successNum = successNum + 1;
                            }
                            else
                            {
                                errorNum = errorNum + 1;
                            }
                        }
                    }
                    else //用户存在，课程不存在
                    {
                        return "{\"msg\":\"Excel表中的课程不存在，请重新检查\"}"; //学院不存在
                    }
                }
                else  //如果该学号的用户不存在
                {
                    errorNum = errorNum + 1;
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