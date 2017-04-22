using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;

namespace XGhms.Helper
{
    public abstract class SQLHelper
    {
        //获取web.config里面的数据库连接字符串
        public static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public SQLHelper() { }
        #region 公共方法=============================================
        /// <summary>
        /// 判断是否存在某表的某个字段
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnName">列名称</param>
        /// <returns>是否存在</returns>
        public static bool ColumnExists(string tableName, string columnName)
        {
            string sql = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
            object res = GetSingle(sql);
            if (res == null)
            {
                return false;
            }
            return Convert.ToInt32(res) > 0;
        }
        /// <summary>
        /// 获取一张表里面的某个字段的最小值
        /// </summary>
        /// <param name="FieldName">字段名称</param>
        /// <param name="TableName">表名称</param>
        /// <returns>返回最小值</returns>
        public static int GetMinID(string FieldName, string TableName)
        {
            string strsql = "select min(" + FieldName + ") from " + TableName;
            object obj = SQLHelper.GetSingle(strsql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        /// <summary>
        /// 获取一张表里面的某个字段的最大值
        /// </summary>
        /// <param name="FieldName">字段名称</param>
        /// <param name="TableName">表名称</param>
        /// <returns>返回最大值</returns>
        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = SQLHelper.GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        /// <summary>
        /// 根据sql语句来判断是否存在该记录
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>true or false</returns>
        public static bool Exists(string strSql)
        {
            object obj = SQLHelper.GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="TableName">表的名称</param>
        /// <returns>true or false</returns>
        public static bool TabExists(string TableName)
        {
            string strsql = "select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
            object obj = SQLHelper.GetSingle(strsql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 判断执行查询语句是否有返回值
        /// </summary>
        /// <param name="strSql">查询SQL语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>true或者false</returns>
        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            object obj = SQLHelper.GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        /// <summary>
        /// 基于带阐述的SQL命令查询（SQL语句和存储过程都适用）
        /// </summary>
        /// <param name="sql">sql=“select * from tbUser where userid=@userid and departmentID=@departmentid”</param>
        /// <param name="pars">参数</param>
        /// <param name="type">类型</param>
        /// <returns>DataSet</returns>
        public static DataSet SelectSqlReturnDataSet(string sql, SqlParameter[] pars, CommandType type)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            if (pars != null && pars.Length > 0)
            {
                foreach (SqlParameter p in pars)
                {
                    sda.SelectCommand.Parameters.Add(p);
                }
            }
            sda.SelectCommand.CommandType = type;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }


        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行的Transact-SQL语句、表名或存储过程
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <param name="conn">打开的连接</param>
        /// <param name="trans">表明要处理的Transact-SQL事务</param>
        /// <param name="cmdText">处理名称</param>
        /// <param name="cmdParms">SqlParameter参数</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            if (!cmdText.Contains(" "))
            {
                cmd.CommandType = CommandType.StoredProcedure;//cmdType;
            }
            else
            {
                cmd.CommandType = CommandType.Text;//cmdType;
            }
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）并且能够自己设置错误等待时间
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <param name="Times">错误等待时间</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }

        }
        public static DataSet Query(string SQLString, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.SelectCommand.CommandTimeout = Times;
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        /// <summary>
        /// 2012-2-21新增重载，执行查询语句，返回DataSet
        /// </summary>
        /// <param name="connection">SqlConnection对象</param>
        /// <param name="trans">SqlTransaction事务</param>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(SqlConnection connection, SqlTransaction trans, string SQLString)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                command.SelectCommand.Transaction = trans;
                command.Fill(ds, "ds");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return ds;

        }

    }
}
