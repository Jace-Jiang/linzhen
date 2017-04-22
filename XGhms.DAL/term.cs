using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XGhms.Helper;
using System.Collections.Generic;
namespace XGhms.DAL
{
    /// <summary>
    /// 数据访问类：term
    /// </summary>
    public partial class term
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
            strSql.Append("select count(1) from xg_term");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return SQLHelper.Exists(strSql.ToString(), parameters);
        }
        public bool Exists(string termName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from xg_term");
            strSql.Append(" where term_name=@term_name ");
            SqlParameter[] parameters = {
					new SqlParameter("@term_name", SqlDbType.NVarChar,50)};
            parameters[0].Value = termName;
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
            strSql.Append("delete from xg_term ");
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
        public int Insert(string termName)
        {
            string sql = "INSERT INTO [xg_term]([term_name]) VALUES('" + termName + "')";
            return SQLHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 查询id得到一个对象实体
        /// </summary>
        /// <param name="id">学期id</param>
        /// <returns>model对象</returns>
        public Model.term GetModel(int id)
        { 
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            Model.term model = new Model.term();
            DataSet ds = SQLHelper.SelectSqlReturnDataSet("term_SelectTermById", parameters, CommandType.StoredProcedure);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["term_name"] != null && ds.Tables[0].Rows[0]["term_name"].ToString() != "")
                {
                    model.term_name = ds.Tables[0].Rows[0]["term_name"].ToString();
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
        /// 管理员专用方法，根据id获取学期名称
        /// </summary>
        /// <param name="id">学期id</param>
        /// <returns>学期名称</returns>
        public string GetTermNameByID(int id)
        {
            //首先检查当前学期是否存在
            string nowTrem = Utils.GetNowTerm(DateTime.Today.Year, DateTime.Today.Month);
            string nextTrem = Utils.GetNextTerm(DateTime.Today.Year, DateTime.Today.Month);
            if (Exists(nowTrem))
            {
                if (Exists(nextTrem))
                {
                    return GetModel(id).term_name;
                }
                else
                {
                    Insert(nextTrem);
                    return GetModel(id).term_name;
                }
            }
            else
            {
                Insert(nowTrem);
                return GetModel(id).term_name;
            }
        }
        /// <summary>
        /// 获取所有的学期（管理员专用）
        /// </summary>
        /// <returns>modellist</returns>
        public IEnumerable<Model.term> GetAllTrem()
        {
            //首先检查当前学期是否存在
            string nowTrem = Utils.GetNowTerm(DateTime.Today.Year, DateTime.Today.Month);
            string nextTrem = Utils.GetNextTerm(DateTime.Today.Year, DateTime.Today.Month);
            var modelList = new List<Model.term>();
            if (Exists(nowTrem))
            {
                if (Exists(nextTrem))
                {
                    using (DataSet ds = SQLHelper.Query("select top 14 * from [xg_term]"))
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            modelList.Add(GetModel(Convert.ToInt32(ds.Tables[0].Rows[i]["id"])));
                        }
                        return modelList;
                    }
                }
                else
                {
                    Insert(nextTrem);
                    using (DataSet ds = SQLHelper.Query("select top 12 * from [xg_term]"))
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            modelList.Add(GetModel(Convert.ToInt32(ds.Tables[0].Rows[i]["id"])));
                        }
                        return modelList;
                    }
                }
            }
            else
            {
                Insert(nowTrem);
                using (DataSet ds = SQLHelper.Query("select top 12 * from [xg_term]"))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        modelList.Add(GetModel(Convert.ToInt32(ds.Tables[0].Rows[i]["id"])));
                    }
                    return modelList;
                }
            }
        }
        /// <summary>
        /// 根据学期名获取学期ID
        /// </summary>
        /// <param name="tremName">学期名</param>
        /// <returns>学期ID</returns>
        public int GetTremIDByTremName(string tremName)
        {
            string sql = "select id from xg_term where term_name=@term_name";
            SqlParameter[] parameters = {
					new SqlParameter("@term_name", SqlDbType.NVarChar,50)};
            parameters[0].Value = tremName;
            return Convert.ToInt32(SQLHelper.GetSingle(sql, parameters));
        }
        #endregion
    }
}
