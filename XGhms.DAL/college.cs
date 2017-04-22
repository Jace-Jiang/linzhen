using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XGhms.Helper;
using System.Collections.Generic;

namespace XGhms.DAL
{
    /// <summary>
    /// 数据访问类：college
    /// </summary>
    public partial class college
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
            strSql.Append("select count(1) from xg_college");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return SQLHelper.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 根据学院名称检查是否存在该记录
        /// </summary>
        /// <param name="id">学院名称</param>
        /// <returns>true or false</returns>
        public bool Exists(string collegeName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from xg_college");
            strSql.Append(" where college_name=@college_name ");
            SqlParameter[] parameters = {
					new SqlParameter("@college_name", SqlDbType.NVarChar,50)};
            parameters[0].Value = collegeName;
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
            strSql.Append("delete from xg_college ");
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
        /// <param name="id">学院id</param>
        /// <returns>model对象</returns>
        public Model.college GetModel(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            Model.college model = new Model.college();
            DataSet ds = SQLHelper.SelectSqlReturnDataSet("college_SelectCollegeById", parameters, CommandType.StoredProcedure);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["college_name"] != null && ds.Tables[0].Rows[0]["college_name"].ToString() != "")
                {
                    model.college_name = ds.Tables[0].Rows[0]["college_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["college_admin"] != null && ds.Tables[0].Rows[0]["college_admin"].ToString() != "")
                {
                    model.college_admin = ds.Tables[0].Rows[0]["college_admin"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 插入新的数据
        /// </summary>
        /// <param name="collegeName">学院名称</param>
        /// <param name="usersList">用户列表</param>
        /// <returns>受影响的行数</returns>
        public int InsertNewCollege(string collegeName,string usersList)
        {
            StringBuilder str = new StringBuilder();
            str.Append("INSERT INTO [dbo].[xg_college]");
            str.Append("([college_name],[college_admin])");
            str.Append(" VALUES");
            str.Append("(@college_name,@college_admin)");
            SqlParameter[] parameters = {
					new SqlParameter("@college_name", SqlDbType.NVarChar,50),
                                        new SqlParameter("@college_admin",SqlDbType.NVarChar,50)};
            parameters[0].Value = collegeName;
            parameters[1].Value = usersList;
            return SQLHelper.ExecuteSql(str.ToString(), parameters);
        }
        /// <summary>
        /// 根据学院ID来更新学院信息
        /// </summary>
        /// <param name="collegeName">学院名称</param>
        /// <param name="usersList">用户列表</param>
        /// <param name="collegeID">学院ID</param>
        /// <returns>受影响的行数</returns>
        public int UpdateCollegeByID(string collegeName, string usersList, int collegeID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [dbo].[xg_college] ");
            str.Append("SET [college_name] = @college_name,[college_admin] = @college_admin");
            str.Append(" where id=" + collegeID);
            SqlParameter[] parameters = {
					new SqlParameter("@college_name", SqlDbType.NVarChar,50),
                                        new SqlParameter("@college_admin",SqlDbType.NVarChar,50)};
            parameters[0].Value = collegeName;
            parameters[1].Value = usersList;
            return SQLHelper.ExecuteSql(str.ToString(), parameters);
        }
        #endregion

        #region 扩展方法=============================================
        /// <summary>
        /// 获取所有的学院部门列表
        /// </summary>
        /// <returns>model列表</returns>
        public IEnumerable<Model.college> GetAllCollegeList()
        {
            var modelList = new List<Model.college>();
            using (DataSet ds = SQLHelper.Query("SELECT [id],[college_name],[college_admin] FROM [dbo].[xg_college]"))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(GetModel(Convert.ToInt32(ds.Tables[0].Rows[i]["id"])));
                }
                return modelList;
            }
        }
        /// <summary>
        /// 根据学院名称获取学院ID
        /// </summary>
        /// <param name="collegeName">学院名称</param>
        /// <returns>学院ID</returns>
        public int GetCollegeIDByCollegeName(string collegeName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from xg_college ");
            strSql.Append(" where college_name=@college_name");
            SqlParameter[] parameters = {
					new SqlParameter("@college_name", SqlDbType.NVarChar,50)};
            parameters[0].Value = collegeName;
            return Convert.ToInt32(SQLHelper.GetSingle(strSql.ToString(),parameters));
        }
        #endregion
    }
}
