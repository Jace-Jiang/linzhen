using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.IO;

namespace XGhms.Web.Handles
{
    /// <summary>
    /// 学生提交作业专用的上传文件方法
    /// </summary>
    public class StudentUploadHandler : IHttpHandler, IRequiresSessionState
    {
        BLL.homework_student hwstuBll = new BLL.homework_student();
        public void ProcessRequest(HttpContext context)
        {
            DataTable dt = new Common.StuPageBase().GetLoginUserInfo();
            if (dt == null)
            {
                context.Response.Write("{\"msg\":\"未登录或已超时，请重新登录！\"}");
                return;
            }
            int userID=Convert.ToInt32(dt.Rows[0]["id"]);
            if (dt.Rows[0]["role_name"].ToString() != "Student")
            {
                context.Response.Write("{\"msg\":\"你没有权限导入，请联系管理员\"}");
                context.Response.End();
                return;
            }
            string[] StuHWinfo = (String[])context.Session["StuHWinfo"];
            if (StuHWinfo==null)
            {
                context.Response.Write("{\"msg\":\"Session值已过期，请刷新网页重试\"}");
                context.Response.End();
                return;
            }
            int hwStatus = hwstuBll.GethwStatusByStuandhwID(Convert.ToInt32(StuHWinfo[1]), userID);
            if (hwStatus==5)
            {
                context.Response.Write("{\"msg\":\"数据库没有找到该记录\"}");
                context.Response.End();
                return;
            }
            if (hwStatus==2)
            {
                context.Response.Write("{\"msg\":\"该作业老师正在审批，无法修改，请联系课程老师\"}");
                context.Response.End();
                return;
            }
            if (hwStatus == 4)
            {
                context.Response.Write("{\"msg\":\"该作业已经完成，请不要重复提交\"}");
                context.Response.End();
                return;
            }
            if (hwStatus==1||hwStatus==3)
            {
                //删除原来的作业
                string oldPath = hwstuBll.GetFilePathByStuandhwID(Convert.ToInt32(StuHWinfo[0]), userID);
                if (System.IO.File.Exists(context.Server.MapPath(oldPath)))//先判断文件是否存在，再执行操作 删除文件
                { 
                    System.IO.File.Delete(context.Server.MapPath(oldPath));
                    Directory.Delete(context.Server.MapPath(oldPath.Remove(oldPath.LastIndexOf("/") + 1)));  //删除文件夹
                }
            }
            //获取上传的文件的对象  
            HttpPostedFile imgFile = context.Request.Files["btnfile"];
            //定义允许上传的文件扩展名
            string[] strs = { "jpg", "rar", "zip", "7z", "doc", "docx", "xls", "xlsx", "pdf", "txt", "jpg", "png", "gif" };
            //最大文件大小
            int maxSize = 1000000;
            if (imgFile == null)
            {
                context.Response.Write("{\"msg\":\"请选择文件\"}");
                context.Response.End();
                return;
            }

            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                context.Response.Write("{\"msg\":\"文件太大\"}");
                context.Response.End();
                return;
            }
            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(strs, fileExt.Substring(1).ToLower()) == -1)
            {
                context.Response.Write("{\"msg\":\"上传文件扩展名是不允许的扩展名。只允许" + GetStringOfStrings(strs) + "格式。\"}");
                context.Response.End();
                return;
            }
            //获取上传文件的名称  
            string s = imgFile.FileName;
            //截取获得上传文件的名称(ie上传会把绝对路径也连带上，这里只得到文件的名称)  
            string str = s.Substring(s.LastIndexOf("\\") + 1);
            //Step 1 检查课程的路径是否存在，不存在则创建
            String savePath = "../Upload/Course/" + StuHWinfo[0].ToString() + "/";
            if (!Directory.Exists(context.Server.MapPath(savePath)))
            {
                Directory.CreateDirectory(context.Server.MapPath(savePath));
            }
            //Step 2 获取一个随机GUID文件夹，并且检查该文件夹是否存在，不存在则创建
            string savePathGUID = "../Upload/Course/" + StuHWinfo[0].ToString() + "/" + Guid.NewGuid() + "/";
            if (!Directory.Exists(context.Server.MapPath(savePathGUID)))
            {
                Directory.CreateDirectory(context.Server.MapPath(savePathGUID));
            }
            //Step 3 获取服务器的绝对路径并且上传保存文件
            string path = savePathGUID + str;
            string filePath = context.Server.MapPath(path);//获取服务器的绝对路径
            imgFile.SaveAs(context.Server.MapPath(path));//保存文件  
            //Step 4 修改数据库的信息并且输出信息
            int ri= hwstuBll.UploadFile(Convert.ToInt32(StuHWinfo[1]), userID, path);
            if (ri==1)
            {
                context.Response.Write("{\"msg\":\"上传作业成功！\"}");  
            }
            else
            {
                //删除文件
                if (System.IO.File.Exists(context.Server.MapPath(path)))//先判断文件是否存在，再执行操作
                { 
                    System.IO.File.Delete(context.Server.MapPath(path));  //删除文件
                    Directory.Delete(context.Server.MapPath(path.Remove(path.LastIndexOf("/") + 1)));  //删除文件夹
                }
                context.Response.Write("{\"msg\":\"上传作业失败！\"}");  
            }
        }

        #region 将字符串的数组转化为字符串
        public string GetStringOfStrings(string[] s)
        {
            string str="";
            foreach (string item in s)
            {
                str =str+ item + ",";
            }
            return str;
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