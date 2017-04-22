using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XGhms.Helper;
using System.Collections.Generic;

namespace XGhms.DAL
{
    /// <summary>
    /// 数据访问类:user_message
    /// </summary>
    public partial class user_message
    {
        #region 基本方法=============================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from xg_user_message");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return SQLHelper.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 根据ID来删除相应的数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from xg_user_message ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            int rows = SQLHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询id得到一个对象实体
        /// </summary>
        /// <param name="id">消息id</param>
        /// <returns>model对象</returns>
        public Model.user_message GetModel(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            Model.user_message model = new Model.user_message();
            DataSet ds = SQLHelper.SelectSqlReturnDataSet("user_message_SelectMessageById", parameters, CommandType.StoredProcedure);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["type"] != null && ds.Tables[0].Rows[0]["type"].ToString() != "")
                {
                    model.type = int.Parse(ds.Tables[0].Rows[0]["type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sender"] != null && ds.Tables[0].Rows[0]["sender"].ToString() != "")
                {
                    model.sender = int.Parse(ds.Tables[0].Rows[0]["sender"].ToString());
                }
                if (ds.Tables[0].Rows[0]["receiver"] != null && ds.Tables[0].Rows[0]["receiver"].ToString() != "")
                {
                    model.receiver = int.Parse(ds.Tables[0].Rows[0]["receiver"].ToString());
                }
                if (ds.Tables[0].Rows[0]["content"] != null && ds.Tables[0].Rows[0]["content"].ToString() != "")
                {
                    model.content = ds.Tables[0].Rows[0]["content"].ToString();
                }
                if (ds.Tables[0].Rows[0]["is_read"] != null && ds.Tables[0].Rows[0]["is_read"].ToString() != "")
                {
                    model.is_read = int.Parse(ds.Tables[0].Rows[0]["is_read"].ToString());
                }
                if (ds.Tables[0].Rows[0]["send_time"] != null && ds.Tables[0].Rows[0]["send_time"].ToString() != "")
                {
                    model.send_time = DateTime.Parse(ds.Tables[0].Rows[0]["send_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["read_time"] != null && ds.Tables[0].Rows[0]["read_time"].ToString() != "")
                {
                    model.read_time = DateTime.Parse(ds.Tables[0].Rows[0]["read_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["last_id"] != null && ds.Tables[0].Rows[0]["last_id"].ToString() != "")
                {
                    model.last_id = int.Parse(ds.Tables[0].Rows[0]["last_id"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据消息ID递归查询所有的消息
        /// </summary>
        /// <param name="mesID">消息ID</param>
        /// <returns>消息的集合</returns>
        public IEnumerable<Model.user_message> GetMessageInfoBymesID(int mesID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@mesID", SqlDbType.Int,6)};
            parameters[0].Value = mesID;
            var modelList=new List<Model.user_message>();
            using (DataSet ds = SQLHelper.SelectSqlReturnDataSet("user_message_SelectMessageListBymesID", parameters, CommandType.StoredProcedure))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(GetModel(Convert.ToInt32(ds.Tables[0].Rows[i]["id"])));
                }
                return modelList;
            }
        }
        /// <summary>
        /// 根据用户ID和消息ID来查看该消息是否存在并且是否属于该用户
        /// </summary>
        /// <param name="id">消息id</param>
        /// <param name="userID">用户ID</param>
        /// <returns>是否属于</returns>
        public bool Exists(int id, int userID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from (select sender,receiver from xg_user_message where id=@id)as T where  sender=@userID or receiver=@userID");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
                                        new SqlParameter("@userID",SqlDbType.Int,4)};
            parameters[0].Value = id;
            parameters[1].Value = userID;
            return SQLHelper.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 消息列表的分页的存储过程，根据各种参数
        /// </summary>
        /// <param name="ProcedureName">存储过程名</param>
        /// <param name="parameters">参数</param>
        /// <returns>消息ID的DataTable</returns>
        public DataTable GetPageOfTypeForMsgList(string ProcedureName, SqlParameter[] parameters)
        {
            using (DataSet ds = SQLHelper.SelectSqlReturnDataSet(ProcedureName, parameters, CommandType.StoredProcedure))
            {
                return ds.Tables[0];
            }
        }
        #endregion

        #region 扩展方法=============================================
        /// <summary>
        /// 根据sql语句获取分页的消息数目
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>该条件下的消息数目</returns>
        public int GetPageNum(string sql)
        {
            object ob = SQLHelper.GetSingle(sql);
            if (ob==null)
            {
                return 0;
            }
            return Convert.ToInt32(ob);
        }
        /// <summary>
        /// 回复消息
        /// </summary>
        /// <param name="type">发送方的类型</param>
        /// <param name="senderID">发送者ID</param>
        /// <param name="receiverID">接收者ID</param>
        /// <param name="msgCon">消息内容</param>
        /// <param name="last_id">上次的消息</param>
        /// <returns>受影响的行数</returns>
        public int InsertByOtherID(int type, int senderID, int receiverID, string msgCon, int last_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@type", SqlDbType.Int,6),
                    new SqlParameter("@sender",SqlDbType.Int,6),
                    new SqlParameter("@receiver",SqlDbType.Int,6),
                    new SqlParameter("@content",SqlDbType.NText),
                    new SqlParameter("@last_id",SqlDbType.TinyInt)
                                        };
            parameters[0].Value = type;
            parameters[1].Value = senderID;
            parameters[2].Value = receiverID;
            parameters[3].Value = msgCon;
            parameters[4].Value = last_id;
            return SQLHelper.ExecuteSql("user_message_InsertByOtherID", parameters);
        }
        /// <summary>
        /// 新建插入消息
        /// </summary>
        /// <param name="type">发送者的类型</param>
        /// <param name="senderID">发送者</param>
        /// <param name="receiverID">接收者</param>
        /// <param name="msgCon">消息内容</param>
        /// <returns>受影响的行数</returns>
        public int InsertNewMsg(int type,int senderID,int receiverID,string msgCon)
        {
            StringBuilder str = new StringBuilder();
            str.Append("INSERT INTO [xg_user_message] ");
            str.Append("([type],[sender],[receiver],[content],[is_read],[send_time],[read_time],[last_id])");
            str.Append(" VALUES(" + type);
            str.Append("," + senderID);
            str.Append("," + receiverID);
            str.Append(",@content");
            str.Append(",0");
            str.Append(",getdate()");
            str.Append(",NULL");
            str.Append(",0)");
            SqlParameter[] parameters = {
					new SqlParameter("@content", SqlDbType.NText)};
            parameters[0].Value=msgCon;
            return SQLHelper.ExecuteSql(str.ToString(), parameters);
        }
        /// <summary>
        /// 为管理员首页获取3条消息列表（管理员专用）
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>消息ID</returns>
        public DataTable GetThreeMsg(int userID)
        {
            string sql = "select top 3 id from xg_user_message where receiver=" + userID + " order by is_read,id desc";
            using (DataSet ds=SQLHelper.Query(sql))
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// 获取未读学生消息数目
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>消息数目</returns>
        public int GetStuMsgNumForDefault(int userID)
        {
            string sql = "select count(id) from xg_user_message where receiver=" + userID + " and is_read=0 and type=1";
            object obj = SQLHelper.GetSingle(sql);
            if (obj==null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 获取未读系统消息数目
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>消息数目</returns>
        public int GetSysMsgNumForDefault(int userID)
        {
            string sql = "select count(id) from xg_user_message where receiver=" + userID + " and is_read=0 and type=0";
            object obj = SQLHelper.GetSingle(sql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 获取未读老师消息数目
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>消息数目</returns>
        public int GetTerMsgNumForDefault(int userID)
        {
            string sql = "select count(id) from xg_user_message where receiver=" + userID + " and is_read=0 and type=2";
            object obj = SQLHelper.GetSingle(sql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 获取未读管理员消息数目
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>消息数目</returns>
        public int GetAdmMsgNumForDefault(int userID)
        {
            string sql = "select count(id) from xg_user_message where receiver=" + userID + " and is_read=0 and type=3";
            object obj = SQLHelper.GetSingle(sql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 获取飞系统消息的未读数目
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>消息数目</returns>
        public int GetNoSysMsgNum(int userID)
        {
            string sql = "select count(id) from xg_user_message where receiver=" + userID + " and is_read=0";
            object obj = SQLHelper.GetSingle(sql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return (Convert.ToInt32(obj) - GetSysMsgNumForDefault(userID));
            }
        }
        /// <summary>
        /// 读取消息后设置该消息为已读
        /// </summary>
        /// <param name="msgID">消息ID</param>
        /// <returns>受影响的行数</returns>
        public int updateMsgIsRead(int msgID)
        {
            string sql = "UPDATE [xg_user_message] SET [is_read] = 1,[read_time] = GETDATE() WHERE id=" + msgID;
            return SQLHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 根据消息ID和用户ID判断这条消息是否给这个用户
        /// </summary>
        /// <param name="msgID">消息ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>是否是给这个用户的消息</returns>
        public bool IsThisMsgToReciver(int msgID,int userID)
        {
            string sql = "select count(1) from xg_user_message where id=" + msgID + " and receiver="+userID;
            return SQLHelper.Exists(sql);
        }
        #endregion
    }
}
