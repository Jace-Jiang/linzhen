using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XGhms.Helper;

namespace XGhms.DAL
{
    /// <summary>
    /// 数据访问类：course_homework
    /// </summary>
    public partial class course_homework
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
            strSql.Append("select count(1) from xg_course_homework");
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
            strSql.Append("delete from xg_course_homework ");
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
        /// <param name="id">课程对应的作业id</param>
        /// <returns>model对象</returns>
        public Model.course_homework GetModel(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            Model.course_homework model = new Model.course_homework();
            DataSet ds = SQLHelper.SelectSqlReturnDataSet("course_homework_SelectHomeWorkById", parameters, CommandType.StoredProcedure);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["course_id"] != null && ds.Tables[0].Rows[0]["course_id"].ToString() != "")
                {
                    model.course_id = int.Parse(ds.Tables[0].Rows[0]["course_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["homework_name"] != null && ds.Tables[0].Rows[0]["homework_name"].ToString() != "")
                {
                    model.homework_name = ds.Tables[0].Rows[0]["homework_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["homework_info"] != null && ds.Tables[0].Rows[0]["homework_info"].ToString() != "")
                {
                    model.homework_info = ds.Tables[0].Rows[0]["homework_info"].ToString();
                }
                if (ds.Tables[0].Rows[0]["homework_beginTime"] != null && ds.Tables[0].Rows[0]["homework_beginTime"].ToString() != "")
                {
                    model.homework_beginTime = DateTime.Parse(ds.Tables[0].Rows[0]["homework_beginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["homework_endTime"] != null && ds.Tables[0].Rows[0]["homework_endTime"].ToString() != "")
                {
                    model.homework_endTime = DateTime.Parse(ds.Tables[0].Rows[0]["homework_endTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据作业的ID来获取作业的说明信息
        /// </summary>
        /// <param name="hwID">作业的ID</param>
        /// <returns>返回作业的html说明</returns>
        public string GetHWinfosByID(int hwID)
        {
            string sql = "select homework_info from xg_course_homework where id=" + hwID;
            if (SQLHelper.GetSingle(sql) == null)
            {
                return "该作业没有说明";
            }
            else
            {
                return SQLHelper.GetSingle(sql).ToString();
            }
        }

        /// <summary>
        /// 根据课程的ID获取前20条作业
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns>返回作业id，作业名称，作业开始时间</returns>
        public DataTable GetTwentyHWByCourID(int courseID)
        {
            string sql = "select top 20 id,homework_name,homework_beginTime from xg_course_homework where course_id=" + courseID + " order by id desc";
            using(DataSet ds=SQLHelper.Query(sql))
            {
                return ds.Tables[0];
            }
        }
        #endregion
        #region 扩展方法=============================================
        /// <summary>
        /// 根据课程ID获取作业的总数，用于分页
        /// </summary>
        /// <param name="CourseID">课程ID</param>
        /// <returns>总数</returns>
        public int GetPageNumOfCourseID(int CourseID)
        {
            string sql = "select count(id) from xg_course_homework where course_id=" + CourseID;
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
        /// 分页，用于查询课程作业id
        /// </summary>
        /// <param name="CourseID">课程ID</param>
        /// <param name="PageBeginNum">开始数目</param>
        /// <param name="PageEndNum">结束数目</param>
        /// <returns>id的table</returns>
        public DataTable GetPageOfCourseID(int CourseID, int PageBeginNum, int PageEndNum)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@courId", SqlDbType.Int,4),
                                        new SqlParameter("@PageBeginNum", SqlDbType.Int),
                                        new SqlParameter("@PageEndNum", SqlDbType.Int)};
            parameters[0].Value = CourseID;
            parameters[1].Value = PageBeginNum;
            parameters[2].Value = PageEndNum;
            using (DataSet ds = SQLHelper.SelectSqlReturnDataSet("course_homework_SelectHomeWorkByCidPage", parameters, CommandType.StoredProcedure))
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// 插入新的作业
        /// </summary>
        /// <param name="cid">课程ID</param>
        /// <param name="hwName">作业名称</param>
        /// <param name="hwInfo">作业信息</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>该作业的ID</returns>
        public int InsertNewHWGethwID(int cid,string hwName,string hwInfo,string beginTime,string endTime)
        {
            StringBuilder str = new StringBuilder();
            str.Append("INSERT INTO [xg_course_homework]");
            str.Append("([course_id],[homework_name],[homework_info],[homework_beginTime],[homework_endTime])");
            str.Append("VALUES(@cid,@hwName,@hwInfo,@beginTime,@endTime) select @@identity");
            SqlParameter[] parameters = { 
                                            new SqlParameter("@cid",SqlDbType.Int),
                                            new SqlParameter("@hwName",SqlDbType.NVarChar),
                                            new SqlParameter("@hwInfo",SqlDbType.NText),
                                            new SqlParameter("@beginTime",SqlDbType.DateTime),
                                            new SqlParameter("@endTime",SqlDbType.DateTime)
                                        };
            parameters[0].Value = cid;
            parameters[1].Value = hwName;
            parameters[2].Value = hwInfo;
            parameters[3].Value = beginTime;
            parameters[4].Value = endTime;
            int hwID = Convert.ToInt32(SQLHelper.GetSingle(str.ToString(), parameters));
            if (hwID > 0)
            {
                return hwID;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 更新修改作业
        /// </summary>
        /// <param name="id">作业ID</param>
        /// <param name="hwName">作业名称</param>
        /// <param name="hwInfo">作业信息</param>
        /// <param name="beginTime">作业开始时间</param>
        /// <param name="endTime">作业结束时间</param>
        /// <returns>受影响的行数</returns>
        public int UpdateHW(int id, string hwName, string hwInfo, string beginTime, string endTime)
        {
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [xg_course_homework]");
            str.Append("SET [homework_name] =@hwName,");
            str.Append(" [homework_info] =@hwInfo,");
            str.Append(" [homework_beginTime] =@beginTime,");
            str.Append(" [homework_endTime] =@endTime");
            str.Append(" where id="+id);
            SqlParameter[] parameters = { 
                                            new SqlParameter("@hwName",SqlDbType.NVarChar),
                                            new SqlParameter("@hwInfo",SqlDbType.NText),
                                            new SqlParameter("@beginTime",SqlDbType.DateTime),
                                            new SqlParameter("@endTime",SqlDbType.DateTime)
                                        };
            parameters[0].Value = hwName;
            parameters[1].Value = hwInfo;
            parameters[2].Value = beginTime;
            parameters[3].Value = endTime;
            return SQLHelper.ExecuteSql(str.ToString(), parameters);
        }
        #endregion
    }
}
