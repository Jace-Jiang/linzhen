// Author: 陈旭东
// Create date: 2015-4-5
// Description:	这个一般处理程序专门处理学生的方法
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.SessionState;
using System.Text;
using XGhms.Helper;

namespace XGhms.Web.Handles
{
    /// <summary>
    /// StudentHandler 的摘要说明
    /// </summary>
    public class StudentHandler : IHttpHandler, IRequiresSessionState
    {
        BLL.course courseBll = new BLL.course();
        BLL.users_info userinfoBll = new BLL.users_info();
        BLL.term termBll = new BLL.term();
        BLL.college collegeBll = new BLL.college();
        BLL.users userBll = new BLL.users();
        BLL.homework_student hwstuBll = new BLL.homework_student();
        BLL.course_homework courhwBll = new BLL.course_homework();
        BLL.course_student courstuBll = new BLL.course_student();
        public void ProcessRequest(HttpContext context)
        {
            DataTable dt = new Common.StuPageBase().GetLoginUserInfo();
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
                case "GetCourseInfoByID":
                    GetCourseInfoByID(context);
                    break;
                case "GetAllTermForNow":
                    GetAllTermForNow(context);
                    break;
                case "GetStuCourseByTrem":
                    GetStuCourseByTrem(context);
                    break;
                case "GetHWListNumByCourseIdAndStuID":
                    GetHWListNumByCourseIdAndStuID(context);
                    break;
                case "GetHWListByCourseIdAndStuID":
                    GetHWListByCourseIdAndStuID(context);
                    break;
                case "GetCourseListByStuandTerm":
                    GetCourseListByStuandTerm(context);
                    break;
                case "GetHomeworkByID":
                    GetHomeworkByID(context);
                    break;
                case "GetHomeworkInfoByID":
                    GetHomeworkInfoByID(context);
                    break;
                case "StuSubmitHomeWork":
                    StuSubmitHomeWork(context);
                    break;
                case "GetHomeworkEditByID":
                    GetHomeworkEditByID(context);
                    break;
                case "GetHomeWorkInfoForTer":
                    GetHomeWorkInfoForTer(context);
                    break;
                case "GetStuScoreDefault":
                    GetStuScoreDefault(context);
                    break;
                case "GetTwentyScoreByCourseID":
                    GetTwentyScoreByCourseID(context);
                    break;
                case "GetStuNoWorkNum":
                    GetStuNoWorkNum(context);
                    break;
                case "GetStuWorkNum":
                    GetStuWorkNum(context);
                    break;
                case "GetAllLine":
                    GetAllLine(context);
                    break;
                default:
                    break;
            }
            #endregion
        }

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

