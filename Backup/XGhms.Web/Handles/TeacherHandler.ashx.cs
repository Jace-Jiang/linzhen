// Author: 陈旭东
// Create date: 2015-4-8
// Description:	这个一般处理程序专门处理老师的事件方法
using System.Web;
using System.Data;
using System.Web.SessionState;
using System;
using System.Text;
using System.Collections.Generic;
using XGhms.Helper;
using System.Collections;

namespace XGhms.Web.Handles
{
    /// <summary>
    /// TeacherHandler 的摘要说明
    /// </summary>
    public class TeacherHandler : IHttpHandler, IRequiresSessionState
    {
        BLL.classes classBll = new BLL.classes();
        BLL.college collegeBll = new BLL.college();
        BLL.users_info userinfoBll = new BLL.users_info();
        BLL.users userBll = new BLL.users();
        BLL.term termBll = new BLL.term();
        BLL.course courseBll = new BLL.course();
        BLL.course_student courstuBll = new BLL.course_student();
        BLL.course_homework courhwBll = new BLL.course_homework();
        BLL.homework_student hwstuBll = new BLL.homework_student();
        public void ProcessRequest(HttpContext context)
        {
            DataTable dt = new Common.TerPageBase().GetLoginUserInfo();
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
                case "GetClassInfoByTerIDClsID":
                    GetClassInfoByTerIDClsID(context);
                    break;
                case "CreateExcelByClassID":
                    CreateExcelByClassID(context);
                    break;
                case "SaveClassInfo":
                    SaveClassInfo(context);
                    break;
                case "GetTerCourseByTrem":
                    GetTerCourseByTrem(context);
                    break;
                case "GetStuListByCourseID":
                    GetStuListByCourseID(context);
                    break;
                case "GetCollegeList":
                    GetCollegeList(context);
                    break;
                case "SaveCourseInfo":
                    SaveCourseInfo(context);
                    break;
                case "GetCourseListByTerandTerm":
                    GetCourseListByTerandTerm(context);
                    break;
                case "GetHomeWorkListBytermAndCor":
                    GetHomeWorkListBytermAndCor(context);
                    break;
                case "GetHomeWorkTotalNumBytermAndCor":
                    GetHomeWorkTotalNumBytermAndCor(context);
                    break;
                case "deleteHomeWork":
                    deleteHomeWork(context);
                    break;
                case "GetStuListRandomByCourseID":
                    GetStuListRandomByCourseID(context);
                    break;
                case "GetCourseInfoBycid":
                    GetCourseInfoBycid(context);
                    break;
                case "addnewHW":
                    addnewHW(context);
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
                case "updoldHW":
                    updoldHW(context);
                    break;
                case "GetHWInfoByid":
                    GetHWInfoByid(context);
                    break;
                case "GetHomeWorkTotalNumByThreeCS":
                    GetHomeWorkTotalNumByThreeCS(context);
                    break;
                case "GetHomeWorkListByByThreeCS":
                    GetHomeWorkListByByThreeCS(context);
                    break;
                case "deleteStuHomework":
                    deleteStuHomework(context);
                    break;
                case "GetStuHWInfoByID":
                    GetStuHWInfoByID(context);
                    break;
                case "CheckStudentWork":
                    CheckStudentWork(context);
                    break;
                case "ReformWork":
                    ReformWork(context);
                    break;
                case "GetNoCheckWorkNum":
                    GetNoCheckWorkNum(context);
                    break;
                case "GetCheckWorkNum":
                    GetCheckWorkNum(context);
                    break;
                case "GetThreeHWlistForDefault":
                    GetThreeHWlistForDefault(context);
                    break;
                case "GetTwentyScoreByCourseID":
                    GetTwentyScoreByCourseID(context);
                    break;
                //case "UpdateUserCourseByUserID":
                //    UpdateUserCourseByUserID(context);
                //    break;
                //case "DeleteStuFromCourse":
                //    DeleteStuFromCourse(context);
                //    break;
                //case "CreateExcelByCourseID":
                //    CreateExcelByCourseID(context);
                //    break;
                default:
                    break;
            }
            #endregion
        }

