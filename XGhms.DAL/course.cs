using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XGhms.Helper;

namespace XGhms.DAL
{
    /// <summary>
    /// 数据访问类：course
    /// </summary>
    public partial class course
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
            strSql.Append("select count(1) from xg_course");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return SQLHelper.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 根据课程ID和老师ID检查是否存在该课程
        /// </summary>
        /// <param name="id">课程ID</param>
        /// <param name="teacherID">老师ID</param>
        /// <returns>是否存在</returns>
        public bool Exists(int id, int teacherID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from xg_course");
            strSql.Append(" where id="+id);
            strSql.Append(" and teacher='" + teacherID + "'");
            return SQLHelper.Exists(strSql.ToString());
        }
        /// <summary>
        /// 根据ID来删除相应的数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true or false</returns>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from xg_course ");
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
        /// <param name="id">课程id</param>
        /// <returns>model对象</returns>
        public Model.course GetModel(int id)
        { 
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            Model.course model = new Model.course();
            DataSet ds = SQLHelper.SelectSqlReturnDataSet("course_SelectCourseById", parameters, CommandType.StoredProcedure);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["course_number"] != null && ds.Tables[0].Rows[0]["course_number"].ToString() != "")
                {
                    model.course_number = ds.Tables[0].Rows[0]["course_number"].ToString();
                }
                if (ds.Tables[0].Rows[0]["course_name"] != null && ds.Tables[0].Rows[0]["course_name"].ToString() != "")
                {
                    model.course_name = ds.Tables[0].Rows[0]["course_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["term_id"] != null && ds.Tables[0].Rows[0]["term_id"].ToString() != "")
                {
                    model.term_id = int.Parse(ds.Tables[0].Rows[0]["term_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["teacher"] != null && ds.Tables[0].Rows[0]["teacher"].ToString() != "")
                {
                    model.teacher = ds.Tables[0].Rows[0]["teacher"].ToString();
                }
                if (ds.Tables[0].Rows[0]["other_teacher"] != null && ds.Tables[0].Rows[0]["other_teacher"].ToString() != "")
                {
                    model.other_teacher = ds.Tables[0].Rows[0]["other_teacher"].ToString();
                }
                if (ds.Tables[0].Rows[0]["student_leader"] != null && ds.Tables[0].Rows[0]["student_leader"].ToString() != "")
                {
                    model.student_leader = ds.Tables[0].Rows[0]["student_leader"].ToString();
                }
                if (ds.Tables[0].Rows[0]["course_info"] != null && ds.Tables[0].Rows[0]["course_info"].ToString() != "")
                {
                    model.course_info = ds.Tables[0].Rows[0]["course_info"].ToString();
                }
                if (ds.Tables[0].Rows[0]["college_id"] != null && ds.Tables[0].Rows[0]["college_id"].ToString() != "")
                {
                    model.college_id = int.Parse(ds.Tables[0].Rows[0]["college_id"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 插入新的课程（管理员插入）
        /// </summary>
        /// <param name="course_number">课程编号</param>
        /// <param name="course_name">课程名</param>
        /// <param name="tremID">学期ID</param>
        /// <param name="terID">老师ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <returns>受影响的行数</returns>
        public int Insert(string course_number, string course_name,int tremID,int terID,int collegeID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("INSERT INTO [xg_course]([course_number],[course_name],[term_id],[teacher],[college_id])");
            str.Append(" VALUES(@course_number,@course_name,");
            str.Append(tremID+",");
            str.Append("'"+terID+"',");
            str.Append(collegeID+")");
            SqlParameter[] parameters = {
					new SqlParameter("@course_number", SqlDbType.NVarChar,50),
                                        new SqlParameter("@course_name",SqlDbType.NVarChar,50)};
            parameters[0].Value = course_number;
            parameters[1].Value = course_name;
            return SQLHelper.ExecuteSql(str.ToString(), parameters);
        }
        /// <summary>
        /// 老师更新课程信息
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="oTerID">助教ID</param>
        /// <param name="stuID">学生ID</param>
        /// <param name="courInfo">课程信息</param>
        /// <returns>受影响的行数</returns>
        public int Update(int courseID, string oTerID, string stuID, string courInfo)
        {
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [xg_course]");
            str.Append(" SET other_teacher =@other_teacher,");
            str.Append("student_leader =@student_leader,");
            str.Append("course_info = @course_info");
            str.Append(" where id=" + courseID);
            SqlParameter[] parameters = {
					new SqlParameter("@other_teacher", SqlDbType.NVarChar,100),
                    new SqlParameter("@student_leader",SqlDbType.NVarChar,50),
                    new SqlParameter("@course_info",SqlDbType.NVarChar,500)};
            parameters[0].Value = oTerID;
            parameters[1].Value = stuID;
            parameters[2].Value = courInfo;
            return SQLHelper.ExecuteSql(str.ToString(), parameters);
        }
        /// <summary>
        /// 根据课程的ID更新相应的课程
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="course_number">课程编号</param>
        /// <param name="course_name">课程名</param>
        /// <param name="tremID">学期ID</param>
        /// <param name="terID">老师ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <returns>受影响的行数</returns>
        public int UpdateOldCourse(int courseID, string course_number, string course_name, int tremID, int terID, int collegeID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("UPDATE [xg_course]");
            str.Append(" SET [course_number] =@course_number,");
            str.Append("[course_name] =@course_name,");
            str.Append("[term_id] = " + tremID);
            str.Append(",[teacher] = '" + terID+"'");
            str.Append(",[college_id] = " + collegeID);
            str.Append(" where id=" + courseID);
            SqlParameter[] parameters = {
					new SqlParameter("@course_number", SqlDbType.NVarChar,50),
                                        new SqlParameter("@course_name",SqlDbType.NVarChar,50)};
            parameters[0].Value = course_number;
            parameters[1].Value = course_name;
            return SQLHelper.ExecuteSql(str.ToString(), parameters);
        }
        /// <summary>
        /// 根据学期ID和学院ID获取课程数目
        /// </summary>
        /// <param name="tremID">课程ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <returns>课程数目</returns>
        public int GetNumTotalByTremIdandCollegeID(int tremID,int collegeID)
        {
            string sql = "select count(id) from xg_course where term_id=" + tremID + " and college_id=" + collegeID;
            object ob = SQLHelper.GetSingle(sql);
            if (ob == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(ob);
            }
        }
        /// <summary>
        /// 根据学期和学院来分页显示课程
        /// </summary>
        /// <param name="tremID">学期ID</param>
        /// <param name="collegeID">学院ID</param>
        /// <param name="PageBeginNum">开始的数目</param>
        /// <param name="PageEndNum">结束的数目</param>
        /// <returns>DataTable</returns>
        public DataTable GetSelectByTremandCollegeandPage(int tremID, int collegeID, int PageBeginNum, int PageEndNum) 
        {
            SqlParameter[] parameters = {
					new SqlParameter("@term_id", SqlDbType.Int,6),
                    new SqlParameter("@college_id", SqlDbType.Int,6),
                    new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                    new SqlParameter("@PageEndNum", SqlDbType.Int,6)};
            parameters[0].Value = tremID;
            parameters[1].Value = collegeID;
            parameters[2].Value = PageBeginNum;
            parameters[3].Value = PageEndNum;
            using (DataSet ds = SQLHelper.SelectSqlReturnDataSet("course_SelectByTremandCollegeandPage", parameters, CommandType.StoredProcedure))
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// 根据学期和老师来查询课程
        /// </summary>
        /// <param name="terID">老师ID</param>
        /// <param name="tremID">学期ID</param>
        /// <returns>课程列表</returns>
        public DataTable GetCourseListByTerTrem(int terID, int tremID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select id from xg_course where");
            str.Append(" term_id=" + tremID);
            str.Append(" and teacher='" + terID + "'");
            using (DataSet ds = SQLHelper.Query(str.ToString()))
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// 根据学期和学生ID来获取课程列表
        /// </summary>
        /// <param name="stuID">学生ID</param>
        /// <param name="tremID">学期ID</param>
        /// <returns>课程列表</returns>
        public DataTable GetCourseListByStuTrem(int stuID, int tremID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select xg_course.id from xg_course,xg_course_student ");
            str.Append("where xg_course.id=xg_course_student.course_id and xg_course_student.student_id=" + stuID);
            str.Append(" and term_id=" + tremID);
            using (DataSet ds = SQLHelper.Query(str.ToString()))
            {
                return ds.Tables[0];
            }
        }
        #endregion
    }
}
