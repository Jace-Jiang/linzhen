// Author: 陈旭东
// Create date: 2015-4-7
// Description:	这个一般处理程序专门处理站内消息
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.SessionState;
using System.Text;
using System.Data.SqlClient;
using XGhms.Helper;

namespace XGhms.Web.Handles
{
    /// <summary>
    /// MessageHandler 的摘要说明
    /// </summary>
    public class MessageHandler : IHttpHandler, IRequiresSessionState
    {
        BLL.role roleBll = new BLL.role();
        BLL.user_message uemesBll = new BLL.user_message();
        BLL.users_info userinfoBll = new BLL.users_info();
        BLL.users userBll = new BLL.users();
        BLL.college collegeBll = new BLL.college();
        BLL.classes classBll = new BLL.classes();
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
                case "GetMessageBymesID":
                    GetMessageBymesID(context);
                    break;
                case "GetUserMsgListCountNum":
                    GetUserMsgListCountNum(context);
                    break;
                case "GetUserMsgList":
                    GetUserMsgList(context);
                    break;
                case "deleteMsg":
                    deleteMsg(context);
                    break;
                case "InsertMsgByOtherID":
                    InsertMsgByOtherID(context);
                    break;
                case "GetCollegeList":
                    GetCollegeList(context);
                    break;
                case "GetAllClassByCollegeID":
                    GetAllClassByCollegeID(context);
                    break;
                case "GetStuListByClassID":
                    GetStuListByClassID(context);
                    break;
                case "GetTerListByCollegeID":
                    GetTerListByCollegeID(context);
                    break;
                case "GetAdminList":
                    GetAdminList(context);
                    break;
                case "InsertNewMsg":
                    InsertNewMsg(context);
                    break;
                case "GetThreeMsgForDefault":
                    GetThreeMsgForDefault(context);
                    break;
                case "GetStuMsgNumForDefault":
                    GetStuMsgNumForDefault(context);
                    break;
                case "GetTerMsgNumForDefault":
                    GetTerMsgNumForDefault(context);
                    break;
                case "GetAdmMsgNumForDefault":
                    GetAdmMsgNumForDefault(context);
                    break;
                case "GetSysMsgNumForDefault":
                    GetSysMsgNumForDefault(context);
                    break;
                case "GetNoSysMsgNum":
                    GetNoSysMsgNum(context);
                    break;
                default:
                    break;
            }
            #endregion
        }

        #region 根据id获取消息
        protected void GetMessageBymesID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dt = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int mesID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取消息ID,
            //首先检查该消息是否存在并且属于该用户
            if (uemesBll.Exists(mesID,Convert.ToInt32(dt.Rows[0]["id"])))
            {
                StringBuilder str = new StringBuilder();
                str.Append("{\"meslist\":[");
                foreach (Model.user_message model in uemesBll.GetMessageInfoBymesID(mesID))
                {
                    str.Append("{\"id\":" + model.id + ",");
                    str.Append("\"type\":" + model.type + ",");
                    str.Append("\"sender\":\"" + userinfoBll.GetUserRealNameForID(model.sender) + "\",");
                    str.Append("\"send_time\":\"" + model.send_time.ToString("yyyy年MM月dd日 hh:mm:ss") + "\",");
                    str.Append("\"mescontent\":\"" + HttpUtility.UrlEncodeUnicode(model.content).Replace("+", "%20") + "\","); //这里进行编码，客户端用escape()解码
                    if (model.sender==Convert.ToInt32(dt.Rows[0]["id"]))
                    {
                        str.Append("\"mesStatus\":0},");
                    }
                    if (model.receiver == Convert.ToInt32(dt.Rows[0]["id"]))
                    {
                        str.Append("\"mesStatus\":1},");
                    }
                }
                str.Append("]}");
                context.Response.Write(str.Remove((str.Length - 3), 1));
            }
            else
            {
                context.Response.Write("false");
                context.Response.End();
                return;
            }
        }
        #endregion

        #region 根据查询条件获取消息的数目用于分页
        protected void GetUserMsgListCountNum(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string msgCondition = HttpContext.Current.Request["msgCondition"]; //获取消息的条件 （已发送 or 已接收）
            string msgType = HttpContext.Current.Request["msgType"]; //获取消息的类型 （全部、用户 or 系统）
            int pageNow = Convert.ToInt32(HttpContext.Current.Request["nowPageNum"]); //获取当前的页码
            DataTable dtUser = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dtUser.Rows[0]["id"]);
            int totalNum = 0;
            if (msgCondition == "Receive") //表示接收到的消息列表
            {
                if (msgType == "all") //表示所有的消息
                {
                    string sql = "select count(id) from xg_user_message where receiver="+userID;
                    totalNum = uemesBll.GetPageNum(sql);
                }
                if (msgType == "student") //表示对象为学生的消息
                {
                    string sql = "select count(id) from xg_user_message where xg_user_message.type = 1 and receiver=" + userID;
                    totalNum = uemesBll.GetPageNum(sql);
                }
                if (msgType == "teacher") //表示对象为老师的消息
                {
                    string sql = "select count(id) from xg_user_message where xg_user_message.type = 2 and receiver=" + userID;
                    totalNum = uemesBll.GetPageNum(sql);
                }
                if (msgType == "admin") //表示对象为管理员的消息
                {
                    string sql = "select count(id) from xg_user_message where xg_user_message.type = 3 and receiver=" + userID;
                    totalNum = uemesBll.GetPageNum(sql);
                }
                if (msgType == "system") //表示对象为系统的消息
                {
                    string sql = "select count(id) from xg_user_message where xg_user_message.type = 0 and receiver=" + userID;
                    totalNum = uemesBll.GetPageNum(sql);
                }
            }
            if (msgCondition == "Send") //表示已发送的消息列表
            {
                if (msgType == "all") //表示所有的消息
                {
                    string sql = "select count(id) from xg_user_message where sender=" + userID;
                    totalNum = uemesBll.GetPageNum(sql);
                }
                if (msgType == "student") //表示对象为学生的消息
                {
                    string sql = "select count(id) from xg_user_message where xg_user_message.type = 1 and sender=" + userID;
                    totalNum = uemesBll.GetPageNum(sql);
                }
                if (msgType == "teacher") //表示对象为老师的消息
                {
                    string sql = "select count(id) from xg_user_message where xg_user_message.type = 2 and sender=" + userID;
                    totalNum = uemesBll.GetPageNum(sql);
                }
                if (msgType == "admin") //表示对象为管理员的消息
                {
                    string sql = "select count(id) from xg_user_message where xg_user_message.type = 3 and sender=" + userID;
                    totalNum = uemesBll.GetPageNum(sql);
                }
                if (msgType == "system") //表示对象为系统的消息
                {
                    totalNum = 0;
                }
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

        #region 获取用户消息，用于分页
        protected void GetUserMsgList(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string msgCondition = HttpContext.Current.Request["msgCondition"]; //获取消息的条件 （已发送 or 已接收）
            string msgType = HttpContext.Current.Request["msgType"]; //获取消息的类型 （全部、用户 or 系统）
            int pageNow = Convert.ToInt32(HttpContext.Current.Request["nowPageNum"]); //获取当前的页码
            DataTable dtUser = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dtUser.Rows[0]["id"]);
            DataTable dt = new DataTable();
            if (msgCondition=="Receive") //表示接收到的消息列表
            {
                if (msgType=="all") //表示所有的消息
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@receiverID", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = userID;
                    parameters[1].Value = ((pageNow - 1) * 10 + 1);
                    parameters[2].Value = pageNow * 10;
                    dt = uemesBll.GetPageOfTypeForMsgList("user_message_SelMsgListAllRecive", parameters);
                }
                if (msgType=="student") //表示对象为学生的消息
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@receiver", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = userID;
                    parameters[1].Value = ((pageNow - 1) * 10 + 1);
                    parameters[2].Value = pageNow * 10;
                    dt = uemesBll.GetPageOfTypeForMsgList("user_message_SelMsgListStuReceive", parameters);
                }
                if (msgType == "teacher") //表示对象为老师的消息
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@receiver", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = userID;
                    parameters[1].Value = ((pageNow - 1) * 10 + 1);
                    parameters[2].Value = pageNow * 10;
                    dt = uemesBll.GetPageOfTypeForMsgList("user_message_SelMsgListTerReceive", parameters);
                }
                if (msgType == "admin") //表示对象为管理员的消息
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@receiver", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = userID;
                    parameters[1].Value = ((pageNow - 1) * 10 + 1);
                    parameters[2].Value = pageNow * 10;
                    dt = uemesBll.GetPageOfTypeForMsgList("user_message_SelMsgListAdmReceive", parameters);
                }
                if (msgType == "system") //表示对象为系统的消息
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@receiver", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = userID;
                    parameters[1].Value = ((pageNow - 1) * 10 + 1);
                    parameters[2].Value = pageNow * 10;
                    dt = uemesBll.GetPageOfTypeForMsgList("user_message_SelMsgListSysReceive", parameters);
                }
            }
            if (msgCondition == "Send") //表示已发送的消息列表
            {
                if (msgType == "all") //表示所有的消息
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@sender", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = userID;
                    parameters[1].Value = ((pageNow - 1) * 10 + 1);
                    parameters[2].Value = pageNow * 10;
                    dt = uemesBll.GetPageOfTypeForMsgList("user_message_SelMsgListAllSend", parameters);
                }
                if (msgType == "student") //表示对象为学生的消息
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@sender", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = userID;
                    parameters[1].Value = ((pageNow - 1) * 10 + 1);
                    parameters[2].Value = pageNow * 10;
                    dt = uemesBll.GetPageOfTypeForMsgList("user_message_SelMsgListStuSend", parameters);
                }
                if (msgType == "teacher") //表示对象为老师的消息
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@sender", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = userID;
                    parameters[1].Value = ((pageNow - 1) * 10 + 1);
                    parameters[2].Value = pageNow * 10;
                    dt = uemesBll.GetPageOfTypeForMsgList("user_message_SelMsgListTerSend", parameters);
                }
                if (msgType == "admin") //表示对象为管理员的消息
                {
                    SqlParameter[] parameters = {
					    new SqlParameter("@sender", SqlDbType.Int,6),
                        new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                        new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
                    parameters[0].Value = userID;
                    parameters[1].Value = ((pageNow - 1) * 10 + 1);
                    parameters[2].Value = pageNow * 10;
                    dt = uemesBll.GetPageOfTypeForMsgList("user_message_SelMsgListAdmSend", parameters);
                }
                if (msgType == "system") //表示对象为系统的消息
                {
                    dt = null;
                }
            }
            if (dt.Rows.Count == 0)
            {
                context.Response.Write("{\"msgList\":[]}");
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{\"msgList\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Model.user_message umsgModel = uemesBll.GetModel(Convert.ToInt32(dt.Rows[i]["id"]));
                Model.users_info userinfoModel = userinfoBll.GetModel(Convert.ToInt32(dt.Rows[i]["id"]));
                
                str.Append("{\"id\":" + umsgModel.id + ",");
                if (umsgModel.receiver!=userID)
                {
                    Model.users userModel = userBll.GetModel(umsgModel.receiver);
                    str.Append("\"userName\":\"" + userModel.user_name + "\",");
                }
                if (umsgModel.sender!=userID)
                {
                    Model.users userModel = userBll.GetModel(umsgModel.sender);
                    str.Append("\"userName\":\"" + userModel.user_name + "\",");
                }
                str.Append("\"msgCon\":\"" + HttpUtility.UrlEncodeUnicode(Utils.KillHtml(umsgModel.content)).Replace("+", "%20") + "\",");
                str.Append("\"msgTime\":\"" + umsgModel.send_time.ToString("yyyy-MM-dd HH:mm") + "\",");
                str.Append("\"msgRead\":" + umsgModel.is_read + "");
                str.Append("},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 根据消息ID删除该消息
        protected void deleteMsg(HttpContext context) 
        {
            context.Response.ContentType = "text/plain";
            int id = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取消息id
            if (uemesBll.Delete(id))
            {
                context.Response.Write("{\"msg\":\" 删除成功 \"}");
                return;
            }
            else
            {
                context.Response.Write("{\"msg\":\" 删除失败 \"}");
                return;
            }
        }
        #endregion

        #region 回复消息的方法
        protected void InsertMsgByOtherID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取消息id
            string msgCon = HttpContext.Current.Request["msgCon"]; //获取消息内容
            DataTable dtUser = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dtUser.Rows[0]["id"]);
            Model.user_message msgModel = uemesBll.GetModel(id);
            int reciveID=0;
            if (msgModel.sender==userID)
            {
                reciveID = msgModel.receiver;
            }
            if (msgModel.receiver==userID)
            {
                reciveID = msgModel.sender;
            }
            if (msgModel.sender!=userID&&msgModel.receiver!=userID)
            {
                context.Response.Write("{\"msg\":\" 你没有权限回复此消息 \"}");
                context.Response.End();
                return;
            }
            if (App_Code.ValidateInput.ValidateKeyWord(msgCon))
            {
                context.Response.Write("{\"msg\":\" 发送失败！存在敏感词汇，请文明用语！ \"}");
                context.Response.End();
                return;
            }
            int ri=uemesBll.InsertByOtherID(GetTypeOfRole(dtUser.Rows[0]["role_name"].ToString()),userID,reciveID,msgCon,id);
            context.Response.Write("{\"msg\":\" 回复成功 \"}");
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

        #region 根据班级获取学生列表
        protected void GetStuListByClassID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int classID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取班级id
            IEnumerable<Model.users_info> modelList = userinfoBll.GetUserListByClassID(classID,roleBll.GetRoleIDByRoleName("Student"));
            if (((System.Collections.Generic.List<XGhms.Model.users_info>)modelList).Count == 0)
            {
                context.Response.Write("{\"stu\":[]}");
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{\"stu\":[");
            foreach (Model.users_info model in modelList)
            {
                str.Append("{\"id\":" + model.user_id + ",");
                str.Append("\"value\":\"" + model.real_name + "\"},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 根据学院获取所有的老师列表
        protected void GetTerListByCollegeID(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int collegeID = Convert.ToInt32(HttpContext.Current.Request["id"]); //获取学院id
            IEnumerable<Model.users_info> modelListTer = userinfoBll.GetTerListByCollegeID(collegeID, roleBll.GetRoleIDByRoleName("Teacher"));
            IEnumerable<Model.users_info> modelListHed = userinfoBll.GetTerListByCollegeID(collegeID, roleBll.GetRoleIDByRoleName("HeadTeacher"));
            if (((System.Collections.Generic.List<XGhms.Model.users_info>)modelListTer).Count == 0 && ((System.Collections.Generic.List<XGhms.Model.users_info>)modelListHed).Count == 0)
            {
                context.Response.Write("{\"ter\":[]}");
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{\"ter\":[");
            foreach (Model.users_info model in modelListTer)
            {
                str.Append("{\"id\":" + model.user_id + ",");
                str.Append("\"value\":\"" + model.real_name + "\"},");
            }
            foreach (Model.users_info model in modelListHed)
            {
                str.Append("{\"id\":" + model.user_id + ",");
                str.Append("\"value\":\"" + model.real_name + "\"},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 获取所有的管理员列表
        protected void GetAdminList(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int[] admRoleIDList=new int[3];
            admRoleIDList[0] = roleBll.GetRoleIDByRoleName("CollegeAdmin");
            admRoleIDList[1] = roleBll.GetRoleIDByRoleName("Admin");
            admRoleIDList[2] = roleBll.GetRoleIDByRoleName("Administrator");
            IEnumerable<Model.users_info> modelListTer = userinfoBll.GetAdminList(admRoleIDList);
            if (((System.Collections.Generic.List<XGhms.Model.users_info>)modelListTer).Count == 0)
            {
                context.Response.Write("{\"adm\":[]}");
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{\"adm\":[");
            foreach (Model.users_info model in modelListTer)
            {
                str.Append("{\"id\":" + model.user_id + ",");
                str.Append("\"value\":\"" + model.real_name + "\"},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 发送新的消息
        protected void InsertNewMsg(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string msgCon = HttpContext.Current.Request["msgCon"]; //获取消息内容
            if (App_Code.ValidateInput.ValidateKeyWord(msgCon))
            {
                context.Response.Write("{\"msg\":\" 发送失败！存在敏感词汇，请文明用语！ \"}");
                context.Response.End();
                return;
            }
            string userList = HttpContext.Current.Request["userList"]; //获取用户列表
            DataTable dtUser = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dtUser.Rows[0]["id"]);
            string[] s = userList.Split(new char[] { ',' });
            int succes = 0;
            int error = 0;
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (uemesBll.InsertNewMsg(GetTypeOfRole(dtUser.Rows[0]["role_name"].ToString()), userID, Convert.ToInt32(s[i]), msgCon)==1)
                {
                    succes = succes + 1;
                }
                else
                {
                    error = error + 1;
                }
            }
            context.Response.Write("{\"msg\":\"成功" + succes + "个，失败" + error + "个\"}");
        }
        #endregion

        #region 获取最近三条消息
        public void GetThreeMsgForDefault(HttpContext context)
        {
            DataTable dtUser = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dtUser.Rows[0]["id"]);
            DataTable dt = uemesBll.GetThreeMsg(userID);
            StringBuilder str = new StringBuilder();
            str.Append("{\"msgList\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Model.user_message umsgModel = uemesBll.GetModel(Convert.ToInt32(dt.Rows[i]["id"]));
                Model.users_info userinfoModel = userinfoBll.GetModel(Convert.ToInt32(dt.Rows[i]["id"]));

                str.Append("{\"id\":" + umsgModel.id + ",");
                if (umsgModel.receiver != userID)
                {
                    Model.users userModel = userBll.GetModel(umsgModel.receiver);
                    str.Append("\"userName\":\"" + userModel.user_name + "\",");
                }
                if (umsgModel.sender != userID)
                {
                    Model.users userModel = userBll.GetModel(umsgModel.sender);
                    str.Append("\"userName\":\"" + userModel.user_name + "\",");
                }
                str.Append("\"msgCon\":\"" + HttpUtility.UrlEncodeUnicode(umsgModel.content).Replace("+", "%20") + "\",");
                str.Append("\"msgTime\":\"" + umsgModel.send_time.ToString("yyyy-MM-dd HH:mm") + "\",");
                str.Append("\"msgRead\":" + umsgModel.is_read + "");
                str.Append("},");
            }
            str.Append("]}");
            context.Response.Write(str.Remove((str.Length - 3), 1));
        }
        #endregion

        #region 获取学生类型的消息未读数目
        public void GetStuMsgNumForDefault(HttpContext context)
        {
            DataTable dtUser = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dtUser.Rows[0]["id"]);
            int num = uemesBll.GetStuMsgNumForDefault(userID);
            context.Response.Write("{\"num\":" + num + "}");
        }
        #endregion

        #region 获取老师类型的消息未读数目
        public void GetTerMsgNumForDefault(HttpContext context)
        {
            DataTable dtUser = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dtUser.Rows[0]["id"]);
            int num = uemesBll.GetTerMsgNumForDefault(userID);
            context.Response.Write("{\"num\":" + num + "}");
        }
        #endregion

        #region 获取管理员类型的消息未读数目
        public void GetAdmMsgNumForDefault(HttpContext context)
        {
            DataTable dtUser = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dtUser.Rows[0]["id"]);
            int num = uemesBll.GetAdmMsgNumForDefault(userID);
            context.Response.Write("{\"num\":" + num + "}");
        }
        #endregion

        #region 获取系统类型的消息未读数目
        public void GetSysMsgNumForDefault(HttpContext context)
        {
            DataTable dtUser = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dtUser.Rows[0]["id"]);
            int num = uemesBll.GetSysMsgNumForDefault(userID);
            context.Response.Write("{\"num\":" + num + "}");
        }
        #endregion

        #region 获取非系统类型的消息未读数目
        public void GetNoSysMsgNum(HttpContext context)
        {
            DataTable dtUser = (DataTable)context.Session["UserInfo"]; //获取Session的值
            int userID = Convert.ToInt32(dtUser.Rows[0]["id"]);
            int num = uemesBll.GetNoSysMsgNum(userID);
            int sysNum = uemesBll.GetSysMsgNumForDefault(userID);
            context.Response.Write("{\"num\":" + (num - sysNum) + "}");
        }
        #endregion

        /// <summary>
        /// 内置方法，根据用户角色获取消息的type
        /// </summary>
        /// <param name="roleName">角色name</param>
        /// <returns>消息的类型type</returns>
        public int GetTypeOfRole(string roleName)
        {
            if (roleName == "Student")
            {
                return 1;
            }
            else if (roleName == "Teacher")
            {
                return 2;
            }
            else if (roleName == "HeadTeacher")
            {
                return 2;
            }
            else if (roleName == "CollegeAdmin")
            {
                return 3;
            }
            else if (roleName == "Admin")
            {
                return 3;
            }
            else if (roleName == "Administrator")
            {
                return 3;
            }
            else
            {
                return 0;
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