        #region 根据班级ID获取班级信息
        protected void GetClassInfoByTerIDClsID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int classID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取班级ID
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
            if (!classBll.Exists(classID))
            {
                context.Response.Write("{\"msgtype\":0}");
                context.Response.End();
                return;
            }
            if (dt.Rows[0]["role_name"].ToString() == "HeadTeacher")
            {
                if (classBll.Exists(classID, userID))
                {
                    Model.classes classModel = classBll.GetModel(classID);
                    StringBuilder str = new StringBuilder();
                    str.Append("{\"msgtype\":2,"); //表示该用户可以修改班级的信息
                    str.Append("\"class_name\":\"" + classModel.class_name + "\",");
                    str.Append("\"college\":\""  + collegeBll.GetModel(classModel.college_id).college_name + "\",");
                    str.Append("\"head_teacher\":\"你自己\",");
                    str.Append("\"class_leader\":" + GetUserForDropdownList(classModel.class_leader) + ",");
                    str.Append("\"squad_leader\":" + GetUserForDropdownList(classModel.squad_leader) + ",");
                    str.Append("\"class_group_secretary\":" + GetUserForDropdownList(classModel.class_group_secretary) + ",");
                    str.Append("\"study_secretary\":" + GetUserForDropdownList(classModel.study_secretary) + ",");
                    str.Append("\"life_secretary\":" + GetUserForDropdownList(classModel.life_secretary));
                    str.Append("}");
                    context.Response.Write(str.ToString());
                    context.Response.End();
                    return;
                }
            }
            Model.classes classModel2 = classBll.GetModel(classID);
            StringBuilder str2 = new StringBuilder();
            str2.Append("{\"msgtype\":1,"); //表示该用户只可以查看班级的信息
            str2.Append("\"class_name\":\"" + classModel2.class_name + "\",");
            str2.Append("\"college\":\"" + collegeBll.GetModel(classModel2.college_id).college_name + "\",");
            str2.Append("\"head_teacher\":\"" + userBll.GetUserNumByUserID(Convert.ToInt32(classModel2.head_teacher)) + "-" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(classModel2.head_teacher)) + "\",");
            str2.Append("\"class_leader\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(classModel2.class_leader)) + "\",");
            str2.Append("\"squad_leader\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(classModel2.squad_leader)) + "\",");
            str2.Append("\"class_group_secretary\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(classModel2.class_group_secretary)) + "\",");
            str2.Append("\"study_secretary\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(classModel2.study_secretary)) + "\",");
            str2.Append("\"life_secretary\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(classModel2.life_secretary)) + "\"");
            str2.Append("}");
            context.Response.Write(str2.ToString());
            context.Response.End();
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
            //使用GB2312对文件名进行编码
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

        #region 辅导员修改班级
        protected void SaveClassInfo(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int classID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取班级ID
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
            if (!classBll.Exists(classID))
            {
                context.Response.Write("{\"msg\":\"你没有权限修改该班级\"}");
                context.Response.End();
                return;
            }
            int sel_leader = Convert.ToInt32(HttpContext.Current.Request["sel_leader"]); //获取班长ID
            int sel_squadLeader = Convert.ToInt32(HttpContext.Current.Request["sel_squadLeader"]); //获取副班长ID
            int sel_groupSecretary = Convert.ToInt32(HttpContext.Current.Request["sel_groupSecretary"]); //获取团支书ID
            int sel_stduySecretary = Convert.ToInt32(HttpContext.Current.Request["sel_stduySecretary"]); //获取学习委员ID
            int sel_lifeSecretary = Convert.ToInt32(HttpContext.Current.Request["sel_lifeSecretary"]); //获取生活委员ID
            int ri = classBll.UpdateClassByTer(classID, sel_leader, sel_squadLeader, sel_groupSecretary, sel_stduySecretary, sel_lifeSecretary);
            if (ri==1)
            {
                context.Response.Write("{\"msg\":\"修改成功！\"}");
            }
            else
            {
                context.Response.Write("{\"msg\":\"修改失败！\"}");
            }
        }
        #endregion

        #region 获取学院列表
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
                str.Append("\"},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 根据学院获取班级列表
        protected void GetAllClassByCollegeID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int collegeID = Convert.ToInt32(HttpContext.Current.Request["collegeID"]); //获取学院ID
            IEnumerable<Model.classes> classList = classBll.GetClassListByCollege(collegeID);
            string collegeName = collegeBll.GetModel(collegeID).college_name;
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

        #region 根据学期和老师的ID获取老师的课程列表
        protected void GetTerCourseByTrem(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int tremID = Convert.ToInt32(HttpContext.Current.Request["tremID"]); //获取学期ID
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
            DataTable dtc = courseBll.GetCourseListByTerTrem(userID, tremID);
            if (dtc.Rows.Count == 0)
            {
                context.Response.Write("{\"courseList\":[]}");
                context.Response.End();
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{\"courseList\":[");
            for (int i = 0; i < dtc.Rows.Count; i++)
            {
                Model.course model = courseBll.GetModel(Convert.ToInt32(dtc.Rows[i]["id"]));
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

        #region 根据课程ID和老师的ID获取课程的信息
        protected void GetCourseInfoByID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int courseID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取课程ID
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
            if (!courseBll.Exists(courseID,userID))
            {
                context.Response.Write("{\"msgtype\":0}");
                context.Response.End();
                return;
            }
            else
            {
                Model.course model = courseBll.GetModel(courseID);
                StringBuilder str = new StringBuilder();
                str.Append("{\"msgtype\":1,");
                str.Append("\"course_name\":\"" + model.course_name + "\",");
                str.Append("\"course_number\":\"" + model.course_number + "\",");
                str.Append("\"term\":\"" + termBll.GetModel(model.term_id).term_name + "\",");
                str.Append("\"college\":\"" + collegeBll.GetModel(model.college_id).college_name + "\",");
                str.Append("\"teacher\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(model.teacher)) + "\",");
                str.Append("\"other_teacher\":\""+userBll.GetUserNumByUserID(Convert.ToInt32(model.other_teacher))+"-"+ userinfoBll.GetUserRealNameForID(Convert.ToInt32(model.other_teacher)) + "\",");
                str.Append("\"student\":\"" + model.student_leader + "\",");
                str.Append("\"course_info\":\"" + HttpUtility.UrlEncodeUnicode(model.course_info).Replace("+", "%20") + "\"");
                str.Append("}");
                context.Response.Write(str.ToString());
            }
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

        #region 根据课程ID获取学生列表
        protected void GetStuListByCourseID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int courseID;
            if (HttpContext.Current.Request["id"]==null||HttpContext.Current.Request["id"]=="")
            {
                int hwID = Convert.ToInt32(HttpContext.Current.Request["hwid"]); //获取课程ID
                courseID = courhwBll.GetModel(hwID).course_id;
            }
            else
            {
                courseID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取课程ID
            }
            DataTable dt = courstuBll.SelectCourseStudentList(courseID);
            StringBuilder str = new StringBuilder();
            str.Append("{\"stulist\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str.Append("{\"id\":"+dt.Rows[i]["student_id"]);
                str.Append(",\"value\":\"" + dt.Rows[i]["real_name"] + "\"},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 随机数的产生，用于随机产生几个名额的学生
        protected void GetStuListRandomByCourseID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int courseID;
            if (HttpContext.Current.Request["id"] == null || HttpContext.Current.Request["id"] == "")
            {
                int hwID = Convert.ToInt32(HttpContext.Current.Request["hwid"]); //获取课程ID
                courseID = courhwBll.GetModel(hwID).course_id;
            }
            else
            {
                courseID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取课程ID
            }
            int num = Convert.ToInt32(HttpContext.Current.Request["value"]); //获取随机的数目
            DataTable dt = courstuBll.SelectCourseStudentList(courseID);
            if (dt.Rows.Count<=num)
            {
                StringBuilder str = new StringBuilder();
                str.Append("{\"stulist\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str.Append("{\"id\":" + dt.Rows[i]["student_id"]);
                    str.Append(",\"value\":\"" + dt.Rows[i]["real_name"] + "\"},");
                }
                str.Append("]}");
                context.Response.Write(str.Remove((str.Length - 3), 1));
                context.Response.End();
                return;
            }
            else
            { //产生num个随机的数，从dt.Rows.Count里面
                Random ab = new Random();//定义一个随机数对象
                int[] x1 = new int[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x1[i] = i;
                }
                ArrayList al = new ArrayList(x1);
                int[] x2 = new int[num];
                int y;
                for (int i = 0; i < num; i++)
                {
                    y = ab.Next(0, al.Count);
                    x2[i] = Convert.ToInt32(al[y]);
                    al.Remove(al[y]);
                }
                StringBuilder str = new StringBuilder();
                str.Append("{\"stulist\":[");
                for (int i = 0; i < num; i++)
                {
                    str.Append("{\"id\":" + dt.Rows[x2[i]]["student_id"]);
                    str.Append(",\"value\":\"" + dt.Rows[x2[i]]["real_name"] + "\"},");
                }
                str.Append("]}");
                context.Response.Write(str.Remove((str.Length - 3), 1));
                context.Response.End();
            }
        }
        #endregion

        #region 保存课程信息
        protected void SaveCourseInfo(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int courseID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取课程ID
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
            if (!courseBll.Exists(courseID, userID))
            {
                context.Response.Write("{\"msg\":\"没有权限\"}");
                context.Response.End();
                return;
            }
            string oteacherID = HttpContext.Current.Request["otherTeacher"]; //获取助教ID
            if (oteacherID.Contains("-"))
            {
                oteacherID = (oteacherID.Split('-'))[0].ToString();
            }
            int studentID = Convert.ToInt32(HttpContext.Current.Request["student"]); //获取学生ID
            string courseInfo = HttpContext.Current.Request["courseinfo"]; //获取课程信息
            //执行更新语句
            int ri = courseBll.Update(courseID,userBll.GetUserIDByUserNum(oteacherID).ToString(),Utils.XSSstring(studentID.ToString()), courseInfo);
            if (ri==1)
            {
                context.Response.Write("{\"msg\":\"更新成功\"}");
            }
            else
            {
                context.Response.Write("{\"msg\":\"更新失败\"}");
            }
        }
        #endregion

        #region 根据学期ID和老师的ID获取老师的本学期课程列表（用于下拉列表）
        protected void GetCourseListByTerandTerm(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int tremID = Convert.ToInt32(HttpContext.Current.Request["tremID"]); //获取学期ID
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
            DataTable dtc = courseBll.GetCourseListByTerTrem(userID, tremID);
            if (dtc.Rows.Count == 0)
            {
                context.Response.Write("{\"courseList\":[]}");
                context.Response.End();
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{\"courseList\":[");
            for (int i = 0; i < dtc.Rows.Count; i++)
            {
                Model.course model = courseBll.GetModel(Convert.ToInt32(dtc.Rows[i]["id"]));
                str.Append("{\"id\":" + model.id + ",");
                str.Append("\"course_name\":\"" + model.course_name + "\"");
                str.Append("},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 根据课程ID获取作业总数，并且分页显示
        protected void GetHomeWorkListBytermAndCor(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int courseID = Convert.ToInt32(HttpContext.Current.Request["courseID"]); //获取课程ID
            int totalNum = courhwBll.GetPageNumOfCourseID(courseID);
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

        #region 根据课程ID获取作业列表，用于分页显示
        protected void GetHomeWorkTotalNumBytermAndCor(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int courseID = Convert.ToInt32(HttpContext.Current.Request["courseID"]); //获取课程ID
            int pageNow = Convert.ToInt32(HttpContext.Current.Request["nowPageNum"]); //获取页码
            DataTable dt = courhwBll.GetPageOfCourseID(courseID, (pageNow - 1) * 10 + 1, pageNow * 10);
            if (dt.Rows.Count==0)
            {
                context.Response.Write("{\"hwList\":[]}");
                context.Response.End();
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{\"hwList\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Model.course_homework courhwModel = courhwBll.GetModel(Convert.ToInt32(dt.Rows[i]["id"]));
                str.Append("{\"id\":" + courhwModel.id + ",");
                str.Append("\"homework_name\":\"" + courhwModel.homework_name + "\",");
                str.Append("\"course_name\":\"" + courseBll.GetModel(courseID).course_name + "\",");
                str.Append("\"homework_beginTime\":\"" + courhwModel.homework_beginTime.ToString("yyyy-MM-dd hh:mm") + "\",");
                str.Append("\"homework_endTime\":\"" + courhwModel.homework_endTime.ToString("yyyy-MM-dd hh:mm") + "\"");
                str.Append("},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 根据作业ID删除作业
        protected void deleteHomeWork(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int hwID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取作业ID
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
            Model.course_homework courhwModel = courhwBll.GetModel(hwID);
            if (courseBll.GetModel(courhwModel.course_id).teacher == userID.ToString())
            {
                if (courhwBll.Delete(hwID))
                {
                    context.Response.Write("{\"msg\":\"删除成功\"}");
                    context.Response.End();
                }
                else
                {
                    context.Response.Write("{\"msg\":\"删除失败\"}");
                    context.Response.End();
                }
            }
            else
            {
                context.Response.Write("{\"msg\":\"没有权限\"}");
                context.Response.End();
            }
        }
        #endregion

        #region 根据课程ID获取课程信息
        protected void GetCourseInfoBycid(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int cid = Convert.ToInt32(HttpContext.Current.Request["cid"]); //获取课程ID
            Model.course courseModel = courseBll.GetModel(cid);
            //首先获取学期是否是本学期
            if (termBll.GetModel(courseModel.term_id).term_name==Utils.GetNowTerm(DateTime.Today.Year,DateTime.Today.Month))
            {
                DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
                int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
                if (courseBll.Exists(cid, userID))
                {
                    StringBuilder str = new StringBuilder();
                    Model.course model = courseBll.GetModel(cid);
                    str.Append("{\"msgtype\":1,\"id\":" + model.id + ",");
                    str.Append("\"course_number\":\"" + model.course_number + "\",");
                    str.Append("\"course_name\":\"" + model.course_name + "\",");
                    str.Append("\"term\":\"" + termBll.GetModel(model.term_id).term_name + "\",");
                    str.Append("\"teacher\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(model.teacher)) + "\",");
                    str.Append("\"college\":\"" + collegeBll.GetModel(model.college_id).college_name + "\"");
                    str.Append("}");
                    context.Response.Write(str.ToString());
                    context.Response.End();
                }
                else
                {
                    context.Response.Write("{\"msgtype\":0,\"msg\":\"你没有权限添加该课程的作业\"}");
                    context.Response.End();
                }
            }
            else
            {
                context.Response.Write("{\"msgtype\":0,\"msg\":\"该课程已过期，请重新选择\"}");
                context.Response.End();
            }
        }
        #endregion

        #region 根据作业ID获取作业信息
        protected void GetHWInfoByid(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取作业ID
            Model.course_homework courhwModel = courhwBll.GetModel(id);
            int courseID = courhwModel.course_id;
            Model.course courseModel = courseBll.GetModel(courseID);
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
            if (courseBll.Exists(courseID, userID))
            {
                StringBuilder str = new StringBuilder();
                str.Append("{\"msgtype\":1,\"id\":" + courhwModel.id + ",");
                str.Append("\"course_name\":\"" + courseModel.course_name + "\",");
                str.Append("\"term\":\"" + termBll.GetModel(courseModel.term_id).term_name + "\",");
                str.Append("\"homework_name\":\"" + HttpUtility.UrlEncodeUnicode(courhwModel.homework_name).Replace("+", "%20") + "\",");
                str.Append("\"homework_info\":\"" + HttpUtility.UrlEncodeUnicode(courhwModel.homework_info).Replace("+", "%20") + "\",");
                str.Append("\"homework_beginTime\":\"" + courhwModel.homework_beginTime.ToString("yyyy/MM/dd HH:mm") + "\",");
                str.Append("\"homework_endTime\":\"" + courhwModel.homework_endTime.ToString("yyyy/MM/dd HH:mm") + "\"");
                str.Append("}");
                context.Response.Write(str.ToString());
                context.Response.End();
            }
            else
            {
                context.Response.Write("{\"msgtype\":0,\"msg\":\"你没有权限查看该课程的作业\"}");
                context.Response.End();
            }
        }
        #endregion

        #region 添加发布新的作业
        protected void addnewHW(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取课程ID
            Model.course courseModel = courseBll.GetModel(id);
            //首先获取学期是否是本学期
            if (termBll.GetModel(courseModel.term_id).term_name == Utils.GetNowTerm(DateTime.Today.Year, DateTime.Today.Month))
            {
                DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
                int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
                if (courseBll.Exists(id, userID))
                {
                    string hwName = HttpContext.Current.Request["hwName"]; //获取作业名
                    string hwInfo = HttpContext.Current.Request["hwCon"]; //获取作业信息
                    string userList = HttpContext.Current.Request["userList"]; //获取学生列表
                    string beginTime=HttpContext.Current.Request["beginTime"]; //获取作业开始时间
                    string endTime=HttpContext.Current.Request["endTime"]; //获取作业结束时间
                    string[] s = userList.Split(new char[] { ',' });
                    int succes = 0;
                    int error = 0;
                    int newhwID = courhwBll.InsertNewHWGethwID(id, hwName, hwInfo, beginTime, endTime);
                    for (int i = 0; i < s.Length - 1; i++)
                    {
                        if (hwstuBll.Insertstuhw(newhwID,Convert.ToInt32(s[i]))== 1)
                        {
                            succes = succes + 1;
                        }
                        else
                        {
                            error = error + 1;
                        }
                    }
                    context.Response.Write("{\"msg\":\"发布作业成功" + succes + "个，失败" + error + "个\"}");
                    context.Response.End();
                }
                else
                {
                    context.Response.Write("{\"msg\":\"你没有权限添加该课程的作业\"}");
                    context.Response.End();
                }
            }
            else
            {
                context.Response.Write("{\"msg\":\"该课程已过期，请重新选择\"}");
                context.Response.End();
            }
        }
        #endregion

        #region 修改作业
        protected void updoldHW(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取作业ID
            int courseID = courhwBll.GetModel(id).course_id;
            Model.course courseModel = courseBll.GetModel(courseID);
            //首先获取学期是否是本学期
            if (termBll.GetModel(courseModel.term_id).term_name == Utils.GetNowTerm(DateTime.Today.Year, DateTime.Today.Month))
            {
                DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
                int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
                if (courseBll.Exists(courseID, userID))
                {
                    string hwName = HttpContext.Current.Request["hwName"]; //获取作业名
                    string hwInfo = HttpContext.Current.Request["hwCon"]; //获取作业信息
                    string userList = HttpContext.Current.Request["userList"]; //获取学生列表
                    string beginTime = HttpContext.Current.Request["beginTime"]; //获取作业开始时间
                    string endTime = HttpContext.Current.Request["endTime"]; //获取作业结束时间
                    string[] s = userList.Split(new char[] { ',' });
                    int succes = 0;
                    int error = 0;
                    int ri = courhwBll.UpdateHW(id, hwName, hwInfo, beginTime, endTime);
                    if (ri != 1)
                    {
                        context.Response.Write("{\"msg\":\"修改失败\"}");
                        context.Response.End();
                        return;
                    }
                    for (int i = 0; i < s.Length - 1; i++)
                    {
                        if (!hwstuBll.Exists("homework_id=" + id + " and student_id="+Convert.ToInt32(s[i])))
                        {
                            if (hwstuBll.Insertstuhw(id, Convert.ToInt32(s[i])) == 1)
                            {
                                succes = succes + 1;
                            }
                            else
                            {
                                error = error + 1;
                            }
                        }
                    }
                    context.Response.Write("{\"msg\":\"修改作业成功，添加成功" + succes + "个，失败" + error + "个\"}");
                    context.Response.End();
                }
                else
                {
                    context.Response.Write("{\"msg\":\"你没有权限修改该课程的作业\"}");
                    context.Response.End();
                }
            }
            else
            {
                context.Response.Write("{\"msg\":\"该作业已过期，请重新选择\"}");
                context.Response.End();
            }
        }
        #endregion

        #region 获取分页数目你，学生的作业分页查询
        protected void GetHomeWorkTotalNumByThreeCS(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int courseID = Convert.ToInt32(HttpContext.Current.Request["courseID"]); //获取课程ID
            int hwStatus = Convert.ToInt32(HttpContext.Current.Request["hwStatus"]); //获取作业状态
            int totalNum = hwstuBll.GetHomeWorkTotalNumByThreeCS(courseID, hwStatus);
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

        #region 学生的作业分页显示的方法
        protected void GetHomeWorkListByByThreeCS(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int courseID = Convert.ToInt32(HttpContext.Current.Request["courseID"]); //获取课程ID
            int hwStatus = Convert.ToInt32(HttpContext.Current.Request["hwStatus"]); //获取作业状态
            int pageNow = Convert.ToInt32(HttpContext.Current.Request["nowPageNum"]); //获取页码
            DataTable dt = hwstuBll.GetHomeWorkListByByThreeCSOfPage(courseID, hwStatus, (pageNow - 1) * 10 + 1, pageNow * 10);
            if (dt.Rows.Count == 0)
            {
                context.Response.Write("{\"hwList\":[]}");
                context.Response.End();
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{\"hwList\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Model.homework_student hwstuModel=hwstuBll.GetModel(Convert.ToInt32(dt.Rows[i]["id"]));
                Model.course_homework courhwModel = courhwBll.GetModel(hwstuModel.homework_id);
                Model.course courseModel = courseBll.GetModel(courhwModel.course_id);
                str.Append("{\"id\":" + courhwModel.id + ",");
                str.Append("\"stuhwID\":" + hwstuModel.id + ",");
                str.Append("\"homework_name\":\"" + courhwModel.homework_name + "\",");
                str.Append("\"course_name\":\"" + courseModel.course_name + "\",");
                str.Append("\"student_name\":\"" + userinfoBll.GetUserRealNameForID(hwstuModel.student_id) + "\",");
                str.Append("\"homework_status\":" + hwstuModel.homework_status);
                str.Append("},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 根据作业ID删除该学生的作业
        protected void deleteStuHomework(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取学生作业ID
            /*权限检查 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
            if (courseBll.GetModel(courhwBll.GetModel(hwstuBll.GetModel(id).homework_id).course_id).teacher != userID.ToString())
            {
                context.Response.Write("{\"msg\":\"你没有权限删除该作业\"}");
                context.Response.End();
                return;
            }
            else
            {
                if (hwstuBll.Delete(id))
                {
                    context.Response.Write("{\"msg\":\"删除成功\"}");
                    context.Response.End();
                    return;
                }
                else
                {
                    context.Response.Write("{\"msg\":\"删除失败\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限检查 End*/
        }
        #endregion

        #region 根据学生作业的ID获取详细信息
        protected void GetStuHWInfoByID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取学生作业ID
            /*权限检查 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
            Model.homework_student hwstuModel = hwstuBll.GetModel(id);
            Model.course_homework courhwModel = courhwBll.GetModel(hwstuModel.homework_id);
            Model.course courseModel = courseBll.GetModel(courhwModel.course_id);
            if (courseModel.teacher != userID.ToString()) //检查权限
            {
                context.Response.Write("{\"msgtype\":0,\"msg\":\"你没有权限查看该作业\"}");
                context.Response.End();
                return;
            }/*权限检查 End*/
            else
            {
                if (hwstuModel.homework_status==1)
                {
                    int ri = hwstuBll.terLookingStuWork(id);
                }
                StringBuilder str = new StringBuilder();
                str.Append("{\"msgtype\":1,\"id\":" + hwstuModel.id + ","); //作业ID
                str.Append("\"course_name\":\"" + courseModel.course_name + "\","); //课程名
                str.Append("\"term\":\"" + termBll.GetModel(courseModel.term_id).term_name + "\","); //学期
                str.Append("\"homework_name\":\"" + courhwModel.homework_name + "\","); //作业名
                str.Append("\"student_name\":\"" + userinfoBll.GetUserRealNameForID(hwstuModel.student_id) + "\","); //学生姓名
                str.Append("\"student_num\":\"" + userBll.GetUserNumByUserID(hwstuModel.student_id) + "\","); //学生学号
                str.Append("\"submit_time\":\"" + hwstuModel.submit_time.ToString("yyyy-MM-dd HH:mm") + "\","); //作业提交时间
                str.Append("\"homework_con\":\"" + HttpUtility.UrlEncodeUnicode(hwstuModel.submit_content).Replace("+", "%20") + "\","); //提交的作业内容
                if (hwstuModel.submit_file==null)
                {
                    str.Append("\"submit_file\":\"\","); //作业的路径
                }
                else
                {
                    str.Append("\"submit_file\":\"" + HttpUtility.UrlEncodeUnicode(hwstuModel.submit_file).Replace("+", "%20") + "\","); //作业的路径
                }
                if (hwstuModel.homework_comment == null)
                {
                    str.Append("\"homework_comment\":\"\","); //作业的回复
                }
                else
                {
                    str.Append("\"homework_comment\":\"" + HttpUtility.UrlEncodeUnicode(hwstuModel.homework_comment).Replace("+", "%20") + "\","); //作业的回复
                }
                str.Append("\"homework_score\":" + hwstuModel.homework_score); //作业的成绩
                str.Append("}");
                context.Response.Write(str.ToString());
                context.Response.End();
            }
        }
        #endregion

        #region 根据学生的ID，老师修改批阅学生的作业
        protected void CheckStudentWork(HttpContext context)
        { 
            context.Response.ContentType = "text/plain";
            int id = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取学生作业ID
            string con = HttpContext.Current.Request["con"]; //获取老师的评价
            int score= Convert.ToInt32(HttpContext.Current.Request["score"]); //获取学生作业的成绩
            /*权限检查 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
            Model.homework_student hwstuModel = hwstuBll.GetModel(id);
            Model.course_homework courhwModel = courhwBll.GetModel(hwstuModel.homework_id);
            Model.course courseModel = courseBll.GetModel(courhwModel.course_id);
            if (courseModel.teacher != userID.ToString()) //检查权限
            {
                context.Response.Write("{\"msg\":\"你没有权限修改该作业\"}");
                context.Response.End();
                return;
            }/*权限检查 End*/
            else
            {
                if (hwstuModel.homework_status==0||hwstuModel.homework_status==3)
                {
                    context.Response.Write("{\"msg\":\"等学生做完了作业再批改吧\"}");
                    context.Response.End();
                    return;
                }
                int ri = hwstuBll.terCheckStuHomeWork(id, con, score);
                if (ri==1)
                {
                    context.Response.Write("{\"msg\":\"提交成功\"}");
                    context.Response.End();
                    return;
                }
                else
                {
                    context.Response.Write("{\"msg\":\"提交失败\"}");
                    context.Response.End();
                    return;
                }
            }
        }
        #endregion

        #region 重做作业，让学生重做作业
        protected void ReformWork(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取学生作业ID
            /*权限检查 Begin*/
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
            if (courseBll.GetModel(courhwBll.GetModel(hwstuBll.GetModel(id).homework_id).course_id).teacher != userID.ToString())
            {
                context.Response.Write("{\"msg\":\"你没有权限修改该作业\"}");
                context.Response.End();
                return;
            }
            else
            {
                int ri = hwstuBll.ReformWork(id);
                if (ri==1)
                {
                    context.Response.Write("{\"msg\":\"提交成功\"}");
                    context.Response.End();
                    return;
                }
                else
                {
                    context.Response.Write("{\"msg\":\"提交失败\"}");
                    context.Response.End();
                    return;
                }
            }
            /*权限检查 End*/
        }
        #endregion

        #region 获取老师未完成批改的作业数目
        protected void GetNoCheckWorkNum(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
            int ri = hwstuBll.GetNoCheckWorkNum(userID);
            context.Response.Write("{\"num\":"+ri+"}");
            context.Response.End();
        }
        #endregion

        #region 获取老师已完成批改的作业数目
        protected void GetCheckWorkNum(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取老师ID
            int ri = hwstuBll.GetCheckWorkNum(userID);
            context.Response.Write("{\"num\":" + ri + "}");
            context.Response.End();
        }
        #endregion

        #region 获取最近三条数据，查看学生的未批改的作业
        protected void GetThreeHWlistForDefault(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dtuser = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dtuser.Rows[0]["id"]); //获取老师ID
            DataTable dt = hwstuBll.GetThreeNoChecikForDefault(userID);
            if (dt.Rows.Count == 0)
            {
                context.Response.Write("{\"hwList\":[]}");
                context.Response.End();
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{\"hwList\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Model.homework_student hwstuModel = hwstuBll.GetModel(Convert.ToInt32(dt.Rows[i]["id"]));
                Model.course_homework courhwModel = courhwBll.GetModel(hwstuModel.homework_id);
                Model.course courseModel = courseBll.GetModel(courhwModel.course_id);
                str.Append("{\"id\":" + courhwModel.id + ",");
                str.Append("\"stuhwID\":" + hwstuModel.id + ",");
                str.Append("\"homework_name\":\"" + courhwModel.homework_name + "\",");
                str.Append("\"course_name\":\"" + courseModel.course_name + "\",");
                str.Append("\"student_name\":\"" + userinfoBll.GetUserRealNameForID(hwstuModel.student_id) + "\",");
                str.Append("\"homework_status\":" + hwstuModel.homework_status);
                str.Append("},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 根据课程ID显示其最近的20条成绩曲线
        protected void GetTwentyScoreByCourseID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取Session的值
            string type = HttpContext.Current.Request["type"]; //获取要显示的类型
            int courseID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取课程的ID
            //检查该课程是否存在并该学生是否学习该课程
            if (courseBll.Exists(courseID))
            {
                StringBuilder str = new StringBuilder();
                str.Append("[");
                //根据学生ID和课程ID获取前20条数据
                DataTable dtOfcor = courhwBll.GetTwentyHWByCourID(courseID);
                if (type == "stu")  //返回该学生的作业
                {
                    for (int i = 0; i < dtOfcor.Rows.Count; i++)
                    {
                        str.Append("[" + (i + 1) + ",");
                        str.Append(hwstuBll.GetScoreByStuIDhwID(Convert.ToInt32(dt.Rows[0]["id"]), Convert.ToInt32(dtOfcor.Rows[i]["id"])) + ",");
                        str.Append("\"" + dtOfcor.Rows[i]["homework_name"].ToString() + "\",");
                        str.Append("\"" + Convert.ToDateTime(dtOfcor.Rows[i]["homework_beginTime"]).ToString("MM月dd号") + "\"");
                        str.Append("],");
                    }
                }
                if (type == "all") //返回班级平均分
                {
                    for (int i = 0; i < dtOfcor.Rows.Count; i++)
                    {
                        str.Append("[" + (i + 1) + ",");
                        str.Append(courhwBll.GetAVGByhwID(Convert.ToInt32(dtOfcor.Rows[i]["id"])) + ",");
                        str.Append("\"" + dtOfcor.Rows[i]["homework_name"].ToString() + "\",");
                        str.Append("\"" + Convert.ToDateTime(dtOfcor.Rows[i]["homework_beginTime"]).ToString("MM月dd号") + "\"");
                        str.Append("],");
                    }
                }
                str.Append("]");
                context.Response.Write(str.Remove((str.Length - 2), 1));
            }
            else
            {
                context.Response.Write("false");
                context.Response.End();
                return;
            }
        }
        #endregion

        #region 如果为空，显示0
        public string GetUserForDropdownList(string userID)
        {
            if (userID==null)
            {
                return "0";
            }
            else
            {
                return userID;
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