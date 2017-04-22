using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XGhms.Helper;

namespace XGhms.DAL
{
    /// <summary>
    /// 数据访问类：role
    /// </summary>
    public partial class role
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
            strSql.Append("select count(1) from xg_role");
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
            strSql.Append("delete from xg_role ");
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
        /// <param name="id">角色id</param>
        /// <returns>model对象</returns>
        public Model.role GetModel(int id)
        { 
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            Model.role model = new Model.role();
            DataSet ds = SQLHelper.SelectSqlReturnDataSet("role_SelectRoleById", parameters, CommandType.StoredProcedure);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["role_name"] != null && ds.Tables[0].Rows[0]["role_name"].ToString() != "")
                {
                    model.role_name = ds.Tables[0].Rows[0]["role_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["role_type"] != null && ds.Tables[0].Rows[0]["role_type"].ToString() != "")
                {
                    model.role_type = int.Parse(ds.Tables[0].Rows[0]["role_type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_sys"] != null && ds.Tables[0].Rows[0]["is_sys"].ToString() != "")
                {
                    model.is_sys = int.Parse(ds.Tables[0].Rows[0]["is_sys"].ToString());
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
        /// 根据角色名称返回角色ID
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>角色ID</returns>
        public int GetRoleIDByRoleName(string roleName)
        {
            string sql = "select id from xg_role where role_name=@role_name";
            SqlParameter[] parameters = {
					new SqlParameter("@role_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = roleName;
            return Convert.ToInt32(SQLHelper.GetSingle(sql,parameters));
        }
        #endregion
    }
}
