using System;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XGhms.Helper;
using System.Collections.Generic;

namespace XGhms.DAL
{
    public partial class course_student
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
            strSql.Append("select count(1) from xg_course_student");
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
            strSql.Append("delete from xg_course_student ");
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
        /// 根据课程ID和学生ID来删除
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="StuID">学生ID</param>
        /// <returns>true or false</returns>
        public bool Delete(int courseID, int StuID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from xg_course_student ");
            strSql.Append(" where course_id=" + courseID);
            strSql.Append(" and student_id=" + StuID);
            int rows = SQLHelper.ExecuteSql(strSql.ToString());
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
        /// <param name="id">课程学生表id</param>
        /// <returns>model对象</returns>
        public Model.course_student GetModel(int id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            Model.course_student model = new Model.course_student();
            DataSet ds = SQLHelper.SelectSqlReturnDataSet("course_student_SelectCurStuById", parameters, CommandType.StoredProcedure);
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
                if (ds.Tables[0].Rows[0]["student_id"] != null && ds.Tables[0].Rows[0]["student_id"].ToString() != "")
                {
                    model.student_id = int.Parse(ds.Tables[0].Rows[0]["student_id"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据学生的ID来获取该学生的课程
        /// </summary>
        /// <param name="stuID">学生ID</param>
        /// <param name="PageBeginNum">分页开始</param>
        /// <param name="PageEndNum">分页结束</param>
        /// <returns>返回course_id的集合</returns>
        public DataTable GetListOfStuID(int stuID, int PageBeginNum, int PageEndNum)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@PageBeginNum", SqlDbType.Int,6),
                    new SqlParameter("@PageEndNum", SqlDbType.Int,6),
                    new SqlParameter("@stuId", SqlDbType.Int,6)};
            parameters[0].Value = PageBeginNum;
            parameters[1].Value = PageEndNum;
            parameters[2].Value = stuID;
            using (DataSet ds = SQLHelper.SelectSqlReturnDataSet("course_SelectPageOfNum", parameters, CommandType.StoredProcedure))
            {
                return ds.Tables[0];
            }
        }
        /// <summary>
        /// 根据课程ID和学生的ID来检查是否存在
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="stuID">学生ID</param>
        /// <returns>是否存在</returns>
        public bool Exists(int courseID,int stuID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from xg_course_student");
            strSql.Append(" where course_id=@courseID ");
            strSql.Append(" and student_id=@stuID");
            SqlParameter[] parameters = {
					new SqlParameter("@courseID", SqlDbType.Int,4),
                    new SqlParameter("@stuID",SqlDbType.Int,4)};
            parameters[0].Value = courseID;
            parameters[1].Value = stuID;
            return SQLHelper.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 执行插入
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="stuID">学生ID</param>
        /// <returns>受影响的行数</returns>
        public int Insert(int courseID, int stuID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("INSERT INTO [xg_course_student]");
            str.Append("([course_id],[student_id])");
            str.Append(" VALUES(" + courseID);
            str.Append("," + stuID+")");
            return SQLHelper.ExecuteSql(str.ToString());
        }
        /// <summary>
        /// 查询某课程的学生列表，用于导出Excel表
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns>学生列表</returns>
        public DataSet SelectCourseStudentForExcel(int courseID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select xg_users.user_number as 学号 ,xg_users_info.real_name as 姓名,xg_classes.class_name as 班级 , xg_users_info.major as 专业 ");
            str.Append(" from xg_classes,xg_course_student,xg_users,xg_users_info ");
            str.Append("where xg_course_student.student_id=xg_users.id and xg_users_info.class_id=xg_classes.id and ");
            str.Append("xg_users_info.user_id=xg_users.id and xg_course_student.course_id=" + courseID);
            return SQLHelper.Query(str.ToString());
        }
        /// <summary>
        /// 根据课程id获取学生列表
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <returns>学生ID和学生姓名</returns>
        public DataTable SelectCourseStudentList(int courseID)
        {
            string sql = "select student_id,real_name from xg_course_student,xg_users_info where xg_course_student.student_id=xg_users_info.user_id and xg_course_student.course_id=" + courseID;
            using(DataSet ds=SQLHelper.Query(sql))
            {
                return ds.Tables[0];
            }
        }
        #endregion
    }
}
