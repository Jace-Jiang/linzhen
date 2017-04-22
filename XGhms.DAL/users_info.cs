using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XGhms.Helper;
using System.Collections.Generic;

namespace XGhms.DAL
{
    /// <summary>
    /// 数据访问类：users_info
    /// </summary>
    public partial class users_info
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
            strSql.Append("select count(1) from xg_users_info");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return SQLHelper.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 检查该用户是否存在
        /// </summary>
        /// <param name="sname">姓名</param>
        /// <returns>true or false</returns>
        public bool Exists(string sname)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from xg_users_info");
            strSql.Append(" where real_name=@sname ");
            SqlParameter[] parameters = {
					new SqlParameter("@sname", SqlDbType.NVarChar,50)};
            parameters[0].Value = sname;
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
            strSql.Append("delete from xg_users_info ");
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
        /// 根据用户ID来删除记录
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>受影响的行数</returns>
        public int DeleteByUserID(int userID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from xg_users_info where user_id="+userID);
            return SQLHelper.ExecuteSql(strSql.ToString());
        }
        /// <summary>
        /// 查询id得到一个对象实体
        /// </summary>
        /// <param name="id">用户信息id</param>
        /// <returns>model对象</returns>
        public Model.users_info GetModel(int id)
        { 
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            Model.users_info model = new Model.users_info();
            DataSet ds = SQLHelper.SelectSqlReturnDataSet("users_info_SelectUserInfoById", parameters, CommandType.StoredProcedure);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_id"] != null && ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["role_id"] != null && ds.Tables[0].Rows[0]["role_id"].ToString() != "")
                {
                    model.role_id = int.Parse(ds.Tables[0].Rows[0]["role_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["real_name"] != null && ds.Tables[0].Rows[0]["real_name"].ToString() != "")
                {
                    model.real_name = ds.Tables[0].Rows[0]["real_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sex"] != null && ds.Tables[0].Rows[0]["sex"].ToString() != "")
                {
                    model.sex = int.Parse(ds.Tables[0].Rows[0]["sex"].ToString());
                }
                if (ds.Tables[0].Rows[0]["birthday"] != null && ds.Tables[0].Rows[0]["birthday"].ToString() != "")
                {
                    model.birthday =DateTime.Parse(ds.Tables[0].Rows[0]["birthday"].ToString());
                }
                if (ds.Tables[0].Rows[0]["telephone"] != null && ds.Tables[0].Rows[0]["telephone"].ToString() != "")
                {
                    model.telephone = ds.Tables[0].Rows[0]["telephone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["email"] != null && ds.Tables[0].Rows[0]["email"].ToString() != "")
                {
                    model.email = ds.Tables[0].Rows[0]["email"].ToString();
                }
                if (ds.Tables[0].Rows[0]["college_id"] != null && ds.Tables[0].Rows[0]["college_id"].ToString() != "")
                {
                    model.college_id = int.Parse(ds.Tables[0].Rows[0]["college_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["class_id"] != null && ds.Tables[0].Rows[0]["class_id"].ToString() != "")
                {
                    model.class_id = int.Parse(ds.Tables[0].Rows[0]["class_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["major"] != null && ds.Tables[0].Rows[0]["major"].ToString() != "")
                {
                    model.major = ds.Tables[0].Rows[0]["major"].ToString();
                }
                if (ds.Tables[0].Rows[0]["address"] != null && ds.Tables[0].Rows[0]["address"].ToString() != "")
                {
                    model.address = ds.Tables[0].Rows[0]["address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["explain"] != null && ds.Tables[0].Rows[0]["explain"].ToString() != "")
                {
                    model.explain = ds.Tables[0].Rows[0]["explain"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据用户ID获取真实姓名
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns>返回真实姓名</returns>
        public string GetUserRealNameForID(int UserID)
        {
            string sql = "select real_name from xg_users_info where user_id=" + UserID;
            object ob = SQLHelper.GetSingle(sql);
            if (ob==null)
            {
                return "";
            }
            else
            {
                return ob.ToString();
            }
        }
        /// <summary>
        /// 根据用户的ID获取model
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>model</returns>
        public Model.users_info GetModelByUserID(int userID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from xg_users_info");
            strSql.Append(" where user_id=@user_id");
            SqlParameter[] parameters = {
					new SqlParameter("@user_id", SqlDbType.Int,4)};
            parameters[0].Value = userID;
            object obj = SQLHelper.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }

        public IEnumerable<Model.users_info> GetModelListByName(string userName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select id from xg_users_info");
            sb.Append(" where real_name=@real_name");
            SqlParameter[] parameters = {
					new SqlParameter("@real_name", SqlDbType.NVarChar,50)};
            parameters[0].Value = userName;
            var modelList=new List<Model.users_info>();
            using(DataSet ds=SQLHelper.SelectSqlReturnDataSet(sb.ToString(),parameters,CommandType.Text))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(GetModel(Convert.ToInt32(ds.Tables[0].Rows[i]["id"])));
                }
                return modelList;
            }
        }
        /// <summary>
        /// 设置学生的班级id
        /// </summary>
        /// <param name="userID">学生ID</param>
        /// <param name="classID">班级ID</param>
        /// <returns>受影响的行数</returns>
        public int UpdateClassByUserID(int userID,int classID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [xg_users_info] ");
            if (classID==0)
            {
                str.Append("SET [class_id] =null" );
            }
            else
            {
                str.Append("SET [class_id] =" + classID);
            }
            str.Append(" WHERE user_id=" + userID);
            return SQLHelper.ExecuteSql(str.ToString());
        }
        /// <summary>
        /// 更新学生的学院和本机
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <param name="classID">班级ID</param>
        /// <returns>受影响的行数</returns>
        public int UpdateClassByUserID(int userID,string realName,int sex,int collegeID, int classID,string major)
        {
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [xg_users_info] ");
            str.Append("SET [real_name] =@real_name");
            str.Append(",[sex] =" + sex);
            str.Append(",[college_id] =" + collegeID);
            if (classID == 0)
            {
                str.Append(",[class_id] =null");
            }
            else
            {
                str.Append(",[class_id] =" + classID);
            }
            str.Append(",[major]=@major");
            str.Append(" WHERE user_id=" + userID);
            SqlParameter[] parameters = {
					new SqlParameter("@real_name", SqlDbType.NVarChar,50),
                                        new SqlParameter("@major",SqlDbType.NVarChar,50)};
            parameters[0].Value = realName;
            parameters[1].Value = major;
            return SQLHelper.ExecuteSql(str.ToString(),parameters);
        }
        /// <summary>
        /// 根据班级ID获取班上的所有人的名单，用于导出Excel表
        /// </summary>
        /// <param name="classID">班级ID</param>
        /// <returns>DataSet</returns>
        public DataSet GetAllStudentByClassID(int classID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select user_number as 学号,real_name as 姓名,college_name as 学院,major as 专业,class_name as 班级");
            str.Append(" from dbo.xg_classes,dbo.xg_college,dbo.xg_users,dbo.xg_users_info ");
            str.Append("where xg_users.id=xg_users_info.user_id and xg_users_info.class_id=xg_classes.id ");
            str.Append(" and xg_users_info.college_id=xg_college.id and xg_classes.id=" + classID);
            using (DataSet ds=SQLHelper.Query(str.ToString()))
            {
                return ds;
            }
        }
        /// <summary>
        /// 根据学生角色的用户列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <param name="classID">班级ID</param>
        /// <returns>用户列表</returns>
        public int GetTotalNumOFStuandClsandRol(int roleID, int collegeID, int classID)
        {
            StringBuilder str = new StringBuilder();
            if (collegeID==0&&classID==0)
            {
                str.Append("select count(id) from xg_users_info where role_id=" + roleID + " and college_id is NULL");
            }
            if (collegeID!=0&&classID==0)
            {
                str.Append("select count(id) from xg_users_info where role_id=" + roleID + " and college_id=" + collegeID + " and class_id is NULL");
            }
            if (collegeID!=0&&classID!=0)
            {
                str.Append("select count(id) from xg_users_info where role_id=" + roleID + " and college_id=" + collegeID + " and class_id="+classID);
            }
            return Convert.ToInt32(SQLHelper.GetSingle(str.ToString()));
        }
        /// <summary>
        /// 根据老师角色的用户列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <returns>用户列表数目</returns>
        public int GetTotalNumOfTerandCls(int roleID, int collegeID)
        {
            StringBuilder str = new StringBuilder();
            if (collegeID == 0)
            {
                str.Append("select count(id) from xg_users_info where role_id=" + roleID + " and college_id is NULL");
            }
            else 
            {
                str.Append("select count(id) from xg_users_info where role_id=" + roleID + " and college_id=" + collegeID);
            }
            return Convert.ToInt32(SQLHelper.GetSingle(str.ToString()));
        }
        /// <summary>
        /// 根据管理员角色获取用户列表
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>用户列表数目</returns>
        public int GetTotalNumOfAdm(int roleID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select count(id) from xg_users_info where role_id=" + roleID);
            return Convert.ToInt32(SQLHelper.GetSingle(str.ToString()));
        }
        /// <summary>
        /// 执行存储过程来显示分页
        /// </summary>
        /// <param name="ProcedureName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>显示xg_users_info表里面的id列表</returns>
        public DataTable GetPageOfRoleForUserList(string ProcedureName, SqlParameter[] parameters)
        {
            using (DataSet ds = SQLHelper.SelectSqlReturnDataSet(ProcedureName, parameters, CommandType.StoredProcedure))
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// 根据班级获取信息
        /// </summary>
        /// <param name="classID">班级ID</param>
        /// <returns>用户列表</returns>
        public IEnumerable<Model.users_info> GetUserListByClassID(int classID, int role_id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select id from xg_users_info");
            sb.Append(" where class_id=@class_id and role_id=" + role_id);
            SqlParameter[] parameters = {
					new SqlParameter("@class_id", SqlDbType.NVarChar,50)};
            parameters[0].Value = classID;
            var modelList = new List<Model.users_info>();
            using (DataSet ds = SQLHelper.SelectSqlReturnDataSet(sb.ToString(), parameters, CommandType.Text))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(GetModel(Convert.ToInt32(ds.Tables[0].Rows[i]["id"])));
                }
                return modelList;
            }
        }
        /// <summary>
        /// 根据学院ID获取所有的老师角色列表
        /// </summary>
        /// <param name="collegeID">学院ID</param>
        /// <param name="role_id">角色ID</param>
        /// <returns>老师列表</returns>
        public IEnumerable<Model.users_info> GetTerListByCollegeID(int collegeID, int role_id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select id from xg_users_info");
            sb.Append(" where college_id=@college_id and role_id=" + role_id);
            SqlParameter[] parameters = {
					new SqlParameter("@college_id", SqlDbType.NVarChar,50)};
            parameters[0].Value = collegeID;
            var modelList = new List<Model.users_info>();
            using (DataSet ds = SQLHelper.SelectSqlReturnDataSet(sb.ToString(), parameters, CommandType.Text))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(GetModel(Convert.ToInt32(ds.Tables[0].Rows[i]["id"])));
                }
                return modelList;
            }
        }
        /// <summary>
        /// 获取所有的管理员
        /// </summary>
        /// <param name="roleID">管理员ID列表</param>
        /// <returns>管理员列表</returns>
        public IEnumerable<Model.users_info> GetAdminList(int[] roleID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select id from xg_users_info");
            sb.Append(" where ");
            for (int i = 0; i < roleID.Length; i++)
            {
                sb.Append(" role_id="+roleID[i]);
                if (i==roleID.Length-1)
                {
                    
                }
                else
                {
                    sb.Append(" or ");
                }
            }
            var modelList = new List<Model.users_info>();
            using (DataSet ds = SQLHelper.Query(sb.ToString()))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(GetModel(Convert.ToInt32(ds.Tables[0].Rows[i]["id"])));
                }
                return modelList;
            }
        }
        /// <summary>
        /// 如果用户查看自己的信息，不存在时插入
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="roleID">角色ID</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="birthday">生日</param>
        /// <param name="telPhone">联系电话</param>
        /// <param name="email">电子邮件</param>
        /// <param name="address">地址</param>
        /// <param name="explain">简介</param>
        /// <returns>受影响的行数</returns>
        public int InsertUserInfoOfUserID(int userID, int roleID, string realName, string birthday, string telPhone, string email, string address, string explain)
        {
            StringBuilder str = new StringBuilder();
            str.Append("INSERT INTO [xg_users_info]");
            str.Append("([user_id],[role_id],[real_name],[sex],[birthday],[telephone],[email],[college_id],[class_id],[major],[address],[explain])");
            str.Append(" VALUES(" + userID);
            str.Append("," + roleID);
            str.Append(",@name");
            str.Append(",1");
            str.Append(",@birthday");
            str.Append(",@telphone");
            str.Append(",@email");
            str.Append(",NULL");
            str.Append(",NULL");
            str.Append(",NULL");
            str.Append(",@address");
            str.Append(",@explain");
            str.Append(")");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,50),
                                        new SqlParameter("@birthday",SqlDbType.DateTime),
                                        new SqlParameter("@telphone",SqlDbType.NVarChar),
                                        new SqlParameter("@email",SqlDbType.NVarChar),
                                        new SqlParameter("@address",SqlDbType.NVarChar),
                                        new SqlParameter("@explain",SqlDbType.NVarChar)};
            parameters[0].Value = realName;
            parameters[1].Value = birthday;
            parameters[2].Value = telPhone;
            parameters[3].Value = email;
            parameters[4].Value = address;
            parameters[5].Value = explain;
            return SQLHelper.ExecuteSql(str.ToString(), parameters);
        }
        /// <summary>
        /// 用户查看自己的信息，保存自己的信息
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="birthday">生日</param>
        /// <param name="telPhone">联系电话</param>
        /// <param name="email">Email</param>
        /// <param name="address">联系地址</param>
        /// <param name="explain">个人简介</param>
        /// <returns>受影响的行数</returns>
        public int UpdateUserInfoOfUserID(int id, string realName, string birthday, string telPhone, string email, string address, string explain)
        {
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [xg_users_info] ");
            str.Append("SET [real_name] =@name,");
            str.Append(" [birthday] =@birthday,");
            str.Append(" [telephone] =@telephone,");
            str.Append(" [email] =@email,");
            str.Append(" [address] =@address,");
            str.Append(" [explain] =@explain");
            str.Append(" WHERE id="+id);
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,50),
                    new SqlParameter("@birthday",SqlDbType.DateTime),
                    new SqlParameter("@telephone",SqlDbType.NVarChar),
                    new SqlParameter("@email",SqlDbType.NVarChar),
                    new SqlParameter("@address",SqlDbType.NVarChar),
                    new SqlParameter("@explain",SqlDbType.NVarChar)};
            parameters[0].Value = realName;
            parameters[1].Value = birthday;
            parameters[2].Value = telPhone;
            parameters[3].Value = email;
            parameters[4].Value = address;
            parameters[5].Value = explain;
            return SQLHelper.ExecuteSql(str.ToString(), parameters);
        }
        /// <summary>
        /// 更新用户，更新角色
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="roleID">角色ID</param>
        /// <returns>受影响的行数</returns>
        public int UpdateUserByIDForAdmin(int UserID, int roleID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [xg_users_info]");
            str.Append(" SET [role_id] = " + roleID);
            str.Append(" where user_id=" + UserID);
            return SQLHelper.ExecuteSql(str.ToString());
        }
        #endregion
    }
}
