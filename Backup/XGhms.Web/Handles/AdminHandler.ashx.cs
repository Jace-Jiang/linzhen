// Author: 陈旭东
// Create date: 2015-4-7
// Description:	这个一般处理程序专门处理管理员的事件方法
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Collections;
using System.Globalization;
using System.IO;
using LitJson;
using XGhms.Helper;
using System.Data.SqlClient;
using System.Web.SessionState;

namespace XGhms.Web.Handles
{
    /// <summary>
    /// AdminHandler 的摘要说明
    /// </summary>
    public class AdminHandler : IHttpHandler, IRequiresSessionState
    {
        BLL.role roleBll = new BLL.role();
        BLL.users userBll = new BLL.users();
        BLL.users_info userinfoBll = new BLL.users_info();
        BLL.college collegeBll = new BLL.college();
        BLL.classes classBll = new BLL.classes();
        BLL.term termBll = new BLL.term();
        BLL.course courseBll = new BLL.course();
        BLL.course_student courstuBll = new BLL.course_student();
        public void ProcessRequest(HttpContext context)
        {
            DataTable dt = new Common.AdmPageBase().GetLoginUserInfo();
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
                case "SelectCollegeAdminAll":
                    SelectCollegeAdminAll(context);
                    break;
                case "GetCollegeInfoByID":
                    GetCollegeInfoByID(context);
                    break;
                case "AddNewCollege":
                    AddNewCollege(context);
                    break;
                case "UpdateOldCollege":
                    UpdateOldCollege(context);
                    break;
                case "DeleteCollegeByID":
                    DeleteCollegeByID(context);
                    break;
                case "GetCollegeList":
                    GetCollegeList(context);
                    break;
                case "GetAllClassAdmin":
                    GetAllClassAdmin(context);
                    break;
                case "GetClassInfoByID":
                    GetClassInfoByID(context);
                    break;
                case "AddNewClass":
                    AddNewClass(context);
                    break;
                case "UpdateOldClass":
                    UpdateOldClass(context);
                    break;
                case "UpdateUserClassByUserID":
                    UpdateUserClassByUserID(context);
                    break;
                case "DeleteStuFromClass":
                    DeleteStuFromClass(context);
                    break;
                case "CreateExcelByClassID":
                    CreateExcelByClassID(context);
                    break;
                case "DeleteClassByID":
                    DeleteClassByID(context);
                    break;
                case "GetAllClassByCollegeID":
                    GetAllClassByCollegeID(context);
                    break;
                case "GetAllTermForNow":
                    GetAllTermForNow(context);
                    break;
                case "CheckUserIDExist":
                    CheckUserIDExist(context);
                    break;
                case "GetCourseInfoByID":
                    GetCourseInfoByID(context);
                    break;
                case "UpdateOldCourse":
                    UpdateOldCourse(context);
                    break;
                case "AddNewCourse":
                    AddNewCourse(context);
                    break;
                case "GetNumTotalByCourseList":
                    GetNumTotalByCourseList(context);
                    break;
                case "DeleteCourseByID":
                    DeleteCourseByID(context);
                    break;
                case "setCourselistForPage":
                    setCourselistForPage(context);
                    break;
                case "AddNewUserByAdmin":
                    AddNewUserByAdmin(context);
                    break;
                case "GetUserInfoByID":
                    GetUserInfoByID(context);
                    break;
                case "UpdateOldUserByAdmin":
                    UpdateOldUserByAdmin(context);
                    break;
                case "ResetPwdFromAdmin":
                    ResetPwdFromAdmin(context);
                    break;
                case "SelectUserListTotalBySelter":
                    SelectUserListTotalBySelter(context);
                    break;
                case "SelectUserListForAdmin":
                    SelectUserListForAdmin(context);
                    break;
                case "DeleteUserByID":
                    DeleteUserByID(context);
                    break;
                case "UpdateUserCourseByUserID":
                    UpdateUserCourseByUserID(context);
                    break;
                case "DeleteStuFromCourse":
                    DeleteStuFromCourse(context);
                    break;
                case "CreateExcelByCourseID":
                    CreateExcelByCourseID(context);
                    break;
                case "GetClassAdminByCollegeID":
                    GetClassAdminByCollegeID(context);
                    break;
                default:
                    break;
            }
            #endregion
        }

