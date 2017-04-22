using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace XGhms.BLL
{
    /// <summary>
    /// 用户消息管理
    /// </summary>
    public partial class user_message
    {
        DAL.user_message umesDal = new DAL.user_message();
        /// <summary>
        /// 根据消息ID递归查询所有的消息
        /// </summary>
        /// <param name="mesID">消息ID</param>
        /// <returns>消息的集合</returns>
        public IEnumerable<Model.user_message> GetMessageInfoBymesID(int mesID)
        {
            return umesDal.GetMessageInfoBymesID(mesID);
        }
        /// <summary>
        /// 根据用户ID和消息ID来查看该消息是否存在并且是否属于该用户
        /// </summary>
        /// <param name="id">消息id</param>
        /// <param name="userID">用户ID</param>
        /// <returns>是否属于</returns>
        public bool Exists(int id, int userID)
        {
            return umesDal.Exists(id, userID);
        }
        /// <summary>
        /// 消息列表的分页的存储过程，根据各种参数
        /// </summary>
        /// <param name="ProcedureName">存储过程名</param>
        /// <param name="parameters">参数</param>
        /// <returns>消息ID的DataTable</returns>
        public DataTable GetPageOfTypeForMsgList(string ProcedureName, SqlParameter[] parameters)
        {
            return umesDal.GetPageOfTypeForMsgList(ProcedureName, parameters);
        }
        /// <summary>
        /// 查询id得到一个对象实体
        /// </summary>
        /// <param name="id">消息id</param>
        /// <returns>model对象</returns>
        public Model.user_message GetModel(int id)
        {
            return umesDal.GetModel(id);
        }
        /// <summary>
        /// 根据sql语句获取分页的消息数目
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>该条件下的消息数目</returns>
        public int GetPageNum(string sql)
        {
            return umesDal.GetPageNum(sql);
        }
        /// <summary>
        /// 根据ID来删除相应的数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Delete(int id)
        {
            return umesDal.Delete(id);
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
            return umesDal.InsertByOtherID( type,  senderID,  receiverID,  msgCon,  last_id);
        }
        /// <summary>
        /// 新建插入消息
        /// </summary>
        /// <param name="type">发送者的类型</param>
        /// <param name="senderID">发送者</param>
        /// <param name="receiverID">接收者</param>
        /// <param name="msgCon">消息内容</param>
        /// <returns>受影响的行数</returns>
        public int InsertNewMsg(int type, int senderID, int receiverID, string msgCon)
        {
            return umesDal.InsertNewMsg(type, senderID, receiverID, msgCon);
        }
        /// <summary>
        /// 为管理员首页获取3条消息列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>消息ID</returns>
        public DataTable GetThreeMsg(int userID)
        {
            return umesDal.GetThreeMsg(userID);
        }
        /// <summary>
        /// 获取未读学生消息数目
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>消息数目</returns>
        public int GetStuMsgNumForDefault(int userID)
        {
            return umesDal.GetStuMsgNumForDefault(userID);
        }
        /// <summary>
        /// 获取未读系统消息数目
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>消息数目</returns>
        public int GetSysMsgNumForDefault(int userID)
        {
            return umesDal.GetSysMsgNumForDefault(userID);
        }
        /// <summary>
        /// 获取未读老师消息数目
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>消息数目</returns>
        public int GetTerMsgNumForDefault(int userID)
        {
            return umesDal.GetTerMsgNumForDefault(userID);
        }
        /// <summary>
        /// 获取未读管理员消息数目
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>消息数目</returns>
        public int GetAdmMsgNumForDefault(int userID)
        {
            return umesDal.GetAdmMsgNumForDefault(userID);
        }
        /// <summary>
        /// 获取飞系统消息的未读数目
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>消息数目</returns>
        public int GetNoSysMsgNum(int userID)
        {
            return umesDal.GetNoSysMsgNum(userID);
        }
        /// <summary>
        /// 读取消息后设置该消息为已读
        /// </summary>
        /// <param name="msgID">消息ID</param>
        /// <returns>受影响的行数</returns>
        public int updateMsgIsRead(int msgID)
        {
            return umesDal.updateMsgIsRead(msgID);
        }
        /// <summary>
        /// 根据消息ID和用户ID判断这条消息是否给这个用户
        /// </summary>
        /// <param name="msgID">消息ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>是否是给这个用户的消息</returns>
        public bool IsThisMsgToReciver(int msgID, int userID)
        {
            return umesDal.IsThisMsgToReciver(msgID, userID);
        }
    }
}