        #region 根据学期和学生的ID获取老师的课程列表
        protected void GetStuCourseByTrem(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int tremID = Convert.ToInt32(HttpContext.Current.Request["tremID"]); //获取学期ID
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取学生ID
            DataTable dtc = courseBll.GetCourseListByStuTrem(userID, tremID);
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

        #region 根据课程ID获取课程的信息
        protected void GetCourseInfoByID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int courseID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取课程ID
            Model.course model = courseBll.GetModel(courseID);
            StringBuilder str = new StringBuilder();
            str.Append("{\"msgtype\":1,");
            str.Append("\"course_name\":\"" + model.course_name + "\",");
            str.Append("\"course_number\":\"" + model.course_number + "\",");
            str.Append("\"term\":\"" + termBll.GetModel(model.term_id).term_name + "\",");
            str.Append("\"college\":\"" + collegeBll.GetModel(model.college_id).college_name + "\",");
            str.Append("\"teacher\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(model.teacher)) + "\",");
            str.Append("\"other_teacher\":\"" + userBll.GetUserNumByUserID(Convert.ToInt32(model.other_teacher)) + "-" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(model.other_teacher)) + "\",");
            str.Append("\"student\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(model.student_leader)) + "\",");
            str.Append("\"course_info\":\"" + HttpUtility.UrlEncodeUnicode(model.course_info).Replace("+", "%20") + "\"");
            str.Append("}");
            context.Response.Write(str.ToString());
        }
        #endregion

        #region 根据课程ID和学生ID还有作业状态获取作业的列表总数
        public void GetHWListNumByCourseIdAndStuID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int courseID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取课程ID
            int status = Convert.ToInt32(HttpContext.Current.Request["status"]); //获取课程ID
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取学生ID
            int totalNum = hwstuBll.GetHWListNumByCourseIdAndStuID(courseID, userID, status);
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
            context.Response.Write("[{\"pagenum\":" + pagenum + "}]");
            context.Response.End();
        }
        #endregion

        #region 根据课程ID和学生ID还有作业状态获取作业的列表
        public void GetHWListByCourseIdAndStuID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int courseID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取课程ID
            int status = Convert.ToInt32(HttpContext.Current.Request["status"]); //获取课程ID
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取学生ID
            int page = Convert.ToInt32(HttpContext.Current.Request["page"]); //获取要显示的页数
            DataTable myHwDt = hwstuBll.GetHWListByCourseIdAndStuID(courseID, userID, status, (page - 1) * 10 + 1, page * 10);
            if (myHwDt.Rows.Count==0)
            {
                context.Response.Write("{\"Homework\":[]}");
                context.Response.End();
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{\"Homework\":[");
            for (int i = 0; i < myHwDt.Rows.Count; i++)
            {
                Model.homework_student hwstuModel = hwstuBll.GetModel(Convert.ToInt32(myHwDt.Rows[i]["id"]));
                Model.course_homework courhwModel = courhwBll.GetModel(hwstuModel.homework_id);
                Model.course courseModel = courseBll.GetModel(courhwModel.course_id);
                str.Append("{\"id\":" + hwstuModel.id + ",");
                str.Append("\"homeworkID\":" + hwstuModel.homework_id + ",");
                str.Append("\"courseName\":\"" + courseModel.course_name + "\",");
                str.Append("\"homeworkName\":\"" + courhwModel.homework_name+ "\",");
                str.Append("\"homeworkTime\":\"" + courhwModel.homework_beginTime.ToString("yyy-MM-dd HH:mm") + "\",");
                str.Append("\"teacher\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(courseModel.teacher)) + "\",");
                str.Append("\"status\":" + hwstuModel.homework_status + "},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 根据学期ID和学生的ID获取学生的本学期课程列表（用于下拉列表）
        protected void GetCourseListByStuandTerm(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int tremID = Convert.ToInt32(HttpContext.Current.Request["tremID"]); //获取学期ID
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取session值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取学生ID
            DataTable dtc = courseBll.GetCourseListByStuTrem(userID, tremID);
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

        #region 根据该学生的作业id来获取作业信息和该作业的情况
        protected void GetHomeworkByID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取学生ID
            int hwID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取作业的ID,同时具有防止SQL注入的作用
            if (!hwstuBll.Exists("homework_id=" + hwID + " and student_id=" + userID))
            {
                context.Response.Write("{\"id\":0,\"msg\":\"该作业不属于该学生\"}");
                context.Response.End();
            }
            else
            {
                //获取作业的相关信息，这里获取作业的名称、授课老师、作业开始时间、结束时间、课程名称，作业状态
                DataTable myHW = hwstuBll.GetStuHomeWorkInfoFormyhw(userID, hwID);
                StringBuilder str = new StringBuilder();
                str.Append("{");
                str.Append("\"id\":" + myHW.Rows[0]["id"] + ",");
                str.Append("\"hwId\":" + myHW.Rows[0]["hwId"] + ",");
                str.Append("\"homework_name\":\"" + myHW.Rows[0]["homework_name"] + "\",");
                str.Append("\"courseID\":" + myHW.Rows[0]["courseID"] + ",");
                str.Append("\"course_name\":\"" + myHW.Rows[0]["course_name"] + "\",");
                str.Append("\"hw_begtime\":\"" + ((DateTime)myHW.Rows[0]["hw_begtime"]).ToString("yyyy-MM-dd HH:mm") + "\",");
                str.Append("\"hw_endtime\":\"" + ((DateTime)myHW.Rows[0]["hw_endtime"]).ToString("yyyy-MM-dd HH:mm") + "\",");
                str.Append("\"course_teacher\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(myHW.Rows[0]["course_teacher"])) + "\",");
                str.Append("\"hw_status\":\"" + myHW.Rows[0]["hw_status"] + "\"");
                str.Append("}");
                context.Response.Write(str.ToString());
                context.Response.End();
            }
        }
        #endregion

        #region 根据学生作业的ID来获取学生作业的详细信息说明
        protected void GetHomeworkInfoByID(HttpContext context)
        {
            context.Response.ContentType = "html/plain";
            int hwID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取作业的ID,同时具有防止SQL注入的作用
            context.Response.Write(courhwBll.GetHWinfosByID(hwID));
            context.Response.End();
        }
        #endregion

        #region 根据作业的ID和学生ID获取作业的提交情况
        protected void GetHomeworkEditByID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取学生ID
            int hwID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取作业的ID,同时具有防止SQL注入的作用
            if (!hwstuBll.Exists("homework_id=" + hwID + " and student_id=" + userID))
            {
                context.Response.Write("{\"id\":0,\"msg\":\"该作业不属于该学生\"}");
                context.Response.End();
                return;
            }
            else
            {
                int rid = hwstuBll.GetIDByhwIDandStuID(hwID, userID);
                if (rid == 0)
                {
                    context.Response.Write("{\"id\":0,\"msg\":\"该作业没有提交\"}");
                    context.Response.End();
                    return;
                }
                else
                {
                    Model.homework_student hwstuModel = hwstuBll.GetModel(rid);
                    StringBuilder str = new StringBuilder();
                    str.Append("{");
                    str.Append("\"id\":" + hwstuModel.id + ",");
                    str.Append("\"submit_content\":\"" + HttpUtility.UrlEncodeUnicode(hwstuModel.submit_content).Replace("+", "%20") + "\",");
                    str.Append("\"submit_file\":\"" + HttpUtility.UrlEncodeUnicode(hwstuModel.submit_file).Replace("+", "%20") + "\"}");
                    context.Response.Write(str.ToString());
                }
            }
        }
        #endregion

        #region 根据作业ID获取老师的批阅状态
        protected void GetHomeWorkInfoForTer(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取学生ID
            int hwID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取作业的ID,同时具有防止SQL注入的作用
            if (!hwstuBll.Exists("homework_id=" + hwID + " and student_id=" + userID))
            {
                context.Response.Write("{\"id\":0,\"msg\":\"该作业不属于该学生\"}");
                context.Response.End();
                return;
            }
            else
            {
                int rid = hwstuBll.GetIDByhwIDandStuID(hwID, userID);
                if (rid == 0)
                {
                    context.Response.Write("{\"id\":0,\"msg\":\"该作业没有提交\"}");
                    context.Response.End();
                    return;
                }
                else
                {
                    Model.homework_student hwstuModel = hwstuBll.GetModel(rid);
                    StringBuilder str = new StringBuilder();
                    str.Append("{");
                    str.Append("\"id\":" + hwstuModel.id + ",");
                    str.Append("\"homework_comment\":\"" + HttpUtility.UrlEncodeUnicode(hwstuModel.homework_comment).Replace("+", "%20") + "\",");
                    str.Append("\"homework_score\":" + hwstuModel.homework_score + "}");
                    context.Response.Write(str.ToString());
                }
            }
        }
        #endregion

        #region 学生提交作业
        protected void StuSubmitHomeWork(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取学生ID
            int hwID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取作业的ID,同时具有防止SQL注入的作用
            if (!hwstuBll.Exists("homework_id=" + hwID + " and student_id=" + userID))
            {
                context.Response.Write("{\"msg\":\"该作业不属于你，你不能提交\"}");
                context.Response.End();
                return;
            }
            else
            {
                int hwStatus = hwstuBll.GethwStatusByStuandhwID(hwID, userID);
                if (hwStatus==2)
                {
                    context.Response.Write("{\"msg\":\"该作业老师正在审批，无法修改，请联系课程老师\"}");
                    context.Response.End();
                    return;
                }
                else if (hwStatus==4)
                {
                    context.Response.Write("{\"msg\":\"该作业已经完成，请不要重复提交\"}");
                    context.Response.End();
                    return;
                }
                else if (hwStatus==0||hwStatus==1||hwStatus==3)
                {
                    string htmlCon=HttpContext.Current.Request["htmlCon"]; //获取输入内容
                    if (App_Code.ValidateInput.ValidateKeyWord(htmlCon))
                    {
                        context.Response.Write("{\"msg\":\" 发送失败！存在敏感词汇，请文明用语！ \"}");
                        context.Response.End();
                        return;
                    }
                    int ri=hwstuBll.StuSubmitHomeWork(hwID,userID,htmlCon);
                    if (ri==1)
                    {
                        context.Response.Write("{\"msg\":\"提交作业成功\"}");
                        context.Response.End();
                        return;
                    }
                    else
                    {
                        context.Response.Write("{\"msg\":\"提交作业失败\"}");
                        context.Response.End();
                        return;
                    }
                }
                else
                {
                    context.Response.Write("{\"msg\":\"数据库没有找到该记录\"}");
                    context.Response.End();
                    return;
                }
            }
        }
        #endregion

        #region 显示该学生最近的十次作业分数情况统计
        protected void GetStuScoreDefault(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取Session的值
            string type = HttpContext.Current.Request["type"]; //获取要显示的类型
            //根据学生ID获取该学生的前十次作业
            DataTable dthw = hwstuBll.GetTenScoreOfStuID(Convert.ToInt32(dt.Rows[0]["id"]));
            if (dthw.Rows.Count==0)
            {
                context.Response.Write("[]");
                context.Response.End();
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("[");
            //这里要获取作业的名称，作业的分数，作业的开始时间
            if (type == "stu")  //返回该学生的作业
            {
                for (int i = 0; i < dthw.Rows.Count; i++)
                {
                    Model.course_homework courhwModel = courhwBll.GetModel(Convert.ToInt32(dthw.Rows[i]["howstuId"]));
                    str.Append("[" + (i + 1) + ",");
                    str.Append(dthw.Rows[i]["hw_score"] + ",");
                    str.Append("\"" + courhwModel.homework_name + "\",");
                    str.Append("\"" + courhwModel.homework_beginTime.ToString("MM月dd号") + "\"");
                    str.Append("],");
                }
            }
            if (type == "all") //返回班级平均分
            {
                for (int i = 0; i < dthw.Rows.Count; i++)
                {
                    Model.course_homework courhwModel = courhwBll.GetModel(Convert.ToInt32(dthw.Rows[i]["howstuId"]));
                    str.Append("[" + (i + 1) + ",");
                    str.Append(courhwBll.GetAVGByhwID(Convert.ToInt32(dthw.Rows[i]["howstuId"])) + ",");
                    str.Append("\"" + courhwModel.homework_name + "\",");
                    str.Append("\"" + courhwModel.homework_beginTime.ToString("MM月dd号") + "\"");
                    str.Append("],");
                }
            }
            str.Append("]");
            context.Response.Write(str.Remove((str.Length - 2), 1));
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
            if (courseBll.Exists(courseID) && courstuBll.Exists(courseID, Convert.ToInt32(dt.Rows[0]["id"])))
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

        #region 显示学生未完成的作业数目
        protected void GetStuNoWorkNum(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取学生ID
            int ri = hwstuBll.GetStuNoWorkNum(userID);
            context.Response.Write("{\"num\":"+ri+"}");
            context.Response.End();
        }
        #endregion

        #region 显示学生已完成的作业数目
        protected void GetStuWorkNum(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dt.Rows[0]["id"]); //获取学生ID
            int ri = hwstuBll.GetStuWorkNum(userID);
            context.Response.Write("{\"num\":" + ri + "}");
            context.Response.End();
        }
        #endregion

        #region 学生首页显示的三条作业情况
        protected void GetAllLine(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取Session的值
            DataTable myHwDt = hwstuBll.GetStuHomeWorkForDefault(dt.Rows[0]["id"].ToString(), 3);
            StringBuilder str = new StringBuilder();
            if (myHwDt.Rows.Count==0)
            {
                context.Response.Write("{\"Homework\":[]}");
                context.Response.End();
                return;
            }
            str.Append("{\"Homework\":[");
            for (int i = 0; i < myHwDt.Rows.Count; i++)
            {
                str.Append("{\"id\":" + myHwDt.Rows[i][0] + ",");
                str.Append("\"homeworkID\":" + myHwDt.Rows[i][1] + ",");
                str.Append("\"courseName\":\"" + myHwDt.Rows[i][2] + "\",");
                str.Append("\"homeworkName\":\"" + myHwDt.Rows[i][3] + "\",");
                str.Append("\"homeworkTime\":\"" + ((DateTime)myHwDt.Rows[i][4]).ToString("yyyy-MM-dd") + "\",");
                str.Append("\"teacher\":\"" + userinfoBll.GetUserRealNameForID(Convert.ToInt32(myHwDt.Rows[i][5])) + "\",");
                str.Append("\"status\":" + myHwDt.Rows[i][6] + "},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
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