        #region 新建添加学院
        protected void AddNewCollege(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                context.Response.Write("{\"msg\":\"你没有删除学院的权限！\"}");
                context.Response.End();
                return;
            }
            /*权限管理 End*/
            string collegeName = HttpContext.Current.Request["collegeName"]; //获取学院名称
            string collegeUsers = HttpContext.Current.Request["userName"]; //获取学院用户列表
            int i = collegeBll.InsertNewCollege(collegeName, collegeUsers);
            if (i == 1)
            {
                context.Response.Write("{\"msg\":\"新建成功！\"}");
            }
            else
            {
                context.Response.Write("{\"msg\":\"新建失败！\"}");
            }
        }
        #endregion

        #region 更新学院信息
        protected void UpdateOldCollege(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                context.Response.Write("{\"msg\":\"你没有删除学院的权限！\"}");
                context.Response.End();
                return;
            }
            /*权限管理 End*/
            string collegeName = HttpContext.Current.Request["collegeName"]; //获取学院名称
            string collegeUsers = HttpContext.Current.Request["userName"]; //获取学院用户列表
            int collegeID = Convert.ToInt32(HttpContext.Current.Request["collegeID"]); //获取学院ID
            int i = collegeBll.UpdateCollegeByID(collegeName, collegeUsers, collegeID);
            if (i == 1)
            {
                context.Response.Write("{\"msg\":\"更新成功！\"}");
            }
            else
            {
                context.Response.Write("{\"msg\":\"更新失败！\"}");
            }
        }
        #endregion

        #region 根据ID来删除学院
        protected void DeleteCollegeByID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int collegeID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取学院ID
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                    context.Response.Write("{\"msg\":\"你没有删除学院的权限！\"}");
                    context.Response.End();
                    return;
            }
            /*权限管理 End*/
            if (collegeBll.Delete(collegeID))
            {
                context.Response.Write("{\"msg\":\"删除成功！\"}");
            }
            else
            {
                context.Response.Write("{\"msg\":\"删除失败！\"}");
            }
        }
        #endregion

        #region 获取所有学院管理员
        protected void SelectCollegeAdminAll(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dt = userBll.GetUserIDandNumByRoleID(roleBll.GetRoleIDByRoleName("CollegeAdmin"));
            StringBuilder str = new StringBuilder();
            str.Append("{\"userList\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str.Append("{\"id\":" + dt.Rows[i]["id"] + ",");
                str.Append("\"value\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(dt.Rows[i]["id"])) + "\"},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 根据学院部门的ID获取学院的信息
        protected void GetCollegeInfoByID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int collegeID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取学院ID
            Model.college collegeModel = collegeBll.GetModel(collegeID);
            StringBuilder str = new StringBuilder();
            str.Append("{\"collegeName\":\"" + collegeModel.college_name + "\",");
            str.Append("\"userList\":[");
            if (collegeModel.college_admin == null)
            {
                str.Append("]}");
                context.Response.Write(str.ToString());
                context.Response.End();
            }
            else
            {
                string[] s = collegeModel.college_admin.Split(new char[] { ',' });
                for (int i = 0; i < s.Length - 1; i++)
                {
                    str.Append("{\"id\":" + s[i] + ",");
                    str.Append("\"value\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(s[i])) + "\"},");
                }
                str.Append("]}");
                context.Response.Write(str.Remove((str.Length - 3), 1));
                context.Response.End();
            }
        }
        #endregion

        #region 获取所有的学院列表
        protected void GetCollegeList(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            IEnumerable<Model.college> modelList = collegeBll.GetAllCollegeList();
            StringBuilder str = new StringBuilder();
            str.Append("{\"collegeList\":[");
            foreach (Model.college model in modelList)
            {
                str.Append("{\"id\":" + model.id + ",");
                str.Append("\"collegeName\":\"" + model.college_name + "\",");
                str.Append("\"collegeAdmin\":\"");
                if (model.college_admin == null)
                {
                    str.Append("");
                }
                else
                {
                    string[] s = model.college_admin.Split(new char[] { ',' });
                    for (int i = 0; i < s.Length - 1; i++)
                    {
                        if (i == s.Length - 2)
                        {
                            str.Append(userinfoBll.GetUserRealNameForID(Convert.ToInt32(s[i])));
                        }
                        else
                        {
                            str.Append(userinfoBll.GetUserRealNameForID(Convert.ToInt32(s[i])) + ", ");
                        }
                    }
                }
                str.Append("\"},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 获取所有的辅导员/班主任
        protected void GetAllClassAdmin(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dt = userBll.GetUserIDandNumByRoleID(roleBll.GetRoleIDByRoleName("HeadTeacher"));
            StringBuilder str = new StringBuilder();
            str.Append("{\"userList\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str.Append("{\"id\":" + dt.Rows[i]["id"] + ",");
                str.Append("\"value\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(dt.Rows[i]["id"])) + "\"},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 根据学院ID获取该学院下面的所有辅导员角色
        protected void GetClassAdminByCollegeID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int collegeID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取学院ID
            DataTable dt = userBll.GetHeadTeacherListByCollegeID(roleBll.GetRoleIDByRoleName("HeadTeacher"), collegeID);
            if (dt.Rows.Count==0)
            {
                context.Response.Write("{\"userList\":[]}");
                context.Response.End();
            }
            else
            {
                StringBuilder str = new StringBuilder();
                str.Append("{\"userList\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str.Append("{\"id\":" + dt.Rows[i]["id"] + ",");
                    str.Append("\"value\":\"" + dt.Rows[i]["real_name"] + "\"},");
                }
                str.Append("]}");
                context.Response.Write(str.Remove((str.Length - 3), 1));
            }
        }
        #endregion

        #region 根据班级的ID获取班级的信息
        protected void GetClassInfoByID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int classID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取班级ID
            IEnumerable<Model.college> modelList = collegeBll.GetAllCollegeList();
            Model.classes classModel = classBll.GetModel(classID);
            StringBuilder str = new StringBuilder();
            str.Append("{\"className\":\"" + classModel.class_name + "\",");
            str.Append("\"collegeID\":" + classModel.college_id + ",");
            str.Append("\"adminID\":" + classModel.head_teacher);
            str.Append("}");
            context.Response.Write(str.ToString());
        }
        #endregion

        #region 新建添加班级
        protected void AddNewClass(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string className = HttpContext.Current.Request["className"]; //获取班级名称
            int collegeID = Convert.ToInt32(HttpContext.Current.Request["collegeID"]); //获取学院ID
            int HeadTeacher = Convert.ToInt32(HttpContext.Current.Request["HeadTeacher"]); //获取辅导员ID
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                string collegeUser = collegeBll.GetModel(collegeID).college_admin;
                if (!collegeUser.Contains(dt.Rows[0]["id"].ToString()))
                {
                    context.Response.Write("{\"msg\":\"你没有权限新建此学院的班级！\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限管理 End*/
            int i = classBll.InsertNewClass(className, collegeID, HeadTeacher);
            if (i == 1)
            {
                context.Response.Write("{\"msg\":\"新建成功\"}");
            }
            else
            {
                context.Response.Write("{\"msg\":\"新建失败\"}");
            }
        }
        #endregion

        #region 更新班级信息
        protected void UpdateOldClass(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string className = HttpContext.Current.Request["className"]; //获取班级名称
            int collegeID = Convert.ToInt32(HttpContext.Current.Request["collegeID"]); //获取学院ID
            int HeadTeacher = Convert.ToInt32(HttpContext.Current.Request["HeadTeacher"]); //获取辅导员ID
            int classID = Convert.ToInt32(HttpContext.Current.Request["classID"]); //获取班级ID
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                string collegeUser = collegeBll.GetModel(collegeID).college_admin; 
                if (!collegeUser.Contains(dt.Rows[0]["id"].ToString()))
                {
                    context.Response.Write("{\"msg\":\"你没有权限选择此学院！\"}");
                    context.Response.End();
                    return;
                }
                int college2ID = classBll.GetModel(classID).college_id;
                string collegeUser2 = collegeBll.GetModel(college2ID).college_admin;
                if (!collegeUser2.Contains(dt.Rows[0]["id"].ToString()))
                {
                    context.Response.Write("{\"msg\":\"你没有权限修改此班级！\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限管理 End*/
            int i = classBll.UpdateClassByID(className, collegeID, HeadTeacher, classID);
            if (i == 1)
            {
                context.Response.Write("{\"msg\":\"修改成功！\"}");
            }
            else
            {
                context.Response.Write("{\"msg\":\"修改失败！\"}");
            }
        }
        #endregion

        #region 更新添加学生到某个班级
        protected void UpdateUserClassByUserID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string userNum = HttpContext.Current.Request["userNum"]; //获取学生学号
            int classID = Convert.ToInt32(HttpContext.Current.Request["classID"]); //获取班级ID
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                int college2ID = classBll.GetModel(classID).college_id;
                string collegeUser2 = collegeBll.GetModel(college2ID).college_admin;
                if (!collegeUser2.Contains(dt.Rows[0]["id"].ToString()))
                {
                    context.Response.Write("{\"msg\":\"你没有权限修改此班级！\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限管理 End*/
            //检查该学号的学生是否存在
            if (userBll.Exists(userNum))
            {
                int i = userinfoBll.UpdateClassByUserID(userBll.GetUserIDByUserNum(userNum), classID);
                if (i == 1)
                {
                    context.Response.Write("{\"msg\":\"添加成功！\"}");
                }
                else
                {
                    context.Response.Write("{\"msg\":\"添加失败！\"}");
                }
            }
            else
            {
                context.Response.Write("{\"msg\":\"该学号学生不存在！\"}");
            }
        }
        #endregion

        #region 根据学号删除班级信息
        protected void DeleteStuFromClass(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string userNum = HttpContext.Current.Request["userNum"]; //获取学生学号
            int classID = Convert.ToInt32(HttpContext.Current.Request["classID"]); //获取班级ID
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                int college2ID = classBll.GetModel(classID).college_id;
                string collegeUser2 = collegeBll.GetModel(college2ID).college_admin;
                if (!collegeUser2.Contains(dt.Rows[0]["id"].ToString()))
                {
                    context.Response.Write("{\"msg\":\"你没有权限修改此班级！\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限管理 End*/
            //检查该学号的学生是否存在
            if (userBll.Exists(userNum))
            {
                int i = userinfoBll.UpdateClassByUserID(userBll.GetUserIDByUserNum(userNum), 0);
                if (i == 1)
                {
                    context.Response.Write("{\"msg\":\"删除成功！\"}");
                }
                else
                {
                    context.Response.Write("{\"msg\":\"删除失败！\"}");
                }
            }
            else
            {
                context.Response.Write("{\"msg\":\"该学号学生不存在！\"}");
            }
        }
        #endregion

        #region 导出改班级的学生，（导出Excel表）
        protected void CreateExcelByClassID(HttpContext context)
        {
            int classID = Convert.ToInt32(HttpContext.Current.Request["classID"]); //获取班级ID
            DataSet ds = userinfoBll.GetAllStudentByClassID(classID);
            string typeid = "1";
            string FileName = classBll.GetModel(classID).class_name + "名单.xls";
            HttpResponse resp;
            resp = context.Response;
            //resp.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
            //使用UTF-8对文件名进行编码
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            resp.AppendHeader("Content-Disposition", "attachment;filename=\"" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + "\"");
            resp.ContentType = "application/ms-excel;";
            string colHeaders = "", ls_item = "";
            int i = 0;

            //定义表对象与行对像，同时用DataSet对其值进行初始化 
            DataTable dt = ds.Tables[0];
            DataRow[] myRow = dt.Select("");
            // typeid=="1"时导出为EXCEL格式文件；typeid=="2"时导出为XML格式文件 
            if (typeid == "1")
            {
                //取得数据表各列标题，各标题之间以\t分割，最后一个列标题后加回车符 
                for (i = 0; i < dt.Columns.Count - 1; i++)
                    colHeaders += dt.Columns[i].Caption.ToString() + "\t";
                colHeaders += dt.Columns[i].Caption.ToString() + "\n";
                //向HTTP输出流中写入取得的数据信息 
                resp.Write(colHeaders);
                //逐行处理数据   
                foreach (DataRow row in myRow)
                {
                    //在当前行中，逐列获得数据，数据之间以\t分割，结束时加回车符\n 
                    for (i = 0; i < row.ItemArray.Length - 1; i++)
                        ls_item += row[i].ToString() + "\t";
                    ls_item += row[i].ToString() + "\n";
                    //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据     
                    resp.Write(ls_item);
                    ls_item = "";
                }
            }
            else
            {
                if (typeid == "2")
                {
                    //从DataSet中直接导出XML数据并且写到HTTP输出流中 
                    resp.Write(ds.GetXml());
                }
            }
            //写缓冲区中的数据到HTTP头文件中 
            resp.End();
        }
        #endregion

        #region 根据学院获取班级列表
        protected void GetAllClassByCollegeID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int collegeID = Convert.ToInt32(HttpContext.Current.Request["collegeID"]); //获取学院ID
            IEnumerable<Model.classes> classList = classBll.GetClassListByCollege(collegeID);
            string collegeName = collegeBll.GetModel(collegeID).college_name;
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            string collegeUser = collegeBll.GetModel(collegeID).college_admin;
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                if (!collegeUser.Contains(dt.Rows[0]["id"].ToString()))
                {
                    context.Response.Write("{\"classList\":[]}");
                    return;
                }
            }
            /*权限管理 End*/
            
            
            if (((System.Collections.Generic.List<XGhms.Model.classes>)classList).Count == 0)
            {
                context.Response.Write("{\"classList\":[]}");
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{\"classList\":[");
            foreach (Model.classes model in classList)
            {
                str.Append("{\"id\":" + model.id + ",");
                str.Append("\"className\":\"" + model.class_name + "\",");
                str.Append("\"collegeName\":\"" + collegeName + "\",");
                str.Append("\"headerTer\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(model.head_teacher)) + "\",");
                str.Append("\"stuLeader\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(model.class_leader)) + "\"},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 根据班级ID删除班级
        protected void DeleteClassByID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int classID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取班级ID
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"] == "CollegeAdmin")
            {
                int collegeID = classBll.GetModel(classID).college_id;
                string collegeUser = collegeBll.GetModel(collegeID).college_admin;
                if (!collegeUser.Contains(dt.Rows[0]["id"].ToString()))
                {
                    context.Response.Write("{\"msg\":\"你没有此权限！\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限管理 End*/
            if (classBll.Delete(classID))
            {
                context.Response.Write("{\"msg\":\"删除成功\"}");
            }
            else
            {
                context.Response.Write("{\"msg\":\"删除失败\"}");
            }
        }
        #endregion

        #region 获取学期的下拉列表（根据当前的）
        protected void GetAllTermForNow(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            IEnumerable<Model.term> modelList = termBll.GetAllTrem();
            StringBuilder str = new StringBuilder();
            str.Append("{\"termList\":[");
            foreach (Model.term model in modelList)
            {
                str.Append("{\"id\":" + model.id + ",");
                str.Append("\"termName\":\"" + model.term_name + "\",");
                str.Append("\"termCheck\":");
                if (model.term_name == Utils.GetNowTerm(DateTime.Today.Year, DateTime.Today.Month))
                {
                    str.Append("1");
                }
                else
                {
                    str.Append("0");
                }
                str.Append("},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 检查用户，根据用户的编号检查
        protected void CheckUserIDExist(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string userID = HttpContext.Current.Request["userID"]; //获取用户ID
            if (userID.Contains("-"))
            {
                userID = (userID.Split('-'))[0].ToString();
            }
            if (userBll.Exists(userID))
            {
                string userName = userinfoBll.GetUserRealNameForID(userBll.GetUserIDByUserNum(userID));
                if (userName == "" || userName == null)
                {
                    context.Response.Write("{\"msg\":0}"); //用户真实姓名不存在
                }
                else
                {
                    context.Response.Write("{\"msg\":1,\"userName\":\"" + userName + "\"}"); //显示
                }
            }
            else
            {
                context.Response.Write("{\"msg\":2}"); //用户不存在
            }
        }
        #endregion

        #region 根据课程的ID获取课程信息
        protected void GetCourseInfoByID(HttpContext context) 
        {
            context.Response.ContentType = "text/plain";
            int courseID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取课程ID
            Model.course model = courseBll.GetModel(courseID);
            IEnumerable<Model.term> modelList = termBll.GetAllTrem();
            StringBuilder str = new StringBuilder();
            str.Append("{\"courseName\":\""+model.course_name+"\",");
            str.Append("\"courseNum\":\""+model.course_number+"\",");
            str.Append("\"termID\":" + model.term_id + ",");
            str.Append("\"courseTer\":\"" + userBll.GetUserNumByUserID(Convert.ToInt32(model.teacher)) + "-" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(model.teacher)) + "\",");
            str.Append("\"collegeID\":"+model.college_id);
            str.Append("}");
            context.Response.Write(str.ToString());
        }
        #endregion

        #region 新建课程（管理员添加）
        protected void AddNewCourse(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string courseName = HttpContext.Current.Request["courseName"]; //获取课程名称
            string courseNum = HttpContext.Current.Request["courseNum"]; //获取课程编码
            string userID = HttpContext.Current.Request["courseTer"]; //获取用户号码
            int collegeNameID = Convert.ToInt32(HttpContext.Current.Request["collegeNameID"]); //获取学院ID
            int selTermID = Convert.ToInt32(HttpContext.Current.Request["selTermID"]); //获取学期ID
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                string collegeUser2 = collegeBll.GetModel(collegeNameID).college_admin;
                if (!collegeUser2.Contains(dt.Rows[0]["id"].ToString()))
                {
                    context.Response.Write("{\"msg\":\"你没有权限添加该学院的课程！\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限管理 End*/
            if (userID.Contains("-"))
            {
                userID = (userID.Split('-'))[0].ToString();
            }
            if (userBll.Exists(userID))
            {
                int isnert = courseBll.Insert(courseNum, courseName, selTermID, userBll.GetUserIDByUserNum(userID), collegeNameID);
                if (isnert==1)
                {
                    context.Response.Write("{\"msg\":\"新建成功！\"}"); //新建成功
                }
                else
                {
                    context.Response.Write("{\"msg\":\"新建失败！\"}"); //新建失败
                }
            }
            else
            {
                context.Response.Write("{\"msg\":\"该用户不存在！\"}"); //用户不存在
            }
        }
        #endregion

        #region 根据课程ID更新课程
        protected void UpdateOldCourse(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int courseID = Convert.ToInt32(HttpContext.Current.Request["courseID"]); //获取课程ID
            string courseName = HttpContext.Current.Request["courseName"]; //获取课程名称
            string courseNum = HttpContext.Current.Request["courseNum"]; //获取课程编码
            string userID = HttpContext.Current.Request["courseTer"]; //获取用户号码
            int collegeNameID = Convert.ToInt32(HttpContext.Current.Request["collegeNameID"]); //获取学院ID
            int selTermID = Convert.ToInt32(HttpContext.Current.Request["selTermID"]); //获取学期ID
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                int college2ID = courseBll.GetModel(courseID).college_id;
                string collegeUser2 = collegeBll.GetModel(college2ID).college_admin;
                if (!collegeUser2.Contains(dt.Rows[0]["id"].ToString()))
                {
                    context.Response.Write("{\"msg\":\"你没有权限修改此课程！\"}");
                    context.Response.End();
                    return;
                }
                string collegeUser = collegeBll.GetModel(collegeNameID).college_admin;
                if (!collegeUser.Contains(dt.Rows[0]["id"].ToString()))
                {
                    context.Response.Write("{\"msg\":\"你没有权限选择此学院！\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限管理 End*/
            if (userID.Contains("-"))
            {
                userID = (userID.Split('-'))[0].ToString();
            }
            if (userBll.Exists(userID))
            {
                int isnert = courseBll.UpdateOldCourse(courseID,courseNum, courseName, selTermID, userBll.GetUserIDByUserNum(userID), collegeNameID);
                if (isnert == 1)
                {
                    context.Response.Write("{\"msg\":\"更新成功！\"}"); //新建成功
                }
                else
                {
                    context.Response.Write("{\"msg\":\"更新失败！\"}"); //保存失败
                }
            }
            else
            {
                context.Response.Write("{\"msg\":\"该用户不存在！\"}"); //用户不存在
            }
        }
        #endregion

        #region 根据学期和学院获取课程的总数
        protected void GetNumTotalByCourseList(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int tremID = Convert.ToInt32(HttpContext.Current.Request["tremID"]); //获取学期ID
            int collegeID = Convert.ToInt32(HttpContext.Current.Request["collegeID"]); //获取学院ID
            int totalNum = courseBll.GetNumTotalByTremIdandCollegeID(tremID, collegeID);
            int pagenum = 1;
            if (totalNum <= 10)  //总数小于10条的情况
            {
                pagenum = 1;
            }
            else //总数大于10条的情况
            {
                if ((totalNum % 10) == 0) //能被10整除的情况
                {
                    pagenum = (totalNum / 10);
                }
                else  //不能被10整除的情况
                {
                    pagenum = (totalNum / 10) + 1;
                }
            }
            context.Response.Write("{\"pagenum\":" + pagenum + "}");
            context.Response.End();
        }
        #endregion

        #region 根据学期和学院还有分页来获取课程的列表
        protected void setCourselistForPage(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int tremID = Convert.ToInt32(HttpContext.Current.Request["tremID"]); //获取学期ID
            int collegeID = Convert.ToInt32(HttpContext.Current.Request["collegeID"]); //获取学院ID
            int pageNow = Convert.ToInt32(HttpContext.Current.Request["page"]); //获取当前的页码
            DataTable dt = courseBll.GetSelectByTremandCollegeandPage(tremID, collegeID, (pageNow - 1) * 10 + 1, pageNow * 10);
            StringBuilder str = new StringBuilder();
            str.Append("{\"courseList\":[" );
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Model.course model = courseBll.GetModel(Convert.ToInt32(dt.Rows[i]["id"]));
                str.Append("{\"id\":" + model.id + ",");
                str.Append("\"course_number\":\"" + model.course_number + "\",");
                str.Append("\"course_name\":\"" + model.course_name + "\",");
                str.Append("\"term\":\"" + termBll.GetModel(model.term_id).term_name + "\",");
                str.Append("\"teacher\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(model.teacher)) + "\",");
                str.Append("\"college\":\"" + collegeBll.GetModel(model.college_id).college_name + "\"");
                str.Append("},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 根据ID来删除课程
        protected void DeleteCourseByID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int courseID = Convert.ToInt32(HttpContext.Current.Request["courseID"]); //获取课程ID
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                int college2ID = courseBll.GetModel(courseID).college_id;
                string collegeUser2 = collegeBll.GetModel(college2ID).college_admin;
                if (!collegeUser2.Contains(dt.Rows[0]["id"].ToString()))
                {
                    context.Response.Write("{\"msg\":\"你没有权限删除此课程！\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限管理 End*/
            if (courseBll.Delete(courseID))
            {
                context.Response.Write("{\"msg\":\"删除成功！\"}"); //删除成功
            }
            else
            {
                context.Response.Write("{\"msg\":\"删除失败！\"}"); //删除失败
            }
        }
        #endregion

        #region 管理员添加新的用户
        protected void AddNewUserByAdmin(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string UserName = HttpContext.Current.Request["UserName"]; //获取用户名
            string UserRealName = HttpContext.Current.Request["UserRealName"]; //获取用户姓名
            string UserRole = HttpContext.Current.Request["UserRole"]; //获取用户角色
            int collegeID = Convert.ToInt32(HttpContext.Current.Request["collegeID"]); //获取学院ID
            int classID = Convert.ToInt32(HttpContext.Current.Request["classID"]); //获取班级ID
            int isLock = Convert.ToInt32(HttpContext.Current.Request["isLock"]); //获取班级ID
            int userSex = Convert.ToInt32(HttpContext.Current.Request["UserSex"]); //获取用户性别
            string majorName = HttpContext.Current.Request["majorName"]; //获取专业
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                string collegeUser2 = collegeBll.GetModel(collegeID).college_admin;
                if (!collegeUser2.Contains(dt.Rows[0]["id"].ToString()))
                {
                    context.Response.Write("{\"msg\":\"你没有权限添加该学院的用户！\"}");
                    context.Response.End();
                    return;
                }
                if (UserRole == "CollegeAdmin" || UserRole == "Admin" || UserRole == "Administrator")
                {
                    context.Response.Write("{\"msg\":\"你没有权限添加该角色！\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限管理 End*/
            if (userBll.Exists(UserName))
            {
                context.Response.Write("{\"msg\":\"该用户已存在！\"}"); //用户已存在
                context.Response.End();
                return;
            }
            int newUserID = userBll.InsertUserReturnID(roleBll.GetRoleIDByRoleName(UserRole), UserName, UserName, Utils.SHA1Encrypt("123456"), isLock);
            if (UserRole=="Student")
            {
                int stuN = userinfoBll.UpdateClassByUserID(newUserID, UserRealName, userSex, collegeID, classID, majorName);
                if (stuN==1)
                {
                    context.Response.Write("{\"msg\":\"保存成功！\"}"); //保存成功
                }
                else
                {
                    context.Response.Write("{\"msg\":\"保存失败！\"}"); //保存失败
                }
            }
            else if (UserRole=="Teacher")
            {
                int terN = userinfoBll.UpdateClassByUserID(newUserID, UserRealName, userSex, collegeID, 0, "");
                if (terN == 1)
                {
                    context.Response.Write("{\"msg\":\"保存成功！\"}"); //保存成功
                }
                else
                {
                    context.Response.Write("{\"msg\":\"保存失败！\"}"); //保存失败
                }
            }
            else if (UserRole == "HeadTeacher")
            {
                int htN = userinfoBll.UpdateClassByUserID(newUserID, UserRealName, userSex, collegeID, 0, "");
                if (htN == 1)
                {
                    context.Response.Write("{\"msg\":\"保存成功！\"}"); //保存成功
                }
                else
                {
                    context.Response.Write("{\"msg\":\"保存失败！\"}"); //保存失败
                }
            }
            else if (UserRole == "CollegeAdmin")
            {
                int colN = userinfoBll.UpdateClassByUserID(newUserID, UserRealName, userSex, 0, 0, "");
                if (colN == 1)
                {
                    context.Response.Write("{\"msg\":\"保存成功！\"}"); //保存成功
                }
                else
                {
                    context.Response.Write("{\"msg\":\"保存失败！\"}"); //保存失败
                }
            }
            else if (UserRole == "Admin")
            {
                int admN = userinfoBll.UpdateClassByUserID(newUserID, UserRealName, userSex, 0, 0, "");
                if (admN == 1)
                {
                    context.Response.Write("{\"msg\":\"保存成功！\"}"); //保存成功
                }
                else
                {
                    context.Response.Write("{\"msg\":\"保存失败！\"}"); //保存失败
                }
            }
            else if (UserRole == "Administrator")
            {
                int admsN = userinfoBll.UpdateClassByUserID(newUserID, UserRealName, userSex, 0, 0, "");
                if (admsN == 1)
                {
                    context.Response.Write("{\"msg\":\"保存成功！\"}"); //保存成功
                }
                else
                {
                    context.Response.Write("{\"msg\":\"保存失败！\"}"); //保存失败
                }
            }
        }
        #endregion

        #region 根据用户ID获取用户的信息
        protected void GetUserInfoByID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int userID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取用户ID
            Model.users userModel = userBll.GetModel(userID);
            Model.users_info userInfoModel = userinfoBll.GetModelByUserID(userID);
            Model.role roleModel = roleBll.GetModel(userModel.role_id);
            StringBuilder str = new StringBuilder();
            str.Append("{\"userID\":"+userModel.id+",");
            str.Append("\"userName\":\""+userModel.user_name+"\",");
            str.Append("\"userRoleID\":" + userModel.role_id + ",");
            str.Append("\"userRoleName\":\"" + roleModel.role_name + "\",");
            str.Append("\"userRealName\":\"" + userInfoModel.real_name + "\",");
            str.Append("\"userSex\":" + userInfoModel.sex + ",");
            str.Append("\"userMajor\":\"" + userInfoModel.major + "\",");
            str.Append("\"userCollegeID\":" + userInfoModel.college_id + ",");
            str.Append("\"userClassID\":" + userInfoModel.class_id + ",");
            str.Append("\"userLock\":" + userModel.is_lock);
            str.Append("}");
            context.Response.Write(str.ToString());
        }
        #endregion

        #region 管理员更新用户
        protected void UpdateOldUserByAdmin(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int UserID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取用户ID
            string UserRealName = HttpContext.Current.Request["UserRealName"]; //获取用户姓名
            string UserRole = HttpContext.Current.Request["UserRole"]; //获取用户角色
            int collegeID = Convert.ToInt32(HttpContext.Current.Request["collegeID"]); //获取学院ID
            int classID = Convert.ToInt32(HttpContext.Current.Request["classID"]); //获取班级ID
            int isLock = Convert.ToInt32(HttpContext.Current.Request["isLock"]); //获取用户是否锁定
            int userSex = Convert.ToInt32(HttpContext.Current.Request["UserSex"]); //获取用户性别
            string majorName = HttpContext.Current.Request["majorName"]; //获取专业名称
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                string collegeUser = collegeBll.GetModel(collegeID).college_admin;
                if (!collegeUser.Contains(dt.Rows[0]["id"].ToString()))
                {
                    context.Response.Write("{\"msg\":\"你没有权限修选择该学院！\"}");
                    context.Response.End();
                    return;
                }
                if (UserRole == "CollegeAdmin"||UserRole == "Admin"||UserRole == "Administrator")
                {
                    context.Response.Write("{\"msg\":\"你没有权限添加该角色！\"}");
                    context.Response.End();
                    return;
                }
                Model.users_info userinfoModel = userinfoBll.GetModelByUserID(UserID); //获取原来的用户的info
                string userrolename=roleBll.GetModel(userinfoModel.role_id).role_name; //获取原来用户的角色
                if (userrolename == "Student" || userrolename == "Teacher" || userrolename == "HeadTeacher")
                {
                    string collegeUser2 = collegeBll.GetModel(userinfoModel.college_id).college_admin;
                    if (!collegeUser2.Contains(dt.Rows[0]["id"].ToString()))
                    {
                        context.Response.Write("{\"msg\":\"你没有权限修改该用户！\"}");
                        context.Response.End();
                        return;
                    }
                }
                if (userrolename == "CollegeAdmin" || userrolename == "Admin" || userrolename == "Administrator")
                {
                    context.Response.Write("{\"msg\":\"你没有权限修改该用户！\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限管理 End*/
            int userUp = userBll.UpdateUserByIDForAdmin(UserID, roleBll.GetRoleIDByRoleName(UserRole), isLock);
            if (UserRole == "Student")
            {
                int stuN = userinfoBll.UpdateClassByUserID(UserID, UserRealName, userSex, collegeID, classID, majorName);
                if (stuN == 1 && userUp==1)
                {
                    context.Response.Write("{\"msg\":\"保存成功！\"}"); //保存成功
                }
                else
                {
                    context.Response.Write("{\"msg\":\"保存失败！\"}"); //保存失败
                }
            }
            else if (UserRole == "Teacher")
            {
                int terN = userinfoBll.UpdateClassByUserID(UserID, UserRealName, userSex, collegeID, 0, "");
                if (terN == 1 && userUp == 1)
                {
                    context.Response.Write("{\"msg\":\"保存成功！\"}"); //保存成功
                }
                else
                {
                    context.Response.Write("{\"msg\":\"保存失败！\"}"); //保存失败
                }
            }
            else if (UserRole == "HeadTeacher")
            {
                int htN = userinfoBll.UpdateClassByUserID(UserID, UserRealName, userSex, collegeID, 0, "");
                if (htN == 1 && userUp == 1)
                {
                    context.Response.Write("{\"msg\":\"保存成功！\"}"); //保存成功
                }
                else
                {
                    context.Response.Write("{\"msg\":\"保存失败！\"}"); //保存失败
                }
            }
            else if (UserRole == "CollegeAdmin")
            {
                int colN = userinfoBll.UpdateClassByUserID(UserID, UserRealName, userSex, 0, 0, "");
                if (colN == 1 && userUp == 1)
                {
                    context.Response.Write("{\"msg\":\"保存成功！\"}"); //保存成功
                }
                else
                {
                    context.Response.Write("{\"msg\":\"保存失败！\"}"); //保存失败
                }
            }
            else if (UserRole == "Admin")
            {
                int admN = userinfoBll.UpdateClassByUserID(UserID, UserRealName, userSex, 0, 0, "");
                if (admN == 1 && userUp == 1)
                {
                    context.Response.Write("{\"msg\":\"保存成功！\"}"); //保存成功
                }
                else
                {
                    context.Response.Write("{\"msg\":\"保存失败！\"}"); //保存失败
                }
            }
            else if (UserRole == "Administrator")
            {
                int admsN = userinfoBll.UpdateClassByUserID(UserID, UserRealName, userSex, 0, 0, "");
                if (admsN == 1 && userUp == 1)
                {
                    context.Response.Write("{\"msg\":\"保存成功！\"}"); //保存成功
                }
                else
                {
                    context.Response.Write("{\"msg\":\"保存失败！\"}"); //保存失败
                }
            }
        }
        #endregion

        #region 重置密码
        protected void ResetPwdFromAdmin(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int UserID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取用户ID
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                Model.users_info userinfoModel = userinfoBll.GetModelByUserID(UserID);
                string userrolename = roleBll.GetModel(userinfoModel.role_id).role_name;
                if (userrolename == "Student" || userrolename == "Teacher" || userrolename == "HeadTeacher")
                {
                    string collegeUser2 = collegeBll.GetModel(userinfoModel.college_id).college_admin;
                    if (!collegeUser2.Contains(dt.Rows[0]["id"].ToString()))
                    {
                        context.Response.Write("{\"msg\":\"你没有权限修改该用户！\"}");
                        context.Response.End();
                        return;
                    }
                }
                else
                {
                    context.Response.Write("{\"msg\":\"你没有权限修改该用户！\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限管理 End*/
            int pwd = userBll.UpdateUserPwd(UserID,Utils.SHA1Encrypt("123456"));
            if (pwd==1)
            {
                context.Response.Write("{\"msg\":\"重置成功！\"}"); //重置成功
            }
            else
            {
                context.Response.Write("{\"msg\":\"重置失败！\"}"); //重置失败
            }
        }
        #endregion

        #region 根据各个参数获取用户列表的总数
        protected void SelectUserListTotalBySelter(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string UserRole = HttpContext.Current.Request["UserRole"]; //获取所选角色
            int CollegeID = Convert.ToInt32(HttpContext.Current.Request["CollegeID"]); //获取学院ID
            int ClassID = Convert.ToInt32(HttpContext.Current.Request["ClassID"]); //获取班级ID
            int totalNum = 0;
            if (UserRole == "Student")
            {
                if (CollegeID==0)  //学院ID为空的，表示该学生不属于与任何学院
                {
                    totalNum = userinfoBll.GetTotalNumOFStuandClsandRol(roleBll.GetRoleIDByRoleName(UserRole), 0, 0);
                }
                if (CollegeID != 0 && ClassID == 0) //学院ID不为空。班级ID为空，表示该学生属于该学院但是不属于任何班级
                {
                    totalNum = userinfoBll.GetTotalNumOFStuandClsandRol(roleBll.GetRoleIDByRoleName(UserRole), CollegeID, 0);
                }
                if (CollegeID != 0 && ClassID != 0) //学院ID和班级ID都不为空，表示该学生属于该学院该班级，正常情况
                {
                    totalNum = userinfoBll.GetTotalNumOFStuandClsandRol(roleBll.GetRoleIDByRoleName(UserRole), CollegeID, ClassID);
                } 
            }
            else if (UserRole == "Teacher")
            {
                if (CollegeID==0)
                {
                    totalNum = userinfoBll.GetTotalNumOfTerandCls(roleBll.GetRoleIDByRoleName(UserRole), 0);
                }
                else
                {
                    totalNum = userinfoBll.GetTotalNumOfTerandCls(roleBll.GetRoleIDByRoleName(UserRole), CollegeID);
                }
            }
            else if (UserRole == "HeadTeacher")
            {
                if (CollegeID == 0)
                {
                    totalNum = userinfoBll.GetTotalNumOfTerandCls(roleBll.GetRoleIDByRoleName(UserRole), 0);
                }
                else
                {
                    totalNum = userinfoBll.GetTotalNumOfTerandCls(roleBll.GetRoleIDByRoleName(UserRole), CollegeID);
                }
            }
            else if (UserRole == "CollegeAdmin")
            {
                totalNum = userinfoBll.GetTotalNumOfAdm(roleBll.GetRoleIDByRoleName(UserRole));
            }
            else if (UserRole == "Admin")
            {
                totalNum = userinfoBll.GetTotalNumOfAdm(roleBll.GetRoleIDByRoleName(UserRole));
            }
            else if (UserRole == "Administrator")
            {
                totalNum = userinfoBll.GetTotalNumOfAdm(roleBll.GetRoleIDByRoleName(UserRole));
            }
            int pagenum = 1;
            if (totalNum <= 10)  //总数小于10条的情况
            {
                pagenum = 1;
            }
            else //总数大于10条的情况
            {
                if ((totalNum % 10) == 0) //能被10整除的情况
                {
                    pagenum = (totalNum / 10);
                }
                else  //不能被10整除的情况
                {
                    pagenum = (totalNum / 10) + 1;
                }
            }
            context.Response.Write("{\"pagenum\":" + pagenum + "}");
            context.Response.End();
        }
        #endregion

        #region 管理员获取用户列表信息
        protected void SelectUserListForAdmin(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string UserRole = HttpContext.Current.Request["UserRole"]; //获取所选角色
            int CollegeID = Convert.ToInt32(HttpContext.Current.Request["CollegeID"]); //获取学院ID
            int ClassID = Convert.ToInt32(HttpContext.Current.Request["ClassID"]); //获取班级ID
            int pageNow = Convert.ToInt32(HttpContext.Current.Request["nowPageNum"]); //获取当前的页码
            DataTable dt=new DataTable();
            if (UserRole == "Student")
            {
                if (CollegeID == 0)  //学院ID为空的，表示该学生不属于与任何学院
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@roleID", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = roleBll.GetRoleIDByRoleName(UserRole);
                    parameters[1].Value = ((pageNow - 1) * 10 + 1);
                    parameters[2].Value = pageNow * 10;
                    dt = userinfoBll.GetPageOfRoleForUserList("users_SelectIDByStuForColidIsNull", parameters);
                }
                if (CollegeID != 0 && ClassID == 0) //学院ID不为空。班级ID为空，表示该学生属于该学院但是不属于任何班级
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@roleID", SqlDbType.Int,6),
                        new SqlParameter("@collegeID", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = roleBll.GetRoleIDByRoleName(UserRole);
                    parameters[1].Value = CollegeID;
                    parameters[2].Value = ((pageNow - 1) * 10 + 1);
                    parameters[3].Value = pageNow * 10;
                    dt = userinfoBll.GetPageOfRoleForUserList("users_SelectIDByStuForColidAndClsIsBull", parameters);
                }
                if (CollegeID != 0 && ClassID != 0) //学院ID和班级ID都不为空，表示该学生属于该学院该班级，正常情况
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@roleID", SqlDbType.Int,6),
                        new SqlParameter("@collegeID", SqlDbType.Int,6),
                        new SqlParameter("@classID", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = roleBll.GetRoleIDByRoleName(UserRole);
                    parameters[1].Value = CollegeID;
                    parameters[2].Value = ClassID;
                    parameters[3].Value = ((pageNow - 1) * 10 + 1);
                    parameters[4].Value = pageNow * 10;
                    dt = userinfoBll.GetPageOfRoleForUserList("users_SelectIDByStuForColidAndClsid", parameters);
                }
            }
            if (UserRole == "Teacher")
            {
                if (CollegeID == 0)
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@roleID", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = roleBll.GetRoleIDByRoleName(UserRole);
                    parameters[1].Value = ((pageNow - 1) * 10 + 1);
                    parameters[2].Value = pageNow * 10;
                    dt = userinfoBll.GetPageOfRoleForUserList("users_SelectIDByTerForColidIsNull", parameters);
                }
                else
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@roleID", SqlDbType.Int,6),
                        new SqlParameter("@collegeID", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = roleBll.GetRoleIDByRoleName(UserRole);
                    parameters[1].Value = CollegeID;
                    parameters[2].Value = ((pageNow - 1) * 10 + 1);
                    parameters[3].Value = pageNow * 10;
                    dt = userinfoBll.GetPageOfRoleForUserList("users_SelectIDByTerForColid", parameters);
                }
            }
            if (UserRole == "HeadTeacher")
            {
                if (CollegeID == 0)
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@roleID", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = roleBll.GetRoleIDByRoleName(UserRole);
                    parameters[1].Value = ((pageNow - 1) * 10 + 1);
                    parameters[2].Value = pageNow * 10;
                    dt = userinfoBll.GetPageOfRoleForUserList("users_SelectIDByTerForColidIsNull", parameters);
                }
                else
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@roleID", SqlDbType.Int,6),
                        new SqlParameter("@collegeID", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = roleBll.GetRoleIDByRoleName(UserRole);
                    parameters[1].Value = CollegeID;
                    parameters[2].Value = ((pageNow - 1) * 10 + 1);
                    parameters[3].Value = pageNow * 10;
                    dt = userinfoBll.GetPageOfRoleForUserList("users_SelectIDByTerForColid", parameters);
                }
            }
            if (UserRole == "CollegeAdmin")
            {
                SqlParameter[] parameters = {
					    new SqlParameter("@roleID", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                parameters[0].Value = roleBll.GetRoleIDByRoleName(UserRole);
                parameters[1].Value = ((pageNow - 1) * 10 + 1);
                parameters[2].Value = pageNow * 10;
                dt = userinfoBll.GetPageOfRoleForUserList("users_SelectIDByAdm", parameters);
            }
            if (UserRole == "Admin")
            {
                SqlParameter[] parameters = {
					    new SqlParameter("@roleID", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                parameters[0].Value = roleBll.GetRoleIDByRoleName(UserRole);
                parameters[1].Value = ((pageNow - 1) * 10 + 1);
                parameters[2].Value = pageNow * 10;
                dt = userinfoBll.GetPageOfRoleForUserList("users_SelectIDByAdm", parameters);
            }
            if (UserRole == "Administrator")
            {
                SqlParameter[] parameters = {
					    new SqlParameter("@roleID", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                parameters[0].Value = roleBll.GetRoleIDByRoleName(UserRole);
                parameters[1].Value = ((pageNow - 1) * 10 + 1);
                parameters[2].Value = pageNow * 10;
                dt = userinfoBll.GetPageOfRoleForUserList("users_SelectIDByAdm", parameters);
            }
            if (dt.Rows.Count==0)
            {
                context.Response.Write("{\"userList\":[]}");
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{\"userList\":[");
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Model.users_info userinfoModel = userinfoBll.GetModel(Convert.ToInt32(dt.Rows[i]["id"]));
                Model.users userModel = userBll.GetModel(userinfoModel.user_id);
                str.Append("{\"id\":" + userinfoModel.user_id + ",");
                str.Append("\"userName\":\"" + userModel.user_name+ "\",");
                str.Append("\"userNum\":\"" + userModel.user_number + "\",");
                str.Append("\"realName\":\"" + userinfoModel.real_name + "\",");
                if (userinfoModel.sex==1)
                {
                    str.Append("\"userSex\":\"男\",");
                }
                else
                {
                    str.Append("\"userSex\":\"女\",");
                }
                str.Append("\"userMajor\":\"" + userinfoModel.major + "\",");
                str.Append("\"userLock\":\"" + userModel.is_lock + "\"");
                str.Append("},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 根据用户ID删除用户
        protected void DeleteUserByID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int userID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取当前的用户名
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                Model.users_info userinfoModel = userinfoBll.GetModelByUserID(userID);
                string userrolename = roleBll.GetModel(userinfoModel.role_id).role_name;
                if (userrolename == "Student" || userrolename == "Teacher" || userrolename == "HeadTeacher")
                {
                    string collegeUser2 = collegeBll.GetModel(userinfoModel.college_id).college_admin;
                    if (!collegeUser2.Contains(dt.Rows[0]["id"].ToString()))
                    {
                        context.Response.Write("{\"msg\":\"你没有权限删除该用户！\"}");
                        context.Response.End();
                        return;
                    }
                }
                else
                {
                    context.Response.Write("{\"msg\":\"你没有权限删除该用户！\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限管理 End*/
            if (userBll.Delete(userID))
            {
                context.Response.Write("{\"msg\":\" 删除成功 \"}");
            }
            else
            {
                context.Response.Write("{\"msg\":\" 删除失败 \"}");
            }
        }
        #endregion

        #region 根据课程ID和学生ID添加课程学生表（单个学生添加）
        protected void UpdateUserCourseByUserID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int courseID = Convert.ToInt32(HttpContext.Current.Request["courseID"]); //获取课程ID
            string stuNum = HttpContext.Current.Request["userNum"]; //获取学生的学号
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                string collegeUser2 = collegeBll.GetModel(courseBll.GetModel(courseID).college_id).college_admin;
                if (!collegeUser2.Contains(dt.Rows[0]["id"].ToString()))
                {
                    context.Response.Write("{\"msg\":\"你没有权限修改该课程！\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限管理 End*/
            if (courstuBll.Exists(courseID, userBll.GetUserIDByUserNum(stuNum)))
            {
                context.Response.Write("{\"msg\":\"该学生已经存在！\"}");
                context.Response.End();
                return;
            }
            int ri = courstuBll.Insert(courseID, userBll.GetUserIDByUserNum(stuNum));
            if (ri == 1)
            {
                context.Response.Write("{\"msg\":\"添加成功！\"}"); //添加成功
            }
            if (ri != 1)
            {
                context.Response.Write("{\"msg\":\"添加失败！\"}"); //添加失败
            }
        }
        #endregion

        #region 根据学生的ID来单个删除课程里面的学生
        protected void DeleteStuFromCourse(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int courseID = Convert.ToInt32(HttpContext.Current.Request["courseID"]); //获取课程ID
            int stuID = Convert.ToInt32(HttpContext.Current.Request["userNum"]); //获取学生ID
            /*权限管理 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            if (dt.Rows[0]["role_name"].ToString() == "CollegeAdmin")
            {
                string collegeUser2 = collegeBll.GetModel(courseBll.GetModel(courseID).college_id).college_admin;
                if (!collegeUser2.Contains(dt.Rows[0]["id"].ToString()))
                {
                    context.Response.Write("{\"msg\":\"你没有权限修改该课程！\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限管理 End*/
            bool ri = courstuBll.Delete(courseID, stuID);
            if (ri)
            {
                context.Response.Write("{\"msg\":\"删除成功！\"}"); //添加成功
            }
            if (!ri)
            {
                context.Response.Write("{\"msg\":\"深处失败！\"}"); //添加失败
            }
        }
        #endregion

        #region 获取课程里面的学生列表
        protected void CreateExcelByCourseID(HttpContext context)
        {
            int courseID = Convert.ToInt32(HttpContext.Current.Request["courseID"]); //获取班级ID
            DataSet ds = courstuBll.SelectCourseStudentForExcel(courseID);
            Model.course courseModel = courseBll.GetModel(courseID);
            Model.term term = termBll.GetModel(courseModel.term_id);
            string typeid = "1";
            string FileName = term.term_name + courseModel.course_name +"("+ courseModel.id + ")学生名单.xls";
            HttpResponse resp;
            resp = context.Response;
            //resp.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
            //使用UTF-8对文件名进行编码
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            resp.AppendHeader("Content-Disposition", "attachment;filename=\"" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + "\"");
            resp.ContentType = "application/ms-excel;";
            string colHeaders = "", ls_item = "";
            int i = 0;

            //定义表对象与行对像，同时用DataSet对其值进行初始化 
            DataTable dt = ds.Tables[0];
            DataRow[] myRow = dt.Select("");
            // typeid=="1"时导出为EXCEL格式文件；typeid=="2"时导出为XML格式文件 
            if (typeid == "1")
            {
                //取得数据表各列标题，各标题之间以\t分割，最后一个列标题后加回车符 
                for (i = 0; i < dt.Columns.Count - 1; i++)
                    colHeaders += dt.Columns[i].Caption.ToString() + "\t";
                colHeaders += dt.Columns[i].Caption.ToString() + "\n";
                //向HTTP输出流中写入取得的数据信息 
                resp.Write(colHeaders);
                //逐行处理数据   
                foreach (DataRow row in myRow)
                {
                    //在当前行中，逐列获得数据，数据之间以\t分割，结束时加回车符\n 
                    for (i = 0; i < row.ItemArray.Length - 1; i++)
                        ls_item += row[i].ToString() + "\t";
                    ls_item += row[i].ToString() + "\n";
                    //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据     
                    resp.Write(ls_item);
                    ls_item = "";
                }
            }
            else
            {
                if (typeid == "2")
                {
                    //从DataSet中直接导出XML数据并且写到HTTP输出流中 
                    resp.Write(ds.GetXml());
                }
            }
            //写缓冲区中的数据到HTTP头文件中 
            resp.End();
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