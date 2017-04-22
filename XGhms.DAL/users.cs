using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XGhms.Helper;

namespace XGhms.DAL
{
    /// <summary>
    /// 数据访问类：users
    /// </summary>
    public partial class users
    {
        #region 基本方法=============================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from xg_users");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return SQLHelper.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 是否存在该编号的用户
        /// </summary>
        /// <param name="id">编号（学号或者教工号）</param>
        /// <returns>true or false</returns>
        public bool Exists(string usernum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from xg_users");
            strSql.Append(" where user_number=@usernum ");
            SqlParameter[] parameters = {
					new SqlParameter("@usernum", SqlDbType.NVarChar,50)};
            parameters[0].Value = usernum;
            return SQLHelper.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 根据用户名和密码检查用户名是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>true or false</returns>
        public bool Exists(string userName,string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from xg_users");
            strSql.Append(" where user_name=@user_name and password=@password");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
                                        new SqlParameter("@password",SqlDbType.NVarChar,100)};
            parameters[0].Value = userName;
            parameters[1].Value = password;
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
            strSql.Append("delete from xg_users ");
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
        /// <param name="id">用户id</param>
        /// <returns>model对象</returns>
        public Model.users GetModel(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            Model.users model = new Model.users();
            DataSet ds = SQLHelper.SelectSqlReturnDataSet("users_SelectUserById", parameters, CommandType.StoredProcedure);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["role_id"] != null && ds.Tables[0].Rows[0]["role_id"].ToString() != "")
                {
                    model.role_id = int.Parse(ds.Tables[0].Rows[0]["role_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["role_type"] != null && ds.Tables[0].Rows[0]["role_type"].ToString() != "")
                {
                    model.role_type = int.Parse(ds.Tables[0].Rows[0]["role_type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_number"] != null && ds.Tables[0].Rows[0]["user_number"].ToString() != "")
                {
                    model.user_number = ds.Tables[0].Rows[0]["user_number"].ToString();
                }
                if (ds.Tables[0].Rows[0]["user_name"] != null && ds.Tables[0].Rows[0]["user_name"].ToString() != "")
                {
                    model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["password"] != null && ds.Tables[0].Rows[0]["password"].ToString() != "")
                {
                    model.password = ds.Tables[0].Rows[0]["password"].ToString();
                }
                if (ds.Tables[0].Rows[0]["is_lock"] != null && ds.Tables[0].Rows[0]["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(ds.Tables[0].Rows[0]["is_lock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"] != null && ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 扩展方法=============================================
        /// <summary>
        /// 根据用户名来判定是否存在该用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>true or false</returns>
        public bool ExistsByUserName(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from xg_users");
            strSql.Append(" where user_name=@user_name ");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = userName;
            return SQLHelper.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 根据用户名获取model
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户表的model</returns>
        public Model.users GetModelByUserName(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from xg_users");
            strSql.Append(" where user_name=@user_name");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = userName;
            object obj = SQLHelper.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }
        /// <summary>
        /// 根据用户编号获取model
        /// </summary>
        /// <param name="userNum">用户编号</param>
        /// <returns>用户表的model</returns>
        public Model.users GetModelByUserNum(string userNum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from xg_users");
            strSql.Append(" where user_number=@user_number");
            SqlParameter[] parameters = {
					new SqlParameter("@user_number", SqlDbType.NVarChar,50)};
            parameters[0].Value = userNum;
            object obj = SQLHelper.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }
        /// <summary>
        /// 根据用户ID获取用户的学号/教工号
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns>学号/教工号</returns>
        public string GetUserNumByUserID(int UserID)
        {
            string sql = "select user_number from xg_users where id=" + UserID;
            object obj = SQLHelper.GetSingle(sql);
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="pwd">新的密码</param>
        /// <returns>返回受影响的行数</returns>
        public int UpdateUserPwd(int UserID, string pwd)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update xg_users set password=@password");
            str.Append(" where id="+UserID);
            SqlParameter[] parameters = {
					new SqlParameter("@password", SqlDbType.NVarChar,100)};
            parameters[0].Value = pwd;
            return SQLHelper.ExecuteSql(str.ToString(), parameters);
        }

        /// <summary>
        /// 根据用户角色返回所有该角色的用户信息
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>返回该角色的所有用户</returns>
        public DataTable GetUserIDandNumByRoleID(int roleID)
        {
            string sql = "select id,user_number,user_name from xg_users where role_id="+roleID;
            using(DataSet ds=SQLHelper.Query(sql))
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// 根据角色和学院ID获取用户列表和真实姓名
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <returns>用户列表和真实姓名</returns>
        public DataTable GetHeadTeacherListByCollegeID(int roleID, int collegeID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select xg_users.id,xg_users_info.real_name from xg_users_info,xg_users ");
            str.Append(" where xg_users.id=xg_users_info.user_id and ");
            str.Append(" xg_users.role_id=" + roleID);
            str.Append(" and xg_users_info.college_id=" + collegeID);
            using (DataSet ds = SQLHelper.Query(str.ToString()))
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// 根据学号/工号获取用户的ID
        /// </summary>
        /// <param name="userNum">学号/工号</param>
        /// <returns>用户ID</returns>
        public int GetUserIDByUserNum(string userNum)
        {
            string sql = "select id from xg_users where user_number=@user_number";
            SqlParameter[] parameters = {
					new SqlParameter("@user_number", SqlDbType.NVarChar,50)};
            parameters[0].Value = userNum;
            return Convert.ToInt32(SQLHelper.GetSingle(sql,parameters));
        }
        /// <summary>
        /// 插入新的用户
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="userNum">学号/工号</param>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="isLock">是否锁定</param>
        /// <returns>产生的用户名</returns>
        public int InsertUserReturnID(int roleID,string userNum,string userName,string pwd,int isLock)
        {
            StringBuilder str = new StringBuilder();
            str.Append("INSERT INTO [xg_users]([role_id],[role_type],[user_number],[user_name],[password],[is_lock],[add_time])");
            str.Append(" VALUES(" + roleID);
            str.Append(",null");
            str.Append(",@userNum");
            str.Append(",@userName");
            str.Append(",@pwd");
            str.Append("," + isLock);
            str.Append(",getdate()");
            str.Append(") select @@identity");
            SqlParameter[] parameters = {
					new SqlParameter("@userNum", SqlDbType.NVarChar,50),
                                        new SqlParameter("@userName",SqlDbType.NVarChar,100),
                                        new SqlParameter("@pwd",SqlDbType.NVarChar,100)};
            parameters[0].Value = userNum;
            parameters[1].Value=userName;
            parameters[2].Value=pwd;
            int UserID=Convert.ToInt32(SQLHelper.GetSingle(str.ToString(), parameters));
            if (UserID>1)
            {
                string sql = "Insert INTO [xg_users_info](user_id,role_id) VALUES(" + UserID + "," + roleID + ")";
                if (SQLHelper.ExecuteSql(sql)==1)
                {
                    return UserID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据用户ID更新用户的角色和锁定状态
        /// </summary>
        /// <param name="UserID">用户id</param>
        /// <param name="roleID">角色ID</param>
        /// <param name="isLock">锁定状态</param>
        /// <returns>受影响的行数</returns>
        public int UpdateUserByIDForAdmin(int UserID,int roleID,int isLock)
        {
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [xg_users]");
            str.Append(" SET [role_id] = " + roleID);
            str.Append(",[is_lock]=" + isLock);
            str.Append(" where id="+UserID);
            return SQLHelper.ExecuteSql(str.ToString());
        }
        #endregion
    }
